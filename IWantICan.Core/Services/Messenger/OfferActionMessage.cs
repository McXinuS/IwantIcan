using MvvmCross.Plugins.Messenger;

namespace IWantICan.Core.Services.Messenger
{
	public class OfferActionMessage : MvxMessage
	{
		public MessengerOfferActionType Type { get; set; }

		public OfferActionMessage(object sender, MessengerOfferActionType type)
			: base(sender)
		{
			Type = type;
		}
	}
}