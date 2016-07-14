using MvvmCross.Plugins.Messenger;

namespace IWantICan.Core.Services
{
    public class FilterDoneMessage : MvxMessage
    {
        public FilterDoneMessage(object sender)
            : base(sender)
        {
        }
    }
}
