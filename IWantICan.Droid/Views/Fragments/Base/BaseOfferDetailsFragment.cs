using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using IWantICan.Droid.Activities;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Support.V4;

namespace IWantICan.Droid.Fragments
{
    public abstract class BaseOfferDetailsFragment : MvxFragment
    {
        protected BaseOfferDetailsFragment()
        {
            RetainInstance = true;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

			var view = this.BindingInflate(FragmentId, null);

			var toolbar = (Toolbar)view.FindViewById(Resource.Id.toolbar);
	        var mainActivity = Activity as OfferDetailsContainerActivity;

	        if (toolbar != null)
	        {
				mainActivity.SetSupportActionBar(toolbar);
				mainActivity.SupportActionBar.SetDisplayHomeAsUpEnabled(true);
	        }

			return view;
        }

        protected abstract int FragmentId { get; }
    }

    public abstract class BaseOfferDetailsFragment<TViewModel> : BaseOfferDetailsFragment where TViewModel : class, IMvxViewModel
    {
        public new TViewModel ViewModel
        {
            get { return (TViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
    }
}

