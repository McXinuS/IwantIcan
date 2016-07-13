using System.Threading.Tasks;
using System.Windows.Input;
using Android.Widget;
using MvvmCross.Core.ViewModels;
using IWantICan.Core.Helpers;
using IWantICan.Core.Interfaces;
using IWantICan.Core.Services;
using MvvmCross.Platform;

namespace IWantICan.Core.ViewModels
{
    public abstract class BaseOfferViewModel : BaseViewModel
    {
        ICategoryService _categoryService;
        protected int[] SelectedCategory { get { return _categoryService.Selected; } }

        private bool _isRefreshing;
        private bool _isEmpty;

        protected BaseOfferViewModel()
        {
            IsEmpty = true;
            _categoryService = Mvx.Resolve<ICategoryService>();
        }

        protected abstract void LoadData();
        public ICommand ReloadCommand
        {
            get
            {
                return new MvxCommand(() =>
                {
                    Task t = new Task(LoadData);
                    t.Start();
                });
            }
        }
		
        protected void GoDetails<T>(T item)
        {
			// bad offer
	        if (item == null)
	        {
				Mvx.Resolve<IDialogService>().Alert("Произошла внутренняя ошибка. Повторите запрос позже", "Ошибка", "ОК");
		        return;
	        }

            ShowViewModel<OfferItemContainerViewModel>(new
            {
                offer = item.Serialize(),
                type = typeof(T).Name
            });
        }

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                RaisePropertyChanged(() => IsRefreshing);
            }
        }

        public bool IsEmpty
        {
            get { return _isEmpty; }
            set
            {
                _isEmpty = value;
                RaisePropertyChanged(() => IsEmpty);
            }
        }
    }
}
