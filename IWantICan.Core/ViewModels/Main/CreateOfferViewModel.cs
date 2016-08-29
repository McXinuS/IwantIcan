using System;
using System.Threading.Tasks;
using IWantICan.Core.Interfaces;
using IWantICan.Core.Models;
using IWantICan.Core.Services;
using IWantICan.Core.Services.Messenger;
using MvvmCross.Platform;
using MvvmCross.Platform.Core;

namespace IWantICan.Core.ViewModels
{
	public class CreateOfferViewModel : BaseEditOfferViewModel
	{
		public CreateOfferViewModel(ICanService canService,
			IWantService wantService,
			ICategoryService categoryService,
			IDialogService dialogService,
			IMessengerService messenger)
			: base(canService, wantService, categoryService, dialogService, messenger)
		{
			Offer = new OfferModel();

			if (Categories == null || Categories.Count == 0)
			{
				var mtDispatcher = Mvx.Resolve<IMvxMainThreadDispatcher>();
				mtDispatcher.RequestMainThreadAction(() =>
				{
					DialogService.Alert(
						Constants.DialogNoNetwork,
						Constants.DialogTitleError,
						Constants.DialogButtonOk,
						() => Close(this));
				});
				return;
			}

			//Category = Categories[0].id;
		}

		public void Init(int type)
		{
			Type = (OfferType)type;
		}

		protected override async Task<bool> OnSave(OfferModel offer)
		{
			var img = Constants.ImagePlaceholderUrl + new Random().Next(1, 1000);
			Offer.imgurl = img;

			bool ok;

			if (Offer.type.Equals(OfferType.Can))
			{
				ok = await CanService.CreateCan(Offer);
			}
			else
			{
				ok = await WantService.CreateWant(Offer);
			}

			if (ok)
			{
				SendOfferActionMessage(MessengerOfferActionType.Create);
			}

			return ok;
		}
	}
}
