using Android.Runtime;
using IWantICan.Core.ViewModels;
using MvvmCross.Droid.Shared.Attributes;

namespace IWantICan.Droid.Fragments
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.main_frame, true)]
    [Register("iwantican.droid.fragments.AllCansFragment")]
    public class AllCansFragment : BaseOfferFragment<AllCansViewModel>
    {
        protected override int FragmentId => Resource.Layout.fragment_offers;
		protected override string EmptyListMessage => Context.Resources.GetString(Resource.String.activity_their_empty_can);
	}
}
