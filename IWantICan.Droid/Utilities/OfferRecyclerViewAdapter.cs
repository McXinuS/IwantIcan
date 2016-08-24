using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Input;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using MvvmCross.Binding;
using MvvmCross.Binding.Attributes;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Binding.ExtensionMethods;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Droid.Support.V7.RecyclerView.ItemTemplates;
using MvvmCross.Platform;
using MvvmCross.Platform.Exceptions;
using MvvmCross.Platform.Platform;
using MvvmCross.Platform.WeakSubscription;

namespace IWantICan.Droid.Utilities
{
	public class OfferRecyclerViewAdapter
	   : Android.Support.V7.Widget.RecyclerView.Adapter
	   , IMvxRecyclerAdapter
	{
		private readonly IMvxAndroidBindingContext _bindingContext;

		// Keep track of all ViewHolders created by this adapter.
		private readonly List<WeakReference<IMvxRecyclerViewHolder>> _viewHolders = new List<WeakReference<IMvxRecyclerViewHolder>>();

		private ICommand _itemClick, _itemLongClick, _itemSwipe;
		private IEnumerable _itemsSource;
		private IDisposable _subscription;
		private IMvxTemplateSelector _itemTemplateSelector;

		protected IMvxAndroidBindingContext BindingContext => _bindingContext;

		int _attachedRecyclerViews;

		public OfferRecyclerViewAdapter() : this(MvxAndroidBindingContextHelpers.Current()) { }
		public OfferRecyclerViewAdapter(IMvxAndroidBindingContext bindingContext)
		{
			this._bindingContext = bindingContext;
		}

		public OfferRecyclerViewAdapter(IntPtr javaReference, JniHandleOwnership transfer)
			: base(javaReference, transfer)
		{ }

		public bool ReloadOnAllItemsSourceSets { get; set; }

		public ICommand ItemClick
		{
			get { return _itemClick; }
			set
			{
				if (ReferenceEquals(_itemClick, value))
				{
					return;
				}

				if (_itemClick != null)
				{
					MvxTrace.Warning("Changing ItemClick may cause inconsistencies where some items still call the old command.");
				}

				_itemClick = value;
			}
		}

		public ICommand ItemLongClick
		{
			get { return _itemLongClick; }
			set
			{
				if (ReferenceEquals(_itemLongClick, value))
				{
					return;
				}

				if (_itemLongClick != null)
				{
					MvxTrace.Warning("Changing ItemLongClick may cause inconsistencies where some items still call the old command.");
				}

				_itemLongClick = value;
			}
		}

		public ICommand ItemSwipe
		{
			get { return _itemSwipe; }
			set
			{
				if (ReferenceEquals(_itemSwipe, value))
				{
					return;
				}

				if (_itemSwipe != null)
				{
					MvxTrace.Warning("Changing ItemSwipe may cause inconsistencies where some items still call the old command.");
				}

				_itemSwipe = value;
			}
		}

		[MvxSetToNullAfterBinding]
		public virtual IEnumerable ItemsSource
		{
			get { return _itemsSource; }
			set { SetItemsSource(value); }
		}

		public virtual IMvxTemplateSelector ItemTemplateSelector
		{
			get { return _itemTemplateSelector; }
			set
			{
				if (ReferenceEquals(_itemTemplateSelector, value))
					return;

				_itemTemplateSelector = value;

				// since the template selector has changed then let's force the list to redisplay by firing NotifyDataSetChanged()
				if (_itemsSource != null)
					NotifyDataSetChanged();
			}
		}

		public override void OnViewAttachedToWindow(Java.Lang.Object holder)
		{
			base.OnViewAttachedToWindow(holder);

			var viewHolder = (IMvxRecyclerViewHolder)holder;
			viewHolder.OnAttachedToWindow();
		}

		public override void OnViewDetachedFromWindow(Java.Lang.Object holder)
		{
			base.OnViewDetachedFromWindow(holder);

			var viewHolder = (IMvxRecyclerViewHolder)holder;
			viewHolder.OnDetachedFromWindow();
		}

		public override void OnViewRecycled(Java.Lang.Object holder)
		{
			base.OnViewRecycled(holder);

			var viewHolder = (IMvxRecyclerViewHolder)holder;
			// TODO
			//viewHolder.OnViewRecycled();
		}

		public override void OnAttachedToRecyclerView(Android.Support.V7.Widget.RecyclerView recyclerView)
		{
			++_attachedRecyclerViews;
			base.OnAttachedToRecyclerView(recyclerView);
		}

		public override void OnDetachedFromRecyclerView(Android.Support.V7.Widget.RecyclerView recyclerView)
		{
			--_attachedRecyclerViews;
			base.OnDetachedFromRecyclerView(recyclerView);
		}
		
		public void OnItemMove(RecyclerView recycler, RecyclerView.ViewHolder p1, RecyclerView.ViewHolder p2)
		{
			NotifyItemMoved(p1.AdapterPosition, p2.AdapterPosition);
		}

		public void OnItemDismiss(RecyclerView.ViewHolder viewHolder, int p1, object item)
		{
			ItemSwipe?.Execute(item);
			//NotifyItemRemoved(viewHolder.AdapterPosition);
		}

		public override Android.Support.V7.Widget.RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
		{
			var itemBindingContext = new MvxAndroidBindingContext(parent.Context, _bindingContext.LayoutInflaterHolder);

			var vh = new MvxRecyclerViewHolder(InflateViewForHolder(parent, viewType, itemBindingContext), itemBindingContext)
			{
				Click = ItemClick,
				LongClick = ItemLongClick
			};

			_viewHolders.Add(new WeakReference<IMvxRecyclerViewHolder>(vh));

			return vh;
		}

		public override int GetItemViewType(int position)
		{
			var itemAtPosition = GetItem(position);
			return ItemTemplateSelector.GetItemViewType(itemAtPosition);
		}

		protected virtual View InflateViewForHolder(ViewGroup parent, int viewType, IMvxAndroidBindingContext bindingContext)
		{
			var layoutId = ItemTemplateSelector.GetItemLayoutId(viewType);
			return bindingContext.BindingInflate(layoutId, parent, false);
		}

		public override void OnBindViewHolder(Android.Support.V7.Widget.RecyclerView.ViewHolder holder, int position)
		{
			((IMvxRecyclerViewHolder)holder).DataContext = GetItem(position);
		}

		public override int ItemCount => _itemsSource.Count();

		public virtual object GetItem(int position)
		{
			return _itemsSource.ElementAt(position);
		}

		public int ItemTemplateId { get; set; }

		protected virtual void SetItemsSource(IEnumerable value)
		{
			if (ReferenceEquals(_itemsSource, value) && !ReloadOnAllItemsSourceSets)
			{
				return;
			}

			if (_subscription != null)
			{
				_subscription.Dispose();
				_subscription = null;
			}

			_itemsSource = value;

			if (_itemsSource != null && !(_itemsSource is IList))
			{
				MvxBindingTrace.Trace(MvxTraceLevel.Warning,
					"Binding to IEnumerable rather than IList - this can be inefficient, especially for large lists");
			}

			var newObservable = _itemsSource as INotifyCollectionChanged;
			if (newObservable != null)
			{
				_subscription = newObservable.WeakSubscribe(OnItemsSourceCollectionChanged);
			}

			NotifyDataSetChanged();
		}

		protected virtual void OnItemsSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			NotifyDataSetChanged(e);
		}

