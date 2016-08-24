using System.Collections.Generic;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Views;
using Android.Widget;
using IWantICan.Core.Models;
using IWantICan.Core.ViewModels;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;

namespace IWantICan.Droid.Fragments
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.main_frame, true)]
    [Register("iwantican.droid.fragments.MyOffersFragment")]
    public class MyOffersFragment : BaseMainFragment<MyOffersViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            ShowHamburgerMenuButton = true;

            Activity.Title = Resources.GetText(Resource.String.toolbar_title_my);

            var viewPager = view.FindViewById<ViewPager>(Resource.Id.viewpager);
            if (viewPager != null)
            {
                var fragments = new List<MvxCachingFragmentStatePagerAdapter.FragmentInfo>
                {
                    new MvxCachingFragmentStatePagerAdapter.FragmentInfo(
                        Resources.GetText(Resource.String.toolbar_tab_I_can),
                        "can",
                        typeof(MyCansFragment),
                        typeof(MyCansViewModel)),
                    new MvxCachingFragmentStatePagerAdapter.FragmentInfo(
                        Resources.GetText(Resource.String.toolbar_tab_i_want),
                        "want",
                        typeof(MyWantsFragment),
                        typeof(MyWantsViewModel))
                };

                viewPager.Adapter = new MvxCachingFragmentStatePagerAdapter(Activity, ChildFragmentManager, fragments);
                viewPager.PageSelected += (sender, args) =>
                        ViewModel.SelectedPage = args.Position;
            }

            var tabLayout = view.FindViewById<TabLayout>(Resource.Id.tabs);
            tabLayout.SetupWithViewPager(viewPager);

            return view;
        }

        protected override int FragmentId => Resource.Layout.fragment_viewpager_offers_my;
    }
}