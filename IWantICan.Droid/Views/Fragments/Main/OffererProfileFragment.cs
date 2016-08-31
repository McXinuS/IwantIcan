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
using Android.Support.Design.Widget;
using Android.Transitions;
using Android.Util;
using MvvmCross.Binding.Droid.BindingContext;

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
            contactsRecView.HasFixedSize = true;
            var layoutManager = new LinearLayoutManager(Activity);
            contactsRecView.SetLayoutManager(layoutManager);
            contactsRecView.Adapter = new MvxRecyclerViewContactsAdapter(Activity, BindingContext as IMvxAndroidBindingContext);

            // screen size
            var display = Activity.WindowManager.DefaultDisplay;
            var size = new Point();
            display.GetSize(size);
            var width = size.X;
            var height = size.Y;

            var offersElv = view.FindViewById<MvxExpandableListView>(Resource.Id.offers);
            // move indicators to the right
            offersElv.SetIndicatorBoundsRelative(width - GetPixelFromDips(45), width - GetPixelFromDips(15));

            var appbar = view.FindViewById<AppBarLayout>(Resource.Id.appbar);
            CoordinatorLayout.LayoutParams lp = (CoordinatorLayout.LayoutParams)appbar.LayoutParameters;
            lp.Height = (int)(height / 2.5);

            SharedElementEnterTransition = new DetailsTransition();
            //EnterTransition = new Fade();
            //ExitTransition = new Fade();
            SharedElementReturnTransition = new DetailsTransition();

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

        public class DetailsTransition : TransitionSet
        {
            public DetailsTransition()
            {
                SetOrdering(TransitionOrdering.Together);
                AddTransition(new ChangeBounds()).
                    AddTransition(new ChangeTransform()).
                    AddTransition(new ChangeImageTransform());
            }
        }
        protected override int FragmentId => Resource.Layout.fragment_offerer_profile;
    }
}