		public virtual void NotifyDataSetChanged(NotifyCollectionChangedEventArgs e)
		{
			try
			{
				switch (e.Action)
				{
					case NotifyCollectionChangedAction.Add:
						NotifyItemRangeInserted(e.NewStartingIndex, e.NewItems.Count);
						break;
					case NotifyCollectionChangedAction.Move:
						for (int i = 0; i < e.NewItems.Count; i++)
						{
							var oldItem = e.OldItems[i];
							var newItem = e.NewItems[i];

							NotifyItemMoved(this.ItemsSource.GetPosition(oldItem), this.ItemsSource.GetPosition(newItem));
						}
						break;
					case NotifyCollectionChangedAction.Replace:
						NotifyItemRangeChanged(e.NewStartingIndex, e.NewItems.Count);
						break;
					case NotifyCollectionChangedAction.Remove:
						NotifyItemRangeRemoved(e.OldStartingIndex, e.OldItems.Count);
						break;
					case NotifyCollectionChangedAction.Reset:
						NotifyDataSetChanged();
						break;
				}
			}
			catch (Exception exception)
			{
				Mvx.Warning(
					"Exception masked during Adapter RealNotifyDataSetChanged {0}. Are you trying to update your collection from a background task? See http://goo.gl/0nW0L6",
					exception.ToLongString());
			}
		}

		public virtual void ClearAllBindings()
		{
			// Only clear bindings if we're attached to at least one RecyclerView.
			// This might be wrong if we're sharing the adapter with multiple RecyclerViews but
			// I haven't hit this case in the wild.  We may need to revisit this if it's a problem.
			if (_attachedRecyclerViews > 0)
			{
				foreach (var vhRef in _viewHolders)
				{
					IMvxRecyclerViewHolder vh;
					if (vhRef.TryGetTarget(out vh))
						vh.ClearAllBindings();
				}

				_viewHolders.Clear();
			}
		}
	}
}