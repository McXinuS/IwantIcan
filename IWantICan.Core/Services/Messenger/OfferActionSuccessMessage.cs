using MvvmCross.Plugins.Messenger;

namespace IWantICan.Core.Services
{
    public class OfferActionSuccessMessage : MvxMessage
    {
        public OfferActionSuccessMessage(object sender)
            : base(sender)
        {
        }
    }
}
