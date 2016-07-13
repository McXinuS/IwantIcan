using Android.App;
using Android.Content.PM;
using Android.OS;
using IWantICan.Core.Services;
using IWantICan.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platform;

namespace IWantICan.Droid.Activities
{
    [Activity(
        Label = "Login",
        Theme = "@style/AppTheme",
        LaunchMode = LaunchMode.SingleTop,
        Name = "iwantican.droid.activities.StartContainerActivity",
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize,
        NoHistory = true
        )]
    public class StartContainerActivity : MvxCachingFragmentCompatActivity<StartContainerViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_start);
        }
    }
}