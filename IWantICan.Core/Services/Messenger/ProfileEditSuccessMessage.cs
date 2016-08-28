using MvvmCross.Plugins.Messenger;

namespace IWantICan.Core.Services
{
	public class ProfileEditSuccessMessage : MvxMessage
	{
		public ProfileEditSuccessMessage(object sender)
			: base(sender)
		{
		}
	}
}
