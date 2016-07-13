namespace IWantICan.Core.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            ShowViewModel<AllOffersViewModel>();
            ShowViewModel<MenuViewModel>();
        }
    }
}
