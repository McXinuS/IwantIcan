using System;
using System.Collections.Generic;
using System.Linq;
using IWantICan.Core.Helpers;
using IWantICan.Core.Interfaces;
using IWantICan.Core.Models;
using IWantICan.Core.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace IWantICan.Core.ViewModels
{
    public class CreateOfferViewModel : BaseViewModel
    {
        private ICanService _canService;
        private IWantService _wantService;
        private IDialogService _dialogService;

        private ICategoryService _categoryService;
        private List<CategoryModel> _categories;

        private string _name;
        private string _description;
        private int _category;

        private OfferType _type;

        public CreateOfferViewModel(ICanService canService,
            IWantService wantService,
            ICategoryService categoryService,
            IDialogService dialogService)
        {
            _canService = canService;
            _wantService = wantService;
            _categoryService = categoryService;
            _dialogService = dialogService;

            Categories = categoryService.GetCategoryList();
            Category = Categories[0].id;
        }

        public void Init(int type)
        {
            _type = (OfferType)type;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged(() => Name); }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; RaisePropertyChanged(() => Description); }
        }

        public int Category
        {
            get { return _category; }
            set { _category = value; RaisePropertyChanged(() => Category); }
        }

        public IMvxCommand SaveCommand
        {
            get { return new MvxCommand(SaveCan); }
        }

        private async void SaveCan()
        {
            if (!Validate())
                return;

            bool ok;
            if (_type.Equals(OfferType.Can))
            {
                // create can model
                CanModel can = new CanModel
                {
                    name = Name,
                    description = Description,
                    subCategoryModelId = Category,
                    imgurl = Constants.ImagePlaceholderUrl + new Random().Next(1, 1000)
                };
                ok = await _canService.CreateCan(can);
            }
            else
            {
                // create want model
                WantModel want = new WantModel
                {
                    name = Name,
                    description = Description,
                    subCategoryModelId = Category,
                    imgurl = Constants.ImagePlaceholderUrl + new Random().Next(1, 1000)
                };
                ok = await _wantService.CreateWant(want);
            }
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

        private bool Validate()
        {
            var toCheck = new List<Tuple<string, string, ValidationType>>
            {
                new Tuple<string, string, ValidationType> (Name, "Name", ValidationType.Common),
                new Tuple<string, string, ValidationType> (Description, "Description", ValidationType.Common)
            };
            return ValidatorHelper.Validate(toCheck, ref _errors);
        }
        
        public IMvxCommand ShowFilterCommand
        {
            get { return new MvxCommand(ShowFilter); }
        }
        
        private void ShowFilter()
        {
            var categories = Categories.Select(c => c.name).ToArray();
            _dialogService.Filter(categories, _categoryService.IndexOf(Category)+1, s => Category = Categories[s-1].id);
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
