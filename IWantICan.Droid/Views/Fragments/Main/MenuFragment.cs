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
using Android.Support.V4.App;

namespace IWantICan.Droid.Fragments
{
	[MvxFragment(typeof(MainViewModel), Resource.Id.navigation_frame, false)]
	[Register("iwantican.droid.fragments.MenuFragment")]
	public class MenuFragment : MvxFragment<MenuViewModel>, NavigationView.IOnNavigationItemSelectedListener
	{
		private NavigationView _navigationView;
		//private IMenuItem _previousMenuItem;

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

			var set = this.CreateBindingSet<MenuFragment, MenuViewModel>();
			set.Bind(this).For(p => p.User).To(vm => vm.User);
			set.Apply();

			// if viewmodel's property has been updated before the bind
			// applied, try to update manually from viewmodel
			if (User == null)
			{
				User = ViewModel.User;
			}

			Activity.SupportFragmentManager.BackStackChanged += SupportFragmentManagerOnBackStackChanged;

			return view;
		}

		private void UpdateAvatar(string url)
		{
			avatarView.LoadWithPicasso(url, Resource.Drawable.avatar_placeholder);
		}

		private void SupportFragmentManagerOnBackStackChanged(object sender, EventArgs eventArgs)
		{
			var fm = sender as FragmentManager;
			
			var allOffers = fm?.FindFragmentByTag(typeof(AllOffersViewModel).FullName);
			var myOffers = fm?.FindFragmentByTag(typeof(MyOffersViewModel).FullName);

			if (allOffers != null && allOffers.IsVisible)
				_navigationView.SetCheckedItem(Resource.Id.nav_all_offers);
			else if (myOffers != null && myOffers.IsVisible)
				_navigationView.SetCheckedItem(Resource.Id.nav_my_offers);
		}

		public bool OnNavigationItemSelected(IMenuItem item)
		{
			//SelectedNavigationItem(item);

			var itemId = item.ItemId;
			Navigate(itemId);

			return true;
		}

		public bool SelectedNavigationItem(IMenuItem item)
		{
			/*if (item != _previousMenuItem)
			{
				_previousMenuItem?.SetChecked(false);
			}*/

			var itemId = item.ItemId;
			if (itemId == Resource.Id.nav_all_offers || itemId == Resource.Id.nav_my_offers)
			{
				item.SetCheckable(true);
				item.SetChecked(true);
				//_previousMenuItem = item;
			}

			return true;
		}

		private async void Navigate(int itemId)
		{
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

			((MainActivity)Activity).drawerLayout.CloseDrawers();
		}
	}
}
