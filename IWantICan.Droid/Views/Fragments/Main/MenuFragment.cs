using System;
using System.Threading.Tasks;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using IWantICan.Core.Models;
using IWantICan.Core.Services;
using IWantICan.Core.ViewModels;
using IWantICan.Droid.Activities;
using IWantICan.Droid.Helpers;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V4;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Binding.BindingContext;
using Refractored.Controls;

namespace IWantICan.Droid.Fragments
{
    [MvxFragment(typeof(MainViewModel), Resource.Id.navigation_frame)]
    [Register("iwantican.droid.fragments.MenuFragment")]
    public class MenuFragment : MvxFragment<MenuViewModel>, NavigationView.IOnNavigationItemSelectedListener
    {
        private NavigationView _navigationView;
        private IMenuItem _previousMenuItem;

		private CircleImageView avatarView;

		private UserModel _user;
		public UserModel User
		{
			get { return _user; }
			set
			{
				_user = value;
				UpdateAvatar(User?.avatar);
			}
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            var view = this.BindingInflate(Resource.Layout.fragment_navigation, null);

            _navigationView = view.FindViewById<NavigationView>(Resource.Id.navigation_view);
            _navigationView.SetNavigationItemSelectedListener(this);
            //_navigationView.Menu.FindItem(Resource.Id.nav_all_offers).SetChecked(true);

            avatarView = _navigationView.GetHeaderView(0).FindViewById<CircleImageView>(Resource.Id.avatar);
            
			if (ViewModel.User != null)
				// if viewmodel's property has been updated before the bind
				// can be applied then update from viewmodel's property
				UpdateAvatar(ViewModel.User?.avatar);
			else
			{
				var set = this.CreateBindingSet<MenuFragment, MenuViewModel>();
				set.Bind(this).For(p => p.User).To(vm => vm.User);
				set.Apply();
			}

			return view;
        }

        public override void OnResume()
        {
            ViewModel.ReloadUserCommand.Execute();
            base.OnResume();
        }

        private void UpdateAvatar(string url)
		{
			avatarView.LoadWithPicasso(url, Resource.Drawable.avatar_placeholder);
		}

		public bool OnNavigationItemSelected(IMenuItem item)
        {
            if (item != _previousMenuItem)
            {
                _previousMenuItem?.SetChecked(false);
            }

            item.SetCheckable(true);
            item.SetChecked(true);

            _previousMenuItem = item;

            Navigate(item.ItemId);

            return true;
        }

        private async void Navigate(int itemId)
        {
            ((MainActivity)Activity).drawerLayout.CloseDrawers();

            // add a small delay to prevent any UI issues
            await Task.Delay(TimeSpan.FromMilliseconds(250));

            switch (itemId)
            {
                case Resource.Id.nav_all_offers:
                    ViewModel.ShowAllOffersCommand.Execute();
                    break;
                case Resource.Id.nav_my_offers:
                    ViewModel.ShowMyOffersCommand.Execute();
                    break;
                case Resource.Id.nav_profile:
                    ViewModel.ShowMyProfileCommand.Execute();
                    break;
                case Resource.Id.nav_logout:
                    ViewModel.LogoutCommand.Execute();
                    break;
            }
        }
    }
}
