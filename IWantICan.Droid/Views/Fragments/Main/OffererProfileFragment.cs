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
        //private readonly string[] _groups = new string[] { "�����", "�����" };

        //private string[] _cans = { "�������", "������", "�������" };
        //private string[] _wants = { "����", "������", "���" };

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

            // ������� ����� ��������� ��� ��������� ���������
            List<IList<IDictionary<string, object>>> �hildDataList = new List<IList<IDictionary<string, object>>>();

            // � ����� ��������� �hildDataList = ArrayList<�hildDataItemList>

            // ������� ��������� ��������� ��� ������ ������
            var �hildDataItemList = new List<IDictionary<string, object>>();
            foreach (string month in _cans)
            {
                map = new Dictionary<string, object> { { "monthName", month } };
                �hildDataItemList.Add(map);
            }
            �hildDataList.Add(�hildDataItemList);

            // ������� ��������� ��������� ��� ������ ������
            �hildDataItemList = new List<IDictionary<string, object>>();
            foreach (string month in _wants)
            {
                map = new Dictionary<string, object> { { "monthName", month } };
                �hildDataItemList.Add(map);
            }
            �hildDataList.Add(�hildDataItemList);
            
            string[] childFrom = { "monthName" };
            int[] childTo = { Android.Resource.Id.Text1 };

            SimpleExpandableListAdapter adapter = new SimpleExpandableListAdapter(
                    Application.Context, groupDataList,
                    Android.Resource.Layout.SimpleExpandableListItem1, groupFrom,
                    groupTo, �hildDataList, Android.Resource.Layout.SimpleListItem1,
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
