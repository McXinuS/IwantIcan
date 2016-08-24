using System;
using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.Widget;
using Android.Views;
using IWantICan.Core.ViewModels;
using IWantICan.Droid.Utilities;
using MvvmCross.Droid.Shared.Attributes;
using MvvmCross.Droid.Support.V7.RecyclerView;

namespace IWantICan.Droid.Fragments
{
    [MvxFragment(typeof(OfferDetailsContainerViewModel), Resource.Id.main_frame, true)]
    [Register("iwantican.droid.fragments.OffererProfileFragment")]
    public class OffererProfileFragment : BaseOfferDetailsFragment<OffererProfileViewModel>
    {
        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            var view = base.OnCreateView(inflater, container, savedInstanceState);
			
	        var contactsRecView = view.FindViewById<MvxRecyclerView>(Resource.Id.contactsRecycler);

			//contactsRecView.Click += ContactsRecViewOnClick;
			contactsRecView.HasFixedSize = true;
			var layoutManager = new LinearLayoutManager(Activity);
			contactsRecView.SetLayoutManager(layoutManager);

			/*
			string[] _groups = new string[] { "�����", "�����" };
			string[] _cans = { "�������", "������", "�������" };
			string[] _wants = { "����", "������", "���" };

			IDictionary<string, object> map;
			
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

            MvxExpandableListAdapter adapter = new MvxExpandableListAdapter(
                    Application.Context, groupDataList,
                    Android.Resource.Layout.SimpleExpandableListItem1, groupFrom,
                    groupTo, �hildDataList, Android.Resource.Layout.SimpleListItem1,
                    childFrom, childTo);
            
            var elv = view.FindViewById<MvxExpandableListView>(Resource.Id.offers);
            elv.SetAdapter(adapter);*/

			return view;
        }

	    private void ContactsRecViewOnClick(object sender, EventArgs eventArgs)
	    {
		    throw new NotImplementedException();
	    }

	    protected override int FragmentId => Resource.Layout.fragment_offerer_profile;
    }
}
