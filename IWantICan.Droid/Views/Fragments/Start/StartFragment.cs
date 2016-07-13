using Android.Runtime;
using IWantICan.Core.ViewModels;
using MvvmCross.Droid.Shared.Attributes;

namespace IWantICan.Droid.Fragments
{
    [MvxFragment(typeof(StartContainerViewModel), Resource.Id.start_frame, true)]
    [Register("iwantican.droid.fragments.StartFragment")]
    public class StartFragment : BaseStartFragment<StartViewModel>
    {
        protected override int FragmentId => Resource.Layout.fragment_start;
    }
}