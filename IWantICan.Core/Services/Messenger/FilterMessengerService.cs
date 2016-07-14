using MvvmCross.Plugins.Messenger;

namespace IWantICan.Core.Services
{
    public class FilterMessengerService : IFilterMessengerService
    {
        private IMvxMessenger _messenger;

        public FilterMessengerService(IMvxMessenger messenger)
        {
            _messenger = messenger;
        }

        public void SendFilterDoneMessage(object sender)
        {
            _messenger.Publish(new FilterDoneMessage(sender));
        }

        public void SendOfferActionSuccessMessage(object sender)
        {
            _messenger.Publish(new OfferActionSuccessMessage(sender));
        }
    }
}
