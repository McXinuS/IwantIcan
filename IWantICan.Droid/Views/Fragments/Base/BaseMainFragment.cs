using Android.Content.Res;
using Android.OS;
using Android.Views;
using IWantICan.Droid.Activities;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Support.V4;

namespace IWantICan.Droid.Fragments
{
    public abstract class BaseMainFragment : MvxFragment
    {
        private bool _showHamburgerMenuButton;
        protected bool ShowHamburgerMenuButton
        {
            get { return _showHamburgerMenuButton; }
            set { _showHamburgerMenuButton = value; _mainActivity.ShowHamburgerMenu = value; }
        }

        MainActivity _mainActivity;

        protected BaseMainFragment()
        {
            RetainInstance = true;
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            base.OnCreateView(inflater, container, savedInstanceState);

            _mainActivity = Activity as MainActivity;

            return this.BindingInflate(FragmentId, null);
        }

        protected abstract int FragmentId { get; }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
                _mainActivity.drawerToggle?.OnConfigurationChanged(newConfig);
        }

        public override void OnActivityCreated(Bundle savedInstanceState)
        {
            base.OnActivityCreated(savedInstanceState);
            _mainActivity.drawerToggle?.SyncState();
		}
	}

    public abstract class BaseMainFragment<TViewModel> : BaseMainFragment where TViewModel : class, IMvxViewModel
    {
        public new TViewModel ViewModel
        {
            get { return (TViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
    }
}