namespace IWantICan.Core.ViewModels
{
    public class OfferDetailsContainerViewModel : BaseViewModel
    {
        public void Init(string offer, string type)
        {
            ShowViewModel<OfferDetailsViewModel>(new {offer, type});
        }
    }
}
