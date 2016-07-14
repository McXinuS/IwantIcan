using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Support.V7.Widget;
using Android.Views;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Support.V7.RecyclerView;

namespace IWantICan.Droid.Fragments
{
    public abstract class BaseOfferFragment : BaseMainFragment
    {
        protected MvxRecyclerView RecyclerView;

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

            // TODO find an answer to why it has been here? :D
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

        public class SimpleDividerItemDecoration : RecyclerView.ItemDecoration
        {
            private Drawable mDivider;

            public SimpleDividerItemDecoration(Context context)
            {
                mDivider = context.Resources.GetDrawable(Resource.Drawable.line_divider);
            }
            
            public override void OnDrawOver(Canvas c, RecyclerView parent, RecyclerView.State state)
            {
                int left = parent.PaddingLeft;
                int right = parent.Width - parent.PaddingRight;

                int childCount = parent.ChildCount;
                for (int i = 0; i < childCount; i++)
                {
                    View child = parent.GetChildAt(i);

                    Android.Support.V7.Widget.RecyclerView.LayoutParams lParams = (RecyclerView.LayoutParams)child.LayoutParameters;

                    int top = child.Bottom + lParams.BottomMargin;
                    int bottom = top + mDivider.IntrinsicHeight;

                    mDivider.SetBounds(left, top, right, bottom);
                    mDivider.Draw(c);
                }
            }
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