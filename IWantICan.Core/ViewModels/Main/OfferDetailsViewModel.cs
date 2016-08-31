using System;
using System.Collections.Generic;
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

		private OfferModel _offer;
		private UserModel _user;
		private List<ContactsEntry> _contacts;

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

		public List<ContactsEntry> Contacts
		{
			get { return _contacts; }
			set { _contacts = value; RaisePropertyChanged(() => Contacts); }
		}

		public OfferDetailsViewModel(
			IUserService userService)
		{
			_userService = userService;
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

			Contacts = new List<ContactsEntry>
			{
				new ContactsEntry(ContactType.Phone, User.phone),
				new ContactsEntry(ContactType.Email, User.email),
				new ContactsEntry(ContactType.VkLink, User.vkLink)
			};
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
	}
}
