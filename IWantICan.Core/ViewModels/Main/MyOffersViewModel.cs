using MvvmCross.Core.ViewModels;

namespace IWantICan.Core.ViewModels
{
    public class MyOffersViewModel : BaseViewModel
    {
        public IMvxCommand CreateOfferCommand
        {
            get { return new MvxCommand(CreateOffer); }
        }

        private void CreateOffer()
        {
            ShowViewModel<CreateOfferViewModel>(new { type = SelectedPage });
        }

        public int SelectedPage { get; set; }
    }
}
