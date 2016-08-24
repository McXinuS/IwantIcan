using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Android.Support.V7.Widget;
using Android.Views;
using IWantICan.Core.ViewModels;
using IWantICan.Droid.Activities;
using MvvmCross.Droid.Shared.Attributes;

namespace IWantICan.Droid.Fragments
{
    [MvxFragment(typeof(StartContainerViewModel), Resource.Id.start_frame, true)]
    [Register("iwantican.droid.fragments.SignupFragment")]
    public class SignupFragment : BaseStartFragment<SignupViewModel>
    {
	    string oldTitle;
	    StartContainerActivity activity;
		ActionBar actionBar;

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

	        HasOptionsMenu = true;

			activity = Activity as StartContainerActivity;
			oldTitle = activity.Title;
			activity.SetTitle(Resource.String.toolbar_title_register);

			actionBar = activity.SupportActionBar;
			actionBar.SetDisplayHomeAsUpEnabled(true);

            return view;
        }
        
        public override void OnCreateOptionsMenu(IMenu menu, MenuInflater inflater)
        {
            inflater.Inflate(Resource.Xml.menu_signup, menu);
            base.OnCreateOptionsMenu(menu, inflater);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Android.Resource.Id.Home:
                    (Activity as StartContainerActivity)?.OnBackPressed();
                    return true;
				case Resource.Id.action_apply:
					ViewModel.SignupCommand.Execute();
		            return true;
            }

            return base.OnOptionsItemSelected(item);
		}

		public override void OnDestroyView()
		{
			activity.Title = oldTitle;
			actionBar.Hide();
			base.OnDestroyView();
		}

		protected override int FragmentId => Resource.Layout.fragment_signup;
    }
}