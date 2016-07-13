namespace IWantICan.Core.ViewModels
{
    public class OfferItemContainerViewModel : BaseViewModel
    {
        public void Init(string offer, string type)
        {
            ShowViewModel<OfferItemViewModel>(new {offer, type});
        }
    }
}
