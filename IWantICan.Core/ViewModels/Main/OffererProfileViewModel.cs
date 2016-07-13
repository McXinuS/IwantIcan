using System;
using System.Threading.Tasks;
using IWantICan.Core.Helpers;
using IWantICan.Core.Models;

namespace IWantICan.Core.ViewModels
{
    public class OffererProfileViewModel : BaseViewModel
    {
        UserModel _user;

        public void Init(string user)
		{
			Task t = new Task(() => LoadData(user));
			t.Start();
        }

	    private async void LoadData(string user)
		{
			// prevent UI issue
			await Task.Delay(TimeSpan.FromMilliseconds(250));
			User = user.Deserialize<UserModel>();
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
