﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using IWantICan.Core.Helpers;
using IWantICan.Core.Interfaces;
using IWantICan.Core.Models;
using IWantICan.Core.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace IWantICan.Core.ViewModels
{
	public class OffererProfileViewModel : BaseViewModel
	{
		private UserModel _user;
		private List<ContactsEntry> _contacts;
        private List<OfferersOfferList> _offers;
		
		public UserModel User
		{
			get { return _user; }
			set { _user = value; RaisePropertyChanged(() => User); }
		}

		public List<OfferersOfferList> Offers
        {
            get { return _offers; }
            set { _offers = value; RaisePropertyChanged(() => Offers); }
        }

		public List<ContactsEntry> Contacts
		{
			get { return _contacts; }
			set { _contacts = value; RaisePropertyChanged(() => Contacts); }
		}

		public void Init(string user)
		{
			Task t = new Task(() => LoadData(user));
			t.Start();
        }

	    private async void LoadData(string user)
		{
			User = user.Deserialize<UserModel>();

			if (User == null) return;
			Contacts = new List<ContactsEntry>
			{
				new ContactsEntry(ContactType.Phone, User.phone),
				new ContactsEntry(ContactType.Email, User.email),
				new ContactsEntry(ContactType.VkLink, User.vkLink)
			};


			ICanService canService = Mvx.Resolve<ICanService>();
            IWantService wantService = Mvx.Resolve<IWantService>();
			
		    var canList = new OfferersOfferList { Header = "Может" };
	        var userCans = await canService.GetCanListByUser(User.id);
		    canList.AddRange(userCans);

			var wantList = new OfferersOfferList { Header = "Хочет" };
	        var userWants = await wantService.GetWantListByUser(User.id);
			wantList.AddRange(userWants);
			
			Offers = new List<OfferersOfferList>
            {
				canList,
				wantList
			};
		}

		public ICommand OfferSelectedCommand
		{
			get { return new MvxCommand<OfferModel>(item => GoDetails(item)); }
		}

		protected void GoDetails(OfferModel item)
		{
			// bad offer
			if (item == null)
			{
				Mvx.Resolve<IDialogService>().Alert("Произошла внутренняя ошибка. Повторите запрос позже", "Ошибка", "ОК");
				return;
			}

			ShowViewModel<OfferDetailsContainerViewModel>(new
			{
				offer = item.Serialize()
			});
		}

		public ICommand ContactSelectedCommand
		{
			get { return new MvxCommand<ContactsEntry>(contact => { return; }); }
		}
	}
}
