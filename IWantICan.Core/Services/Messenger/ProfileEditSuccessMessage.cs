using MvvmCross.Plugins.Messenger;

namespace IWantICan.Core.Services.Messenger
{
	public class ProfileEditSuccessMessage : MvxMessage
	{
		public ProfileEditSuccessMessage(object sender)
			: base(sender)
		{
		}
	}
}
