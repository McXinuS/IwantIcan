using Android.App;
using Android.OS;
using Android.Views;
using IWantICan.Core.Models;
using IWantICan.Core.ViewModels;
using IWantICan.Droid.Helpers;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.AppCompat;
using Refractored.Controls;

namespace IWantICan.Droid.Activities
{
    [Activity(
        Label = "@string/toolbar_title_change_profile",
        Theme = "@style/AppTheme.ProfileMy",
		WindowSoftInputMode = SoftInput.StateAlwaysHidden
        )]
    public class MyProfileActivity : MvxAppCompatActivity<MyProfileViewModel>
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

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
			SupportActionBar.SetDisplayHomeAsUpEnabled(true);
			SetContentView(Resource.Layout.activity_my_profile);

            avatarView = FindViewById<CircleImageView>(Resource.Id.avatar);
			
            if (ViewModel.User != null)
                // if viewmodel's property has been updated before the bind
                // can be applied then update from viewmodel's property
                UpdateAvatar(ViewModel.User?.avatar);
            else
            {
                var set = this.CreateBindingSet<MyProfileActivity, MyProfileViewModel>();
                set.Bind(this).For(p => p.User).To(vm => vm.User);
                set.Apply();
            }
        }

        private void UpdateAvatar(string url)
        {
            avatarView.LoadWithPicasso(url, Resource.Drawable.avatar_placeholder);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Xml.menu_profile, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    OnBackPressed();
                    return true;
                case Resource.Id.action_apply:
                    ViewModel.SaveCommand.Execute();
                    return true;
            }
            return base.OnOptionsItemSelected(item);
		}
	}
}