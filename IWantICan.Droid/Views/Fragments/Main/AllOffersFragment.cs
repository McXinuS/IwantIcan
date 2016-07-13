using System.Collections.Generic;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Android.Views;
using IWantICan.Core.ViewModels;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;

namespace IWantICan.Droid.Fragments
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.main_frame, true)]
    [Register("iwantican.droid.fragments.OffersFragment")]
    public class OffersFragment : BaseMainFragment<AllOffersViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            ShowHamburgerMenuButton = true;

            Activity.Title = Resources.GetText(Resource.String.toolbar_title_all);

            var viewPager = view.FindViewById<ViewPager>(Resource.Id.viewpager);
            if (viewPager != null)
            {
                var fragments = new List<MvxCachingFragmentStatePagerAdapter.FragmentInfo>
                {
                    new MvxCachingFragmentStatePagerAdapter.FragmentInfo(
                        Resources.GetText(Resource.String.toolbar_tab_they_can),
                        "can",
                        typeof(AllCansFragment),
                        typeof(AllCansViewModel)),
                    new MvxCachingFragmentStatePagerAdapter.FragmentInfo(
                        Resources.GetText(Resource.String.toolbar_tab_they_want),
                        "want",
                        typeof(AllWantsFragment),
                        typeof(AllWantsViewModel))
                };

                viewPager.Adapter = new MvxCachingFragmentStatePagerAdapter(Activity, ChildFragmentManager, fragments);
            }

            var tabLayout = view.FindViewById<TabLayout>(Resource.Id.tabs);
            tabLayout.SetupWithViewPager(viewPager);

            HasOptionsMenu = true;

            return view;
        }

        protected override int FragmentId => Resource.Layout.fragment_offers;

        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Xml.menu_offers, menu);
            base.OnCreateOptionsMenu(menu, inflater);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_filter:
                    ViewModel.ShowFilterCommand.Execute();
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}
