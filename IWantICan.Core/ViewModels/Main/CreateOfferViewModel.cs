using System;
using System.Collections.Generic;
using System.Linq;
using IWantICan.Core.Interfaces;
using IWantICan.Core.Models;
using IWantICan.Core.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace IWantICan.Core.ViewModels
{
    public class CreateOfferViewModel : BaseViewModel
    {
        private IDialogService _dialogService;
        private ICanService _canService;
        private IWantService _wantService;
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
            _dialogService = dialogService;
            _canService = canService;
            _wantService = wantService;
            Categories = categoryService.GetCategoryList();
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
            bool ok;
            if (_type.Equals(OfferType.Can))
            {
                CanModel can = new CanModel
                {
                    name = Name,
                    description = Description,
                    subCategoryModelId = Category,
                    // TODO update when API is updated
                    imgurl = Constants.ImagePlaceholderUrl + new Random().Next(1, 1000)
                };
                ok = await _canService.CreateCan(can);
            }
            else
            {
                WantModel want = new WantModel
                {
                    name = Name,
                    description = Description,
                    subCategoryModelId = Category,
                    // TODO update when API is updated
                    imgurl = Constants.ImagePlaceholderUrl + new Random().Next(1, 1000)
                };
                ok = await _wantService.CreateWant(want);
            }
            if (ok)
                _dialogService.Alert(Constants.DialogSaveOk, 
                    Constants.DialogTitleOk, 
                    "ОК", 
                    () => Close(this));
            else
                _dialogService.Alert(Constants.DialogSaveFailed, 
                    Constants.DialogTitleError, 
                    "ОК");
        }
        
        public IMvxCommand ShowFilterCommand
        {
            get { return new MvxCommand(ShowFilter); }
        }
        
        private async void ShowFilter()
        {
            var categories = Categories.Select(c => c.name).ToArray();
            _dialogService.Filter(categories, Category, s => Category = s);
        }

        public List<CategoryModel> Categories
        {
            get { return _categories; }
            set { _categories = value; RaisePropertyChanged(() => Categories); }
        }
    }
}
