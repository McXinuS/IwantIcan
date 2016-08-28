using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IWantICan.Core.Helpers;
using IWantICan.Core.Interfaces;
using IWantICan.Core.Models;
using IWantICan.Core.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace IWantICan.Core.ViewModels
{
    public class EditOfferViewModel : BaseEditOfferViewModel
	{
		protected sealed override ICanService CanService { get; set; }
		protected sealed override IWantService WantService { get; set; }
		protected sealed override ICategoryService CategoryService { get; set; }
		protected sealed override IDialogService DialogService { get; set; }

		public EditOfferViewModel(ICanService canService,
            IWantService wantService,
            ICategoryService categoryService,
            IDialogService dialogService)
        {
            CanService = canService;
            WantService = wantService;
            CategoryService = categoryService;
            DialogService = dialogService;

            Categories = categoryService.GetCategoryList();
        }

	    public void Init(string offer)
        {
			Offer = offer.Deserialize<OfferModel>();
			Type = Offer.type;
		    Category = Offer.subCategoryModelId;
        }

        protected override async void Save()
        {
            if (!Validate())
                return;

            bool ok;

            if (Offer.type == OfferType.Can)
                ok = await CanService.UpdateCan(Offer);
            else
                ok = await WantService.UpdateWant(Offer);

            if (ok)
                DialogService.Alert(
					Constants.DialogSaveSuccess,
                    Constants.DialogTitleSuccess,
                    "ОК",
                    () => Close(this));
            else
                DialogService.Alert(
					Constants.DialogSaveFailed,
                    Constants.DialogTitleError,
                    "ОК");
        }

        public IMvxCommand DeleteCommand
        {
            get { return new MvxCommand(DoDeleteCommand); }
        }

        private void DoDeleteCommand()
        {
            DialogService.Alert(
				Constants.DialogDeleteConfirm,
				Constants.DialogTitleConfirm,
				Constants.DialogButtonOk,
				Constants.DialogButtonCancel, 
				Delete);
        }

        private async void Delete()
        {
            bool ok;

			if (Offer.type == OfferType.Can)
				ok = await CanService.DeleteCan(Offer.id);
            else
                ok = await WantService.DeleteWant(Offer.id);

			if (ok)
                DialogService.Alert(Constants.DialogDeleteSuccess,
                    Constants.DialogTitleSuccess,
                    "ОК",
                    () => Close(this));
            else
                DialogService.Alert(Constants.DialogDeleteFailed,
                    Constants.DialogTitleError,
                    "ОК");
        }
	}
}
