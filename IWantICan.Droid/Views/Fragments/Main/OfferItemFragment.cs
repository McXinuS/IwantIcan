using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using IWantICan.Core.Models;
using IWantICan.Core.ViewModels;
using IWantICan.Droid.Helpers;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Shared.Attributes;
using Refractored.Controls;
using Square.Picasso;

namespace IWantICan.Droid.Fragments
{
	[MvxFragment(typeof(OfferItemContainerViewModel), Resource.Id.main_frame, true)]
	[Register("iwantican.droid.fragments.OfferItemFragment")]
	public class OfferItemFragment : BaseOfferItemFragment<OfferItemViewModel>
	{
		private CircleImageView avatarView;

		private UserModel _user;
		public UserModel User
		{
			get { return _user; }
			set
			{
				_user = value;
				UpdateAvatar(User?.avatar);
			}
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = base.OnCreateView(inflater, container, savedInstanceState);

			var backArrow = view.FindViewById<ImageView>(Resource.Id.backArrow);
			backArrow.Click += (sender, args) => { Activity.OnBackPressed(); };

			avatarView = view.FindViewById<CircleImageView>(Resource.Id.profileAvatar);
            
			if (ViewModel.User != null)
				// if viewmodel's property has been updated before the bind
				// can be applied then update from viewmodel's property
				UpdateAvatar(ViewModel.User?.avatar);
			else
			{
				var set = this.CreateBindingSet<OfferItemFragment, OfferItemViewModel>();
				set.Bind(this).For(p => p.User).To(vm => vm.User);
				set.Apply();
			}

			return view;
		}

		private void UpdateAvatar(string url)
		{
			avatarView.LoadWithPicasso(url, Resource.Drawable.avatar_placeholder);
		}

		protected override int FragmentId => Resource.Layout.fragment_offer_details;
	}
}
