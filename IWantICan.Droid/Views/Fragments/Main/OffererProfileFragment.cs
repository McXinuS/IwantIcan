using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using IWantICan.Core.ViewModels;
using MvvmCross.Binding.Droid.Views;
using MvvmCross.Droid.Shared.Attributes;

namespace IWantICan.Droid.Fragments
{
    [MvxFragment(typeof(OfferItemContainerViewModel), Resource.Id.main_frame, true)]
    [Register("iwantican.droid.fragments.OffererProfileFragment")]
    public class OffererProfileFragment : BaseOfferItemFragment<OffererProfileViewModel>
    {
        //private readonly string[] _groups = new string[] { "Õî÷åò", "Ìîæåò" };

        //private string[] _cans = { "Äåêàáğü", "ßíâàğü", "Ôåâğàëü" };
        //private string[] _wants = { "Ìàğò", "Àïğåëü", "Ìàé" };

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);

            var backArrow = view.FindViewById<ImageView>(Resource.Id.backArrow);
            backArrow.Click += (sender, args) => { Activity.OnBackPressed(); };

            // TODO intent web/phone/mailto action on corresponding layout click

            /*IDictionary<string, object> map;

            var groupDataList = new List<IDictionary<string, object>>();
            foreach (var group in _groups)
            {
                map = new Dictionary<string, object> {{"groupName", group}};
                groupDataList.Add(map);
            }
            
            string[] groupFrom = { "groupName" };
            int[] groupTo = { Android.Resource.Id.Text1 };

            // ñîçäàåì îáùóş êîëëåêöèş äëÿ êîëëåêöèé ıëåìåíòîâ
            List<IList<IDictionary<string, object>>> ñhildDataList = new List<IList<IDictionary<string, object>>>();

            // â èòîãå ïîëó÷èòñÿ ñhildDataList = ArrayList<ñhildDataItemList>

            // ñîçäàåì êîëëåêöèş ıëåìåíòîâ äëÿ ïåğâîé ãğóïïû
            var ñhildDataItemList = new List<IDictionary<string, object>>();
            foreach (string month in _cans)
            {
                map = new Dictionary<string, object> { { "monthName", month } };
                ñhildDataItemList.Add(map);
            }
            ñhildDataList.Add(ñhildDataItemList);

            // ñîçäàåì êîëëåêöèş ıëåìåíòîâ äëÿ âòîğîé ãğóïïû
            ñhildDataItemList = new List<IDictionary<string, object>>();
            foreach (string month in _wants)
            {
                map = new Dictionary<string, object> { { "monthName", month } };
                ñhildDataItemList.Add(map);
            }
            ñhildDataList.Add(ñhildDataItemList);
            
            string[] childFrom = { "monthName" };
            int[] childTo = { Android.Resource.Id.Text1 };

            SimpleExpandableListAdapter adapter = new SimpleExpandableListAdapter(
                    Application.Context, groupDataList,
                    Android.Resource.Layout.SimpleExpandableListItem1, groupFrom,
                    groupTo, ñhildDataList, Android.Resource.Layout.SimpleListItem1,
                    childFrom, childTo);
            
            var elv = view.FindViewById<ExpandableListView>(Resource.Id.offers);
            elv.SetAdapter(adapter);*/

            //MvxExpandableListView mvxElv = view. FindViewById<MvxExpandableListView>(Resource.Id.offers);
            //mvxElv.GroupTemplateId

            return view;
        }

        protected override int FragmentId => Resource.Layout.fragment_offerer_profile;
    }
}
