using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IWantICan.Core.Helpers;
using IWantICan.Core.Interfaces;
using IWantICan.Core.Models;
using IWantICan.Core.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.Core;

namespace IWantICan.Core.ViewModels
{
	public class MyProfileViewModel : BaseViewModel
	{
		private UserModel _user;
		IUserService _userService;
		IDialogService _dialogService;

		public MyProfileViewModel(IUserService UserService, IDialogService DialogService)
		{
			_userService = UserService;
			_dialogService = DialogService;

			Task t = new Task(LoadUser);
			t.Start();
		}

		private async void LoadUser()
		{
			User = await _userService.GetCurrentUser();

			if (User == null)
			{
				var mtDispatcher = Mvx.Resolve<IMvxMainThreadDispatcher>();
				mtDispatcher.RequestMainThreadAction(() =>
				{
					_dialogService.Alert(
						Constants.DialogNoNetwork,
						Constants.DialogTitleError,
						Constants.DialogButtonOk,
						() => Close(this));
				});
			}
		}

		public IMvxCommand SaveCommand
		{
			get { return new MvxCommand(SaveUser); }
		}

		private async void SaveUser()
		{
			var toCheck = new List<Tuple<string, string, ValidationType>>
			{
				new Tuple<string, string, ValidationType> (User.name, "Name", ValidationType.Common),
				new Tuple<string, string, ValidationType> (User.surname, "Surname", ValidationType.Common)
			};
			if (!string.IsNullOrWhiteSpace(User.phone))
				toCheck.Add(new Tuple<string, string, ValidationType>(User.phone, "Phone", ValidationType.Phone));
			if (!string.IsNullOrWhiteSpace(User.email))
				toCheck.Add(new Tuple<string, string, ValidationType>(User.email, "Email", ValidationType.Email));
			if (!string.IsNullOrWhiteSpace(User.vkLink))
				toCheck.Add(new Tuple<string, string, ValidationType>(User.vkLink, "Vk", ValidationType.Vk));

			var validated = ValidatorHelper.Validate(toCheck, ref _errors);
			if (validated)
			{
				var ok = await _userService.UpdateUser(User);
				if (ok)
					_dialogService.Alert(Constants.DialogSaveSuccess,
						Constants.DialogTitleSuccess,
						"ОК",
						() => Close(this));
				else
					_dialogService.Alert(Constants.DialogSaveFailed,
						Constants.DialogTitleError,
						"ОК");
			}
		}

		public UserModel User
		{
			get { return _user; }
			set { _user = value; RaisePropertyChanged(() => User); }
		}

		private ObservableDictionary<string, string> _errors = new ObservableDictionary<string, string>();
		public ObservableDictionary<string, string> Errors
		{
			get { return _errors; }
			set { _errors = value; RaisePropertyChanged(() => Errors); }
		}
	}
}
