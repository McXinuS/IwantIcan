using System.Threading.Tasks;
using IWantICan.Core.Models;
using IWantICan.Core.Services;
using IWantICan.Core.Services.Messenger;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;

namespace IWantICan.Core.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        IAuthService _authService;
        IUserService _userService;

	    IMvxMessenger _messenger;
		private readonly MvxSubscriptionToken _token;

		UserModel _user;
        
        public MenuViewModel(IAuthService authService,
            IUserService userService,
			IMvxMessenger messenger)
        {
            _authService = authService;
            _userService = userService;
			
			_messenger = messenger;
			_token = _messenger.Subscribe<ProfileEditSuccessMessage>(async message => await LoadHeader());

	        Task.Run(LoadHeader);
        }

        async Task LoadHeader()
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
            get { return new MvxCommand(() => Task.Run(LoadHeader)); }
        }
    }
}