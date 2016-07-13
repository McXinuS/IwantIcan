using System.Threading.Tasks;
using IWantICan.Core.Interfaces;
using IWantICan.Core.Models;
using IWantICan.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IWantICan.Core.ViewModels
{
    public class MyProfileViewModel : BaseViewModel
    {
        private UserModel _user;
        IUserService _userService;
        IDialogService _dialogService;

        public MyProfileViewModel(IUserService UserService, IDialogService DialogService)
        {
            _userService = UserService;
            _dialogService = DialogService;

            Task t = new Task(LoadUser);
            t.Start();
        }

        public IMvxCommand SaveCommand
        {
            get { return new MvxCommand(SaveUser); }
        }

        private async void SaveUser()
        {
            var ok = await _userService.UpdateUser(User);
            if (ok)
                _dialogService.Alert(Constants.DialogSaveOk,
                    Constants.DialogTitleOk,
                    "ОК",
                    () => Close(this));
            else
                _dialogService.Alert(Constants.DialogSaveFailed,
                    Constants.DialogTitleError,
                    "ОК");
        }

        private async void LoadUser()
        {
            User = await _userService.GetCurrentUser();
        }

        public UserModel User
        {
            get { return _user; }
            set { _user = value; RaisePropertyChanged(() => User); }
        }
    }
}
