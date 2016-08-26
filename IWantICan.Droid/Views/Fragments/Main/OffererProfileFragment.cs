using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using IWantICan.Core.ViewModels;
using IWantICan.Droid.Utilities;
using MvvmCross.Binding.Droid.Views;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V7.RecyclerView;

namespace IWantICan.Droid.Fragments
{
    [MvxFragment(typeof(OfferDetailsContainerViewModel), Resource.Id.main_frame, true)]
    [Register("iwantican.droid.fragments.OffererProfileFragment")]
    public class OffererProfileFragment : BaseOfferDetailsFragment<OffererProfileViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

			var contactsRecView = view.FindViewById<MvxRecyclerView>(Resource.Id.contactsRecycler);
			//contactsRecView.Click += ContactsRecViewOnClick;
			contactsRecView.HasFixedSize = true;
			var layoutManager = new LinearLayoutManager(Activity);
			contactsRecView.SetLayoutManager(layoutManager);
			
			return view;
        }

	    private void ContactsRecViewOnClick(object sender, EventArgs eventArgs)
	    {
		    throw new NotImplementedException();
	    }

	    protected override int FragmentId => Resource.Layout.fragment_offerer_profile;
    }
}
