using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using IWantICan.Core.ViewModels;
using MvvmCross.Droid.Shared.Caching;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace IWantICan.Droid.Activities
{
    [Activity(
        Label = "Login",
        Theme = "@style/AppTheme.Login",
        LaunchMode = LaunchMode.SingleTop,
		WindowSoftInputMode = SoftInput.StateAlwaysHidden,
		NoHistory = true
        )]
    public class StartContainerActivity : MvxCachingFragmentCompatActivity<StartContainerViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_start);
        }

		public override void OnBeforeFragmentChanging(IMvxCachedFragmentInfo fragmentInfo, Android.Support.V4.App.FragmentTransaction transaction)
		{
			if (fragmentInfo.ViewModelType == typeof (SignupViewModel))
			{
				SupportActionBar.Show();

				transaction.SetCustomAnimations(
					Resource.Animation.abc_grow_fade_in_from_bottom,
					Resource.Animation.abc_shrink_fade_out_from_bottom,
					Resource.Animation.abc_grow_fade_in_from_bottom,
					Resource.Animation.abc_shrink_fade_out_from_bottom)
					.AddToBackStack(null);
			}
			else
			{
				SupportActionBar.Hide();
			}

			base.OnBeforeFragmentChanging(fragmentInfo, transaction);
		}


	}
}