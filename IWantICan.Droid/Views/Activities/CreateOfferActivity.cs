using Android.App;
using Android.OS;
using Android.Views;
using IWantICan.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace IWantICan.Droid.Activities
{
    [Activity(
        Label = "@string/toolbar_title_create",
        Theme = "@style/AppTheme.OfferCreate"
        )]
    public class CreateOfferActivity : MvxAppCompatActivity<CreateOfferViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
			SupportActionBar.SetDisplayHomeAsUpEnabled(true);
            SetContentView(Resource.Layout.activity_offer_create);
        }
        
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Xml.menu_create_offer, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    OnBackPressed();
                    break;
                case Resource.Id.action_apply:
                    ViewModel.SaveCommand.Execute();
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}
