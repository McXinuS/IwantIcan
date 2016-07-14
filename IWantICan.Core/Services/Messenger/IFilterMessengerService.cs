namespace IWantICan.Core.Services
{
    public interface IFilterMessengerService
    {
        void SendFilterDoneMessage(object sender);
        void SendOfferActionSuccessMessage(object sender);
    }
}
