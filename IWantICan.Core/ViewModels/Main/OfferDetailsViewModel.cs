using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Android.Text.Method;
using IWantICan.Core.Helpers;
using IWantICan.Core.Interfaces;
using IWantICan.Core.Models;
using IWantICan.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IWantICan.Core.ViewModels
{
	public class OfferDetailsViewModel : BaseViewModel
	{
		IUserService _userService;
		IDialogService _dialogService;

		private OfferModel _offer;
		private UserModel _user;

		public OfferModel Offer
		{
			get { return _offer; }
			set { _offer = value; RaisePropertyChanged(() => Offer); }
		}

		public UserModel User
		{
			get { return _user; }
			set { _user = value; RaisePropertyChanged(() => User); }
		}

		public OfferDetailsViewModel(
			IUserService UserService,
			IDialogService DialogService)
		{
			_userService = UserService;
			_dialogService = DialogService;
		}

		public void Init(string offer)
		{
			Task t = new Task(() => LoadData(offer));
			t.Start();
		}

		private async void LoadData(string offer)
		{
			Offer = offer.Deserialize<OfferModel>();
			User = await _userService.GetUser(Offer.UserModelId);
		}

		public IMvxCommand GoProfileViewCommand
		{
			get
			{
				return new MvxCommand(GoProfileView);
			}
		}

		private void GoProfileView()
		{
			if (User == null) return;

			object param = new { user = User.Serialize() };
			ShowViewModel<OffererProfileViewModel>(param);
		}

		public ICommand ShowContactsCommand
		{
			get { return new MvxCommand(() => _dialogService.ContactDialog(User)); }
		}
	}
}
