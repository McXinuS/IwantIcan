using System;
using Android.App;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Views.Animations;
using IWantICan.Core.Models;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Droid.Platform;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platform.Platform;

namespace IWantICan.Droid.Utilities
{
	public class MvxRecyclerViewContactsAdapter : MvxRecyclerAdapter
	{
		Activity _activity;

		public MvxRecyclerViewContactsAdapter(Activity activity, IMvxAndroidBindingContext bindingContext)
			  : base(bindingContext)
		{
			_activity = activity;
		}

		public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
		{
			base.OnBindViewHolder(holder, position);

			holder.ItemView.Click += (s, e) =>
			{
				IntentContact(holder);
			};
		}

		void IntentContact(RecyclerView.ViewHolder holder)
		{
			var contact = (ContactsEntry)GetItem(holder.AdapterPosition);

			if (string.IsNullOrWhiteSpace(contact.Value))
				return;
			
			try
			{
				Intent intent;
				switch (contact.Type)
				{
					case ContactType.Phone:
						intent = new Intent(Intent.ActionView);
						intent.SetData(Android.Net.Uri.Parse("tel:" + contact.Value));
						break;
					case ContactType.Email:
						intent = new Intent(Intent.ActionSendto);
						intent.SetData(Android.Net.Uri.Parse("mailto:" + contact.Value));
						break;
					case ContactType.VkLink:
						intent = new Intent(Intent.ActionView);
						var url = contact.Value;
						if (!url.StartsWith("http://") && !url.StartsWith("https://"))
							url = "http://" + url;
						intent.SetData(Android.Net.Uri.Parse(url));
						break;
					default:
						throw new ArgumentOutOfRangeException();
				}
				_activity.StartActivity(intent);

			}
			catch (ActivityNotFoundException ex)
			{
				(new MvxDebugTrace()).Trace(MvxTraceLevel.Warning, "Intent failed", ex.Message);
			}
		}
	}
}
