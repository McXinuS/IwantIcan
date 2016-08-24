using Android.App;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using IWantICan.Core.ViewModels;
using IWantICan.Droid.Utilities;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Support.V7.RecyclerView;

namespace IWantICan.Droid.Fragments
{
    public abstract class BaseOfferFragment : BaseMainFragment
    {
        protected MvxRecyclerView RecyclerView;

		protected abstract string EmptyListMessage { get; }

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            ShowHamburgerMenuButton = true;

            RecyclerView = view.FindViewById<MvxRecyclerView>(Resource.Id.my_recycler_view);

            if (RecyclerView != null)
            {
                RecyclerView.HasFixedSize = true;
                var layoutManager = new LinearLayoutManager(Activity);
                RecyclerView.SetLayoutManager(layoutManager);
                RecyclerView.AddItemDecoration(new SimpleDividerItemDecoration(Application.Context));
            }

			if (!string.IsNullOrWhiteSpace(EmptyListMessage) && RecyclerView != null)
			{
				var tb = view.FindViewById<TextView>(Resource.Id.empty_view);
				tb.Text = EmptyListMessage;
			}

			// TODO
            /*
            var swipeToRefresh = view.FindViewById<MvxSwipeRefreshLayout>(Resource.Id.refresher);
            var appBar = Activity.FindViewById<AppBarLayout>(Resource.Id.appbar);
            if (appBar != null)
            {
                appBar.OffsetChanged += (sender, args) => swipeToRefresh.Enabled = args.VerticalOffset == 0;
            }
			*/

            return view;
        }

	    public override void OnDestroyView()
	    {
			(ViewModel as BaseOfferViewModel)?.MessengerUnsibscribeCommand.Execute(null);
		    base.OnDestroyView();
	    }
    }

    public abstract class BaseOfferFragment<TViewModel> : BaseOfferFragment where TViewModel : class, IMvxViewModel
    {
        public new TViewModel ViewModel
        {
            get { return (TViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
    }
}