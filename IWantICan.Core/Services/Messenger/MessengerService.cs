using MvvmCross.Plugins.Messenger;

namespace IWantICan.Core.Services
{
    public class MessengerService : IMessengerService
    {
        private IMvxMessenger _messenger;

        public MessengerService(IMvxMessenger messenger)
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

        public void SendProfileEditSuccessMessage(object sender)
        {
            _messenger.Publish(new ProfileEditSuccessMessage(sender));
        }
    }
}
