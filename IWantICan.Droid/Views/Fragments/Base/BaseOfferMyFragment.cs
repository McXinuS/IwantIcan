using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Android.App;
using Android.Content;
using Android.Support.V7.Widget;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.Droid.BindingContext;
using MvvmCross.Binding.Droid.Views;
using MvvmCross.Binding.ExtensionMethods;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Droid.Support.V7.RecyclerView.ItemTemplates;
using PopupMenu = Android.Widget.PopupMenu;

namespace IWantICan.Droid.Fragments
{
    public abstract class BaseOfferMyFragment : BaseOfferFragment
    {
        //protected MyOfferAdapter OfferAdapter { get; set; }

        public BaseOfferMyFragment()
        {
            //InitAdapter();
        }
        
        // TODO
        /*public async void InitAdapter()
        {
            OfferAdapter = new MyOfferAdapter();//(Application.Context, (IMvxAndroidBindingContext)BindingContext);
            OfferAdapter.ItemsSource = new List<int>();
            RecyclerView.Adapter = OfferAdapter;
        }

        public class MyOfferAdapter : MvxRecyclerAdapter
        {
            public class OfferViewHolder: RecyclerView.ViewHolder
            {
                public ImageView OverflowView;

                public OfferViewHolder(View itemView)
                    : base(itemView)
                {
                    OverflowView = itemView.FindViewById<ImageView>(Resource.Id.options);
                }
            }

            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                OfferViewHolder vh = holder as OfferViewHolder;
                
                vh.OverflowView.Click += (sender, args) => Toast.MakeText(Application.Context,
                    $"{position} item is selected", ToastLength.Short).Show();
            }

            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                var itemView = LayoutInflater.From(parent.Context).
                            Inflate(Resource.Layout.listitem_offer_my, parent, false);
                return new OfferViewHolder(itemView);
            }
        }*/

        private void ShowPopupMenu(View v)
        {
            PopupMenu popupMenu = new PopupMenu(Context, v);
            popupMenu.Inflate(Resource.Xml.popupmenu_offer);

            popupMenu.MenuItemClick += (sender, args) =>
            {
                switch (args.Item.ItemId)
                {
                    case Resource.Id.action_edit:
                        return;
                    case Resource.Id.action_remove:
                        return;
                }
            };
        }
    }

    public abstract class BaseOfferMyFragment<TViewModel> : BaseOfferMyFragment where TViewModel : class, IMvxViewModel
    {
        public new TViewModel ViewModel
        {
            get { return (TViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
    }
}