using System;
using System.Collections.Generic;
using System.Windows.Input;
using IWantICan.Core.Helpers;
using IWantICan.Core.Interfaces;
using IWantICan.Core.Models;
using IWantICan.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IWantICan.Core.ViewModels
{
	public class SignupViewModel : BaseViewModel
	{
		private readonly IUserService _userService;
		private readonly IDialogService _dialogService;

		ObservableDictionary<string, string> _errors = new ObservableDictionary<string, string>();

		bool _isLoading;

		string _login;
		string _password;
		string _name;
		string _surname;
		string _email;
		string _phone;
		string _vklink;

		public SignupViewModel(IUserService userService, IDialogService dialogService)
		{
			_userService = userService;
			_dialogService = dialogService;
			IsLoading = false;
		}

		public IMvxCommand SignupCommand
		{
			get { return new MvxCommand(DoSignupCommand); }
		}

		private async void DoSignupCommand()
		{
			var toCheck = new List<Tuple<string, string, ValidationType>>
			{
				new Tuple<string, string, ValidationType> (Login, "Login", ValidationType.Login),
				new Tuple<string, string, ValidationType> (Password, "Password", ValidationType.Password),
				new Tuple<string, string, ValidationType> (Name, "Name", ValidationType.Common),
				new Tuple<string, string, ValidationType> (Surname, "Surname", ValidationType.Common),
				new Tuple<string, string, ValidationType>(Phone, "Phone", ValidationType.Phone),
				new Tuple<string, string, ValidationType>(Email, "Email", ValidationType.Email),
				new Tuple<string, string, ValidationType>(VkLink, "Vk", ValidationType.Vk)
			};

			var validated = ValidatorHelper.Validate(toCheck, ref _errors);
			if (!validated)
				return;

			IsLoading = true;

			var user = new UserModel
			{
				email = Email,
				login = Login,
				name = Name,
				surname = Surname,
				password = Password,
				phone = Phone,
				vkLink = VkLink
			};

			var ok = await _userService.CreateUser(user);
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
			}
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

		public string Name
		{
			get { return _name; }
			set { _name = value; RaisePropertyChanged(() => Name); }
		}

		public string Surname
		{
			get { return _surname; }
			set { _surname = value; RaisePropertyChanged(() => Surname); }
		}

		public string Email
		{
			get { return _email; }
			set { _email = value; RaisePropertyChanged(() => Email); }
		}

		public string Phone
		{
			get { return _phone; }
			set { _phone = value; RaisePropertyChanged(() => Phone); }
		}

		public string VkLink
		{
			get { return _vklink; }
			set { _vklink = value; RaisePropertyChanged(() => VkLink); }
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
