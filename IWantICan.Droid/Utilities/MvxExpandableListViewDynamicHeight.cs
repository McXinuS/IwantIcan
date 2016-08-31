using System;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Views;
using MvvmCross.Binding.Droid.Views;

namespace IWantICan.Droid.Utilities
{
    class MvxExpandableListViewDynamicHeight : MvxExpandableListView
    {
        public MvxExpandableListViewDynamicHeight(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public MvxExpandableListViewDynamicHeight(Context context, IAttributeSet attrs, MvxExpandableListAdapter adapter) : base(context, attrs, adapter)
        {
        }

        protected MvxExpandableListViewDynamicHeight(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
        {
            if (Adapter == null)
            {
                base.OnMeasure(widthMeasureSpec, heightMeasureSpec);
                return;
            }

            var totalHeight = 0;
            for (var i = 0; i < Adapter.Count; i++)
            {
                var listItem = Adapter.GetView(i, null, this);
                listItem.Measure(0, 0);
                totalHeight += listItem.MeasuredHeight;
            }

            totalHeight += (DividerHeight * (Adapter.Count - 1));

            var heightMeasureSpecTotal = MeasureSpec.MakeMeasureSpec(totalHeight, MeasureSpecMode.AtMost);

            base.OnMeasure(widthMeasureSpec, heightMeasureSpecTotal);
        }
    }
}