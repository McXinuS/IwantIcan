using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using IWantICan.Core.ViewModels;
using MvvmCross.Droid.Shared.Attributes;

namespace IWantICan.Droid.Fragments
{
    [MvxFragment(typeof(OfferItemContainerViewModel), Resource.Id.main_frame, true)]
    [Register("iwantican.droid.fragments.OffererProfileFragment")]
    public class OffererProfileFragment : BaseOfferItemFragment<OffererProfileViewModel>
	{
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var view = base.OnCreateView(inflater, container, savedInstanceState);

			var backArrow = view.FindViewById<ImageView>(Resource.Id.backArrow);
			backArrow.Click += (sender, args) => { Activity.OnBackPressed(); };

			// TODO intent web/phone/mailto action on corresponding layout click

			return view;
		}

		protected override int FragmentId => Resource.Layout.fragment_offerer_profile;
    }
}
