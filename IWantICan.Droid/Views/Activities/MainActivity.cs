using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Support.V4.View;
using Android.Support.V4.Widget;
using Android.Views;
using Android.Views.InputMethods;
using IWantICan.Core.ViewModels;
using IWantICan.Droid.Fragments;
using MvvmCross.Droid.Shared.Caching;
using MvvmCross.Droid.Support.V7.AppCompat;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace IWantICan.Droid.Activities
{
    [Activity(
        Label = "Offers",
        Theme = "@style/AppTheme.Main",
        LaunchMode = LaunchMode.SingleTop
        )]
    public class MainActivity : MvxCachingFragmentCompatActivity<MainViewModel>
    {
        public MvxActionBarDrawerToggle drawerToggle;
        public DrawerLayout drawerLayout;

        private bool _showHamburgerMenu;
        public bool ShowHamburgerMenu
        {
            get { return _showHamburgerMenu; }
            set
            {
                _showHamburgerMenu = value;
                if (value)
                    ShowHamburguerButton();
                else
                    ShowBackButton();
            }
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_main);

            drawerLayout = FindViewById<DrawerLayout>(Resource.Id.drawer_layout);

	        SupportActionBar.Elevation = 0;
			SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            drawerToggle = new MvxActionBarDrawerToggle(
                this,                           // host Activity
                drawerLayout,                   // drawerLayout object
                //toolbar,                        // nav drawer icon to replace 'Up' caret
                Resource.String.drawer_open,    // "open drawer" description
                Resource.String.drawer_close    // "close drawer" description
            );

            drawerToggle.DrawerOpened += (sender, e) => HideSoftKeyboard();
			drawerLayout.AddDrawerListener(drawerToggle);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    if (ShowHamburgerMenu && !drawerLayout.IsDrawerOpen(GravityCompat.Start))
                        drawerLayout.OpenDrawer(GravityCompat.Start);
                    else
                        OnBackPressed();
                    return true;
            }

            return base.OnOptionsItemSelected(item);
        }

		public override void OnBeforeFragmentChanging(IMvxCachedFragmentInfo fragmentInfo, Android.Support.V4.App.FragmentTransaction transaction)
		{
			if (fragmentInfo.ViewModelType == typeof(AllOffersViewModel)
				|| fragmentInfo.ViewModelType == typeof(MyOffersViewModel))
			{
				transaction.SetCustomAnimations(
					Resource.Animation.abc_fade_in,
					Resource.Animation.slide_from_center_to_right,
					Resource.Animation.abc_fade_in,
					Resource.Animation.slide_from_center_to_right);
			}

			base.OnBeforeFragmentChanging(fragmentInfo, transaction);
		}


		private void ShowBackButton()
        {
            drawerToggle.DrawerIndicatorEnabled = false;
            //drawerLayout.SetDrawerLockMode(DrawerLayout.LockModeLockedClosed);
        }

        private void ShowHamburguerButton()
        {
            drawerToggle.DrawerIndicatorEnabled = true;
            //drawerLayout.SetDrawerLockMode(DrawerLayout.LockModeUnlocked);
        }

        public override void OnBackPressed()
        {
            if (drawerLayout != null && drawerLayout.IsDrawerOpen(GravityCompat.Start))
                drawerLayout.CloseDrawers();
            else
                base.OnBackPressed();
        }

        public void HideSoftKeyboard()
        {
            if (CurrentFocus == null) return;

            InputMethodManager inputMethodManager = (InputMethodManager)GetSystemService(InputMethodService);
            inputMethodManager.HideSoftInputFromWindow(CurrentFocus.WindowToken, 0);

            CurrentFocus.ClearFocus();
        }
    }
}