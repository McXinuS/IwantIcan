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

        private int _id;
        private string _name;
        private string _description;
        private int _category;
        private string _imgUrl;

        private string _type;

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
            Category = Categories[0].id;
        }

        public void Init(string offer, string type)
        {
            _type = type;
            Task t = new Task(() => LoadData(offer, type));
            t.Start();
        }

        private void LoadData(string offer, string type)
        {
            if (type.Contains("CanModel"))
            {
                var Can = offer.Deserialize<CanModel>();
                if (Can != null)
                {
                    _id = Can.id;
                    Name = Can.name;
                    Description = Can.description;
                    Category = (int)Can.subCategoryModelId;
                    _imgUrl = Can.imgurl;
                }
            }
            else
            {
                var Want = offer.Deserialize<WantModel>();
                if (Want != null)
                {
                    _id = Want.id;
                    Name = Want.name;
                    Description = Want.description;
                    Category = Want.subCategoryModelId;
                    _imgUrl = Want.imgurl;
                }
            }
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
            get { return new MvxCommand(Save); }
        }

        private async void Save()
        {
            if (!Validate())
                return;

            bool ok;
            if (_type.Contains("CanModel"))
            {
                // update can model
                CanModel can = new CanModel
                {
                    id = _id,
                    name = Name,
                    description = Description,
                    subCategoryModelId = Category,
                    imgurl = _imgUrl
                };
                ok = await _canService.UpdateCan(can);
            }
            else
            {
                // update want model
                WantModel want = new WantModel
                {
                    id = _id,
                    name = Name,
                    description = Description,
                    subCategoryModelId = Category,
                    imgurl = _imgUrl
                };
                ok = await _wantService.UpdateWant(want);
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

        public IMvxCommand DeleteCommand
        {
            get { return new MvxCommand(DoDeleteCommand); }
        }

        private void DoDeleteCommand()
        {
            _dialogService.Alert(Constants.DialogDeleteConfirm, Constants.DialogTitleDeleteConfirm,
                "OK", "ОТМЕНА", Delete);
        }

        private async void Delete()
        {
            bool ok;
            if (_type.Contains("CanModel"))
                ok = await _canService.DeleteCan(_id);
            else
                ok = await _wantService.DeleteWant(_id);
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
            _dialogService.Filter(categories, _categoryService.IndexOf(Category) + 1, s => Category = Categories[s - 1].id);
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
