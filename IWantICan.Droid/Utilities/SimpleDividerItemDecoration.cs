using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V7.Widget;
using Android.Views;

namespace IWantICan.Droid.Utilities
{
	public class SimpleDividerItemDecoration : RecyclerView.ItemDecoration
	{
		private Drawable mDivider;

		public SimpleDividerItemDecoration(Context context)
		{
			mDivider = context.Resources.GetDrawable(Resource.Drawable.line_divider);
		}

		public override void OnDrawOver(Canvas c, RecyclerView parent, RecyclerView.State state)
		{
			int left = parent.PaddingLeft;
			int right = parent.Width - parent.PaddingRight;

			int childCount = parent.ChildCount;
			for (int i = 0; i < childCount; i++)
			{
				View child = parent.GetChildAt(i);

				RecyclerView.LayoutParams lParams = (RecyclerView.LayoutParams)child.LayoutParameters;

				int top = child.Bottom + lParams.BottomMargin;
				int bottom = top + mDivider.IntrinsicHeight;

				mDivider.SetBounds(left, top, right, bottom);
				mDivider.Draw(c);
			}
		}
	}
}