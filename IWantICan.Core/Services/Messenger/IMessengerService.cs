namespace IWantICan.Core.Services
{
    public interface IMessengerService
    {
        void SendFilterDoneMessage(object sender);
        void SendOfferActionSuccessMessage(object sender);
        void SendProfileEditSuccessMessage(object sender);
    }
}
