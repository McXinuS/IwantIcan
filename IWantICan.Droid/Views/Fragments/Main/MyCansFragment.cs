using Android.Runtime;
using IWantICan.Core.ViewModels;
using MvvmCross.Droid.Shared.Attributes;

namespace IWantICan.Droid.Fragments
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.main_frame, true)]
    [Register("iwantican.droid.fragments.MyCansFragment")]
    public class MyCansFragment : BaseOfferMyFragment<MyCansViewModel>
    {
        protected override int FragmentId => Resource.Layout.fragment_cans_my;
    }
}
