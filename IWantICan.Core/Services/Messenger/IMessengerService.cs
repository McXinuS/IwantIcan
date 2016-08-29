namespace IWantICan.Core.Services.Messenger
{
	public interface IMessengerService
	{
		void SendOfferActionMessage(object sender, MessengerOfferActionType type);
		void SendProfileEditSuccessMessage(object sender);
	}
}