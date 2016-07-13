using Android.Runtime;
using IWantICan.Core.ViewModels;
using MvvmCross.Droid.Shared.Attributes;

namespace IWantICan.Droid.Fragments
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.main_frame, true)]
    [Register("iwantican.droid.fragments.MyWantsFragment")]
    public class MyWantsFragment : BaseOfferMyFragment<MyWantsViewModel>
    {
        protected override int FragmentId => Resource.Layout.fragment_wants_my;
    }
}
