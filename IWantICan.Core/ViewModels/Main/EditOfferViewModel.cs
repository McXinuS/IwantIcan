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
    public class EditOfferViewModel : BaseViewModel
    {
        private ICanService _canService;
        private IWantService _wantService;
        private IDialogService _dialogService;

        private ICategoryService _categoryService;
        private List<CategoryModel> _categories;

	    private OfferModel _offer;

	    public OfferModel Offer
	    {
		    get { return _offer; }
			set { _offer = value; RaisePropertyChanged(() => Offer); }
	    }

        public EditOfferViewModel(ICanService canService,
            IWantService wantService,
            ICategoryService categoryService,
            IDialogService dialogService)
        {
            _canService = canService;
            _wantService = wantService;
            _categoryService = categoryService;
            _dialogService = dialogService;

            Categories = categoryService.GetCategoryList();
        }

        public void Init(string offer)
        {
			Offer = offer.Deserialize<OfferModel>();
        }

        public IMvxCommand SaveCommand
        {
            get { return new MvxCommand(Save); }
        }

        private async void Save()
        {
            if (!Validate())
                return;

            bool ok;

            if (Offer.type == OfferType.Can)
                ok = await _canService.UpdateCan(Offer);
            else
                ok = await _wantService.UpdateWant(Offer);

            if (ok)
                _dialogService.Alert(
					Constants.DialogSaveSuccess,
                    Constants.DialogTitleSuccess,
                    "ОК",
                    () => Close(this));
            else
                _dialogService.Alert(
					Constants.DialogSaveFailed,
                    Constants.DialogTitleError,
                    "ОК");
        }

        private bool Validate()
        {
            var toCheck = new List<Tuple<string, string, ValidationType>>
            {
                new Tuple<string, string, ValidationType> (Offer.name, "Name", ValidationType.Common),
                new Tuple<string, string, ValidationType> (Offer.description, "Description", ValidationType.Common)
            };
            return ValidatorHelper.Validate(toCheck, ref _errors);
        }

        public IMvxCommand DeleteCommand
        {
            get { return new MvxCommand(DoDeleteCommand); }
        }

        private void DoDeleteCommand()
        {
            _dialogService.Alert(
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
				ok = await _canService.DeleteCan(Offer.id);
            else
                ok = await _wantService.DeleteWant(Offer.id);

			if (ok)
                _dialogService.Alert(Constants.DialogDeleteSuccess,
                    Constants.DialogTitleSuccess,
                    "ОК",
                    () => Close(this));
            else
                _dialogService.Alert(Constants.DialogDeleteFailed,
                    Constants.DialogTitleError,
                    "ОК");
        }

        public IMvxCommand ShowFilterCommand
        {
            get { return new MvxCommand(ShowFilter); }
        }

        private void ShowFilter()
        {
            var categories = Categories.Select(c => c.name).ToArray();
            _dialogService.Filter(
				categories,
				_categoryService.IndexOf(Offer.subCategoryModelId) + 1,
				s => Offer.subCategoryModelId = Categories[s - 1].id);
        }

        public List<CategoryModel> Categories
        {
            get { return _categories; }
            set { _categories = value; RaisePropertyChanged(() => Categories); }
        }

        private ObservableDictionary<string, string> _errors = new ObservableDictionary<string, string>();
        public ObservableDictionary<string, string> Errors
        {
            get { return _errors; }
            set { _errors = value; RaisePropertyChanged(() => Errors); }
        }
    }
}
