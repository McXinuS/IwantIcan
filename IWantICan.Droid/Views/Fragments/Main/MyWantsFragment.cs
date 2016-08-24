using Android.Runtime;
using IWantICan.Core.ViewModels;
using MvvmCross.Droid.Shared.Attributes;

namespace IWantICan.Droid.Fragments
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.main_frame, true)]
    [Register("iwantican.droid.fragments.MyWantsFragment")]
    public class MyWantsFragment : BaseOfferMyFragment<MyWantsViewModel>
    {
        protected override int FragmentId => Resource.Layout.fragment_offers_my;
		protected override string EmptyListMessage => Context.Resources.GetString(Resource.String.activity_my_empty_want);
	}
}
