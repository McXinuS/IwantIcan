using System.Collections.Generic;
using System.Reflection;
using Android.Content;
using Android.Support.V4.App;
using Android.Views;
using MvvmCross.Droid.Shared.Presenter;

namespace IWantICan.Droid.Utilities
{
	public class CustomPresenter : MvxFragmentsPresenter
	{
		public CustomPresenter(IEnumerable<Assembly> AndroidViewAssemblies)
			: base(AndroidViewAssemblies)
		{ }

		protected override void Show(Intent intent)
		{
			if (Activity == null) return;

			/*if (intent.Component.ClassName.Contains("OfferDetailsActivity"))
			{
				ActivityOptionsCompat options = ActivityOptionsCompat.MakeSceneTransitionAnimation(this, (View)ivProfile, "profile");

				Activity.StartActivity(intent, options.ToBundle());
			}
			else
			{*/
				Activity.StartActivity(intent);

				if (intent.Component.ClassName.Contains("CreateOfferActivity"))
				{
					Activity.OverridePendingTransition(
						Resource.Animation.grow_from_right_bottom,
						Resource.Animation.abc_shrink_fade_out_from_bottom);
				}
			//}
		}
	}
}
