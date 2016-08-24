using System.Collections.Generic;
using Android.Support.V7.Widget;
using Android.Support.V7.Widget.Helper;
using IWantICan.Core.Models;

namespace IWantICan.Droid.Utilities
{
	class SimpleItemTouchHelperCallback : ItemTouchHelper.Callback
	{
		private readonly OfferRecyclerViewAdapter _adapter;
		List<OfferModel> _list;
		public SimpleItemTouchHelperCallback(OfferRecyclerViewAdapter adapter, List<OfferModel> list)
		{
			_adapter = adapter;
			_list = list;
		}

		public override int GetMovementFlags(RecyclerView p0, RecyclerView.ViewHolder p1)
		{
			//int dragFlags = ItemTouchHelper.Up | ItemTouchHelper.Down;
			int swipeFlags = ItemTouchHelper.Start | ItemTouchHelper.End;
			return MakeMovementFlags(0, swipeFlags);
		}

		public override bool OnMove(RecyclerView p0, RecyclerView.ViewHolder p1, RecyclerView.ViewHolder p2)
		{
			_adapter.OnItemMove(p0, p1, p2);
			return true;
		}

		public override void OnSwiped(RecyclerView.ViewHolder p0, int p1)
		{
			var item = _list[p0.AdapterPosition];
			_adapter.OnItemDismiss(p0, p1, item);
		}
	}
}
