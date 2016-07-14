using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IWantICan.Core.Helpers;
using IWantICan.Core.Models;
using IWantICan.Core.Services;
using MvvmCross.Platform;

namespace IWantICan.Core.ViewModels
{
    public class OffererProfileViewModel : BaseViewModel
    {
        private UserModel _user;
        private List<List<string>> _offers;

        public List<List<string>> Offers
        {
            get { return _offers; }
            set { _offers = value; RaisePropertyChanged(() => Offers); }
        }

        public void Init(string user)
		{
			Task t = new Task(() => LoadData(user));
			t.Start();
        }

	    private async void LoadData(string user)
		{
			User = user.Deserialize<UserModel>();

	        /*ICanService canService = Mvx.Resolve<ICanService>();
            IWantService wantService = Mvx.Resolve<IWantService>();

	        var cans = await canService.GetCanListByUser(User.id);
	        var wants = await wantService.GetWantListByUser(User.id);

            var canNames = cans.Select(t => t.name).ToList();
            var wantNames = wants.Select(t => t.name).ToList();

	        Offers = new List<List<string>>
            {
                ...
            };*/

		}

	    public UserModel User
        {
            get { return _user; }
            set
            {
                _user = value;
                RaisePropertyChanged(() => User);
            }
        }
    }
}
