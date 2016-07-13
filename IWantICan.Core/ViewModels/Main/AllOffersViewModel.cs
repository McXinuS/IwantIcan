using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IWantICan.Core.Interfaces;
using IWantICan.Core.Models;
using IWantICan.Core.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace IWantICan.Core.ViewModels
{
    public class AllOffersViewModel : BaseViewModel
    {
        ICategoryService _categoryService;
        IDialogService _dialogService = Mvx.Resolve<IDialogService>();


        private List<CategoryModel> _categories;

        public AllOffersViewModel(ICategoryService dategoryService, IDialogService dialogService)
        {
            _categoryService = dategoryService;
            _dialogService = dialogService;

            Task t = new Task(LoadCategories);
            t.Start();
        }

        async void LoadCategories()
        {
            Categories = _categoryService.GetCategoryList();
        }

        public List<CategoryModel> Categories
        {
            get { return _categories; }
            set { _categories = value; RaisePropertyChanged(() => Categories); }
        }

        // TODO tell viewmodels to refresh
        public int[] FilterSelection
        {
            get { return _categoryService.Selected; }
            set { _categoryService.Selected = value; }
        }

        public IMvxCommand ShowFilterCommand
        {
            get { return new MvxCommand(ShowFilter); }
        }

        private async void ShowFilter()
        {
            var categories = Categories.Select(c => c.name).ToArray();
            _dialogService.Filter(categories, FilterSelection, s => FilterSelection = s);
        }
    }
}