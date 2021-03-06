﻿using System;
using System.Collections.Generic;
using System.Windows.Input;
using IWantICan.Core.Helpers;
using IWantICan.Core.Interfaces;
using IWantICan.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IWantICan.Core.ViewModels
{
	public class StartViewModel : BaseViewModel
	{
		private readonly IAuthService _authService;
		private readonly IDialogService _dialogService;

		string _login;
		string _password;

		private ObservableDictionary<string, string> _errors = new ObservableDictionary<string, string>();

		bool _isLoading;


		public StartViewModel(ISharedPreferencesService sharedPreferencesService,
			IAuthService AuthService,
			IDialogService DialogService)
		{
			_authService = AuthService;
			_dialogService = DialogService;
			Login = sharedPreferencesService.LastLogin;
			IsLoading = false;
		}

		public ICommand TryLoginCommand
		{
			get { return new MvxCommand(() => DoLoginCommand()); }
		}

		public async void DoLoginCommand()
		{
			var toCheck = new List<Tuple<string, string, ValidationType>>
			{
				new Tuple<string,  string,ValidationType>(Login, "Login", ValidationType.Login),
				new Tuple<string,  string,ValidationType>(Password, "Password", ValidationType.Password)
			};
			var validated = ValidatorHelper.Validate(toCheck, ref _errors);
			if (!validated)
				return;

			IsLoading = true;
			var ok = await _authService.Login(Login, Password);
			IsLoading = false;

			if (ok)
			{
				ShowViewModel<MainViewModel>();
				Close(this);
			}
			else
			{
				_dialogService.Alert(
					Constants.OperationFailed,
					Constants.DialogTitleError,
					Constants.DialogButtonOk);
				Errors["Login"] = Constants.LoginOrPasswordWarning;
				Errors["Password"] = Constants.LoginOrPasswordWarning;
			}
		}

		public ICommand GoSignupViewCommand
		{
			get { return new MvxCommand(() => ShowViewModel<SignupViewModel>()); }
		}

		public string Login
		{
			get { return _login; }
			set { _login = value; RaisePropertyChanged(() => Login); }
		}

		public string Password
		{
			get { return _password; }
			set { _password = value; RaisePropertyChanged(() => Password); }
		}

		public bool IsLoading
		{
			get { return _isLoading; }
			set { _isLoading = value; RaisePropertyChanged(() => IsLoading); }
		}

		public ObservableDictionary<string, string> Errors
		{
			get { return _errors; }
			set { _errors = value; RaisePropertyChanged(() => Errors); }
		}
	}
}
