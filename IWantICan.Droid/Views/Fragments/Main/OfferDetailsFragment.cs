using System;
using System.Collections.Generic;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using IWantICan.Core.Models;
using IWantICan.Core.ViewModels;
using IWantICan.Droid.Helpers;
using IWantICan.Droid.Utilities;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;
using Refractored.Controls;

namespace IWantICan.Droid.Fragments
{
	[MvxFragment(typeof(OfferDetailsContainerViewModel), Resource.Id.main_frame, true)]
	[Register("iwantican.droid.fragments.OfferDetailsFragment")]
	public class OfferDetailsFragment : BaseOfferDetailsFragment<OfferDetailsViewModel>
	{
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
			var view = base.OnCreateView(inflater, container, savedInstanceState);
			
			var contactsFab = view.FindViewById<FloatingActionButton>(Resource.Id.goMessage);
			contactsFab.Click += ShowContactDialog;

			avatarView = view.FindViewById<CircleImageView>(Resource.Id.profileAvatar);
			if (ViewModel.User != null)
				// if viewmodel's property has been updated before the bind
				// can be applied then update from viewmodel's property
				UpdateAvatar(ViewModel.User?.avatar);
			else
			{
				var set = this.CreateBindingSet<OfferDetailsFragment, OfferDetailsViewModel>();
				set.Bind(this).For(p => p.User).To(vm => vm.User);
				set.Apply();
			}

            // screen size
            var display = Activity.WindowManager.DefaultDisplay;
            var size = new Point();
            display.GetSize(size);
            var height = size.Y;

            var appbar = view.FindViewById<AppBarLayout>(Resource.Id.appbar);
            CoordinatorLayout.LayoutParams lp = (CoordinatorLayout.LayoutParams)appbar.LayoutParameters;
            lp.Height = (int)(height / 2.5);

            return view;
		}

		private void UpdateAvatar(string url)
		{
			avatarView.LoadWithPicasso(url, Resource.Drawable.avatar_placeholder);
		}

		/// <summary>
		/// Shows a dialog with contact information of the user.
		/// </summary>
		private void ShowContactDialog(object sender, EventArgs eventArgs)
		{
			if (ViewModel.Contacts == null)
				return;
			
			var adb = new Dialog(Activity);
			// adb.SetTitle($"Связаться с {user.name} {user.surname}"); // layout width issue
			adb.SetTitle("Связаться");
			adb.SetCancelable(true);
			adb.SetContentView(Resource.Layout.dialog_contact);

			var contactsRecycler = (MvxRecyclerView)adb.FindViewById(Resource.Id.contactsRecycler);
			contactsRecycler.Adapter = new MvxRecyclerViewContactsAdapter(Activity, BindingContext as IMvxAndroidBindingContext);
			var set = this.CreateBindingSet<OfferDetailsFragment, OfferDetailsViewModel>();
			set.Bind(contactsRecycler).For(p => p.ItemsSource).To(vm => vm.Contacts);
			set.Apply();

			// close button
			((Button)adb.FindViewById(Resource.Id.closeButton)).Click += (s, a) => { adb.Dismiss(); };

			adb.Show();
		}

		protected override int FragmentId => Resource.Layout.fragment_offer_details;
	}
}
