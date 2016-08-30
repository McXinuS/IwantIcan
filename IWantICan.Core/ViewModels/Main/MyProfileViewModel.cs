using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IWantICan.Core.Helpers;
using IWantICan.Core.Interfaces;
using IWantICan.Core.Models;
using IWantICan.Core.Services;
using IWantICan.Core.Services.Messenger;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.Core;

namespace IWantICan.Core.ViewModels
{
	public class MyProfileViewModel : BaseViewModel
	{
		private UserModel _user;
		private readonly IUserService _userService;
		private readonly IDialogService _dialogService;
		private readonly IMessengerService _messenger;

		private ObservableDictionary<string, string> _errors = new ObservableDictionary<string, string>();

		public MyProfileViewModel(IUserService userService,
			IDialogService dialogService,
			IMessengerService messenger)
		{
			_userService = userService;
			_dialogService = dialogService;
			_messenger = messenger;

			Task.Run(LoadUser);
		}

		private async Task LoadUser()
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
				new Tuple<string, string, ValidationType> (User.surname, "Surname", ValidationType.Common),
				new Tuple<string, string, ValidationType> (User.phone, "Phone", ValidationType.Phone),
				new Tuple<string, string, ValidationType> (User.email, "Email", ValidationType.Email),
				new Tuple<string, string, ValidationType> (User.vkLink, "Vk", ValidationType.Vk)
			};

			var validated = ValidatorHelper.Validate(toCheck, ref _errors);
			if (validated)
			{
				var ok = await _userService.UpdateUser(User);
				if (ok)
				{
					_messenger.SendProfileEditSuccessMessage(this);
					_dialogService.Alert(Constants.DialogSaveSuccess,
						Constants.DialogTitleSuccess,
						"ОК",
						() => Close(this));
				}
				else
				{
					_dialogService.Alert(Constants.DialogSaveFailed,
						Constants.DialogTitleError,
						"ОК");
				}
			}
		}

		public UserModel User
		{
			get { return _user; }
			set { _user = value; RaisePropertyChanged(() => User); }
		}

		public ObservableDictionary<string, string> Errors
		{
			get { return _errors; }
			set { _errors = value; RaisePropertyChanged(() => Errors); }
		}
	}
}
