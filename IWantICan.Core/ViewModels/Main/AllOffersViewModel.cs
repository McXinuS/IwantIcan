using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IWantICan.Core.Interfaces;
using IWantICan.Core.Models;
using IWantICan.Core.Services;
using IWantICan.Core.Services.Messenger;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace IWantICan.Core.ViewModels
{
    public class AllOffersViewModel : BaseViewModel
    {
        ICategoryService _categoryService;
        IDialogService _dialogService = Mvx.Resolve<IDialogService>();
        IMessengerService _messenger;

        public AllOffersViewModel(ICategoryService categoryService,
            IDialogService dialogService,
            IMessengerService messenger)
        {
            _categoryService = categoryService;
            _dialogService = dialogService;
			_messenger = messenger;
        }

        public List<CategoryModel> Categories
        {
            get { return _categoryService.GetCategoryList(); }
        }
        
        public int[] FilterSelection
        {
            get { return _categoryService.Selected; }
            set { _categoryService.Selected = value; }
        }

        public IMvxCommand ShowFilterCommand
        {
            get { return new MvxCommand(ShowFilter); }
        }

        private void ShowFilter()
        {
            if (Categories == null)
                return;
            var categories = Categories.Select(c => c.name).ToArray();
            _dialogService.Filter(categories, IndexesFromSelection(FilterSelection), SelectionFromIndex);
        }

        private int[] IndexesFromSelection(int[] sel)
        {
            var indexes = new int[sel.Length];

            for (var i = 0; i < sel.Length; i++)
                indexes[i] = _categoryService.IndexOf(sel[i])+1;

            return indexes;
        }

        private int[] SelectionFromIndex(int[] indexes)
        {
            FilterSelection = new int[indexes.Length];

            for (var i = 0; i < indexes.Length; i++)
                FilterSelection[i] = Categories[indexes[i]-1].id;

			_messenger.SendOfferActionMessage(this, MessengerOfferActionType.FilterDone);

            return FilterSelection;
        }
    }
}