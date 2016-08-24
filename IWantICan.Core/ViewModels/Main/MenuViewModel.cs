using System.Threading.Tasks;
using IWantICan.Core.Models;
using IWantICan.Core.Services;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;

namespace IWantICan.Core.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        IAuthService _authService;
        IUserService _userService;

        UserModel _user;
        
        public MenuViewModel(IAuthService AuthService,
            IUserService UserService)
        {
            _authService = AuthService;
            _userService = UserService;

            ReloadUserCommand.Execute();
        }

        async void LoadHeader()
        {
            User = await _userService.GetCurrentUser();
        }

        public IMvxCommand ShowAllOffersCommand
        {
            get { return new MvxCommand(() => ShowViewModel<AllOffersViewModel>()); }
        }

        public IMvxCommand ShowMyOffersCommand
        {
            get { return new MvxCommand(() => ShowViewModel<MyOffersViewModel>()); }
        }

        public IMvxCommand ShowMyProfileCommand
        {
            get { return new MvxCommand(() => ShowViewModel<MyProfileViewModel>()); }
        }

        public IMvxCommand LogoutCommand
        {
            get { return new MvxCommand(DoLogoutCommand); }
        }

        private async void DoLogoutCommand()
        {
            var ok = await _authService.Logout();
            if (ok)
            {
                ShowViewModel<StartContainerViewModel>();
                Close(this);
            }
        }

        public UserModel User
        {
            get { return _user; }
            set { _user = value; RaisePropertyChanged(() => User); }
        }

        public IMvxCommand ReloadUserCommand
        {
            get { return new MvxCommand(() =>
            {
                Task t = new Task(LoadHeader);
                t.Start();
            } ); }
        }
    }
}