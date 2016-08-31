using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Support.V4.View;
using Android.Views;
using IWantICan.Core.ViewModels;
using MvvmCross.Droid.Shared.Caching;
using MvvmCross.Droid.Support.V7.AppCompat;
using Refractored.Controls;

namespace IWantICan.Droid.Activities
{
    [Activity(
        Label = "",
        Theme = "@style/AppTheme.OfferDetails"
        )]
    public class OfferDetailsContainerActivity : MvxCachingFragmentCompatActivity<OfferDetailsContainerViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.activity_offer_details);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    OnBackPressed();
                    break;
            }
            return base.OnOptionsItemSelected(item);
        }

        public override void OnBeforeFragmentChanging(IMvxCachedFragmentInfo fragmentInfo, Android.Support.V4.App.FragmentTransaction transaction)
        {
            if (fragmentInfo.ViewModelType == typeof(OffererProfileViewModel))
            {
                var avatar = FindViewById<CircleImageView>(Resource.Id.profileAvatar);
                transaction.AddSharedElement(avatar, "profileAvatar");
            }

            transaction.SetCustomAnimations(
                Resource.Animation.abc_grow_fade_in_from_bottom,
                Resource.Animation.abc_shrink_fade_out_from_bottom);

            base.OnBeforeFragmentChanging(fragmentInfo, transaction);
        }
    }
}
