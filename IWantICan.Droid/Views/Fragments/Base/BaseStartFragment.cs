using Android.OS;
using Android.Support.V4.Content.Res;
using Android.Text.Method;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Support.V4;

namespace IWantICan.Droid.Fragments
{
	public abstract class BaseStartFragment : MvxFragment
	{
		EditText mEtPwd;
		CheckBox mCbShowPwd;

		protected BaseStartFragment()
		{
			RetainInstance = true;
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			base.OnCreateView(inflater, container, savedInstanceState);

			var view = this.BindingInflate(FragmentId, null);

			mEtPwd = (EditText)view.FindViewById(Resource.Id.editPassword);
			mCbShowPwd = (CheckBox)view.FindViewById(Resource.Id.showPasswordCb);

			if (mEtPwd != null && mCbShowPwd != null)
			{
				HidePassword();
				mCbShowPwd.CheckedChange += (sender, args) =>
				{
					if (mCbShowPwd.Checked)
						ShowPassword();
					else
						HidePassword();
				};
			}

			HideSoftKeyboard();

			return view;
		}

		private void HidePassword()
		{
			mCbShowPwd.SetButtonDrawable(ResourcesCompat.GetDrawable(Resources,
				Resource.Drawable.ic_eye,
				Activity.Theme));
			mEtPwd.TransformationMethod = PasswordTransformationMethod.Instance;
		}

		private void ShowPassword()
		{
			mCbShowPwd.SetButtonDrawable(ResourcesCompat.GetDrawable(Resources,
				Resource.Drawable.ic_eye_strike,
				Activity.Theme));
			mEtPwd.TransformationMethod = HideReturnsTransformationMethod.Instance;
		}

		protected void HideSoftKeyboard()
		{
			var currentFocus = Activity.CurrentFocus;

			if (currentFocus == null) return;

			InputMethodManager inputMethodManager = (InputMethodManager)Activity.GetSystemService(Android.Content.Context.InputMethodService);
			inputMethodManager.HideSoftInputFromWindow(currentFocus.WindowToken, 0);

			currentFocus.ClearFocus();
		}

		protected abstract int FragmentId { get; }
	}

	public abstract class BaseStartFragment<TViewModel> : BaseStartFragment where TViewModel : class, IMvxViewModel
	{
		public new TViewModel ViewModel
		{
			get { return (TViewModel)base.ViewModel; }
			set { base.ViewModel = value; }
		}
	}
}

