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
        Theme = "@style/AppTheme",
        Name = "iwantican.droid.activities.CreateOfferActivity"
        )]
    public class CreateOfferActivity : MvxAppCompatActivity<CreateOfferViewModel>
    {
        protected Toolbar toolbar { get; private set; }
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_offer_create);

            toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);

            //EditText et = FindViewById<EditText>(Resource.Id.editCat);
            //et.SetOnKeyListener(null);
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
