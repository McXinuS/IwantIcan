using MvvmCross.Plugins.Messenger;

namespace IWantICan.Core.Services.Messenger
{
    public class MessengerService : IMessengerService
    {
        private IMvxMessenger _messenger;

        public MessengerService(IMvxMessenger messenger)
        {
            _messenger = messenger;
        }

		public void SendOfferActionMessage(object sender, MessengerOfferActionType type)
		{
			_messenger.Publish(new OfferActionMessage(sender, type));
		}

		public void SendProfileEditSuccessMessage(object sender)
        {
            _messenger.Publish(new ProfileEditSuccessMessage(sender));
        }
    }
}
