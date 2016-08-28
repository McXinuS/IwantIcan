using System;
using System.Collections.Generic;
using System.Linq;
using IWantICan.Core.Helpers;
using IWantICan.Core.Interfaces;
using IWantICan.Core.Models;
using IWantICan.Core.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.Core;

namespace IWantICan.Core.ViewModels
{
	public class CreateOfferViewModel : BaseEditOfferViewModel
	{
		protected sealed override ICanService CanService { get; set; }
		protected sealed override IWantService WantService { get; set; }
		protected sealed override ICategoryService CategoryService { get; set; }
		protected sealed override IDialogService DialogService { get; set; }

		public CreateOfferViewModel(ICanService canService,
			IWantService wantService,
			ICategoryService categoryService,
			IDialogService dialogService)
		{
			CanService = canService;
			WantService = wantService;
			CategoryService = categoryService;
			DialogService = dialogService;

			Offer = new OfferModel();

			Categories = categoryService.GetCategoryList();

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

		protected override async void Save()
		{
			if (!Validate())
				return;

			var img = Constants.ImagePlaceholderUrl + new Random().Next(1, 1000);
			Offer.imgurl = img;
			
			bool ok;
			if (Offer.type.Equals(OfferType.Can))
				ok = await CanService.CreateCan(Offer);
			else
				ok = await WantService.CreateWant(Offer);

			if (ok)
				DialogService.Alert(Constants.DialogSaveSuccess,
					Constants.DialogTitleSuccess,
					Constants.DialogButtonOk,
					() => Close(this));
			else
				DialogService.Alert(Constants.DialogSaveFailed,
					Constants.DialogTitleError,
					Constants.DialogButtonOk);
		}
	}
}
