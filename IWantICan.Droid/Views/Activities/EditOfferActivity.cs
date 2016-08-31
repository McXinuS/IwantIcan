using Android.App;
using Android.OS;
using Android.Views;
using Android.Widget;
using IWantICan.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace IWantICan.Droid.Activities
{
    [Activity(
        Label = "@string/toolbar_title_edit",
        Theme = "@style/AppTheme.OfferEdit",
		WindowSoftInputMode = SoftInput.StateAlwaysHidden
		)]
    public class EditOfferActivity : MvxAppCompatActivity<EditOfferViewModel>
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
			SupportActionBar.SetDisplayHomeAsUpEnabled(true);
			SetContentView(Resource.Layout.activity_offer_create);
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
