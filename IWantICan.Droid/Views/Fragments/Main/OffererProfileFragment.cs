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
using Android.Graphics;

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

            var offersElv = view.FindViewById<MvxExpandableListView>(Resource.Id.offers);
			var display = Activity.WindowManager.DefaultDisplay;
			var size = new Point();
			display.GetSize(size);
			var width = size.X;
			offersElv.SetIndicatorBoundsRelative(width - GetPixelFromDips(45), width - GetPixelFromDips(15));
            offersElv.NestedScrollingEnabled = true;
            //offersElv.Focusable = false;

            return view;
        }

		/// <summary>
		///  Convert the dps to pixels, based on density scale
		/// </summary>
		public int GetPixelFromDips(float pixels)
		{
			var scale = Resources.DisplayMetrics.Density;
			return (int)(pixels * scale + 0.5f);
		}

		private void ContactsRecViewOnClick(object sender, EventArgs eventArgs)
	    {
		    throw new NotImplementedException();
	    }

	    protected override int FragmentId => Resource.Layout.fragment_offerer_profile;
    }
}
