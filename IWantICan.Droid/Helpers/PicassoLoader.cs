using Android.Widget;
using Square.Picasso;

namespace IWantICan.Droid.Helpers
{
	public static class PicassoLoader
	{
		/// <summary>
		/// <see cref="Refractored.Controls"/> optimized: NoFade included
		/// Loads an image from URL into ImageView using Picasso.
		/// </summary>
		public static void LoadWithPicasso(this ImageView view, string imageUrl)
		{
			Picasso.With(view.Context)
				.Load(imageUrl)
				.NoFade()
				.Into(view);
		}

		/// <summary>
		/// Loads an image from URL into ImageView using Picasso.
		/// </summary>
		public static void LoadWithPicasso(this ImageView view, string imageUrl, int placeholder)
		{
		    if (view == null)
		        return;
			Picasso.With(view.Context)
				.Load(imageUrl)
				.NoFade()
				.Placeholder(placeholder)
				.Error(placeholder)
				.Into(view);
		}
	}
}