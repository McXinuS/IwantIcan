using System;
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
        private List<List<OfferModel>> _offers;
		
		public UserModel User
		{
			get { return _user; }
			set { _user = value; RaisePropertyChanged(() => User); }
		}

		public List<List<OfferModel>> Offers
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
				new ContactsEntry(
					"res:ic_call_black_24dp",
					User.phone,
					Constants.ProfileMobile),
				new ContactsEntry(
					"res:ic_email_black_24dp",
					User.email,
					Constants.ProfileEmail),
				new ContactsEntry(
					"res:ic_vk_black_24dp",
					User.vkLink,
					Constants.ProfileVk)
			};

			ICanService canService = Mvx.Resolve<ICanService>();
            IWantService wantService = Mvx.Resolve<IWantService>();

	        var cans = await canService.GetCanListByUser(User.id);
	        var wants = await wantService.GetWantListByUser(User.id);

            //var canNames = cans.Select(t => t.name).ToList();
            //var wantNames = wants.Select(t => t.name).ToList();

	        Offers = new List<List<OfferModel>>
            {
				cans,
				wants
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
	}
}
