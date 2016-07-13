using Android.App;
using Android.Content.PM;
using Android.OS;
using IWantICan.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace IWantICan.Droid.Activities
{
    [Activity(
        Label = "Login",
        Theme = "@style/AppTheme",
        Name = "iwantican.droid.activities.OfferItemContainerActivity",
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize
        )]
    public class OfferItemContainerActivity : MvxCachingFragmentCompatActivity<OfferItemContainerViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_offer_item);
        }
    }
}
