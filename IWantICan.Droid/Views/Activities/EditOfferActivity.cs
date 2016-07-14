using Android.App;
using Android.OS;
using Android.Views;
using IWantICan.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;
using Toolbar = Android.Support.V7.Widget.Toolbar;

namespace IWantICan.Droid.Activities
{
    [Activity(
        Label = "@string/toolbar_title_edit",
        Theme = "@style/AppTheme",
        Name = "iwantican.droid.activities.EditOfferActivity"
        )]
    public class EditOfferActivity : MvxAppCompatActivity<EditOfferViewModel>
    {
        protected Toolbar toolbar { get; private set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.activity_offer_create);

            toolbar = FindViewById<Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);
            SupportActionBar.SetDisplayHomeAsUpEnabled(true);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Xml.menu_edit_offer, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    OnBackPressed();
                    break;
                case Resource.Id.action_delete:
                    ViewModel.DeleteCommand.Execute();
                    return true;
                case Resource.Id.action_apply:
                    ViewModel.SaveCommand.Execute();
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}
