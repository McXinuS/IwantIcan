using System.Windows.Input;
using Android.App;
using MvvmCross.Core.ViewModels;
using WantCan.Core.Services;

namespace WantCan.Core.ViewModels
{
    public class LoginViewModel : MvxViewModel
    {
        ILoginService _loginService;

        public LoginViewModel(ISharedPreferencesService sharedPreferencesService,
            ILoginService LoginService)
        {
            _loginService = LoginService;
            Login = sharedPreferencesService.LastLogin;
        }

        public ICommand TryLoginCommand
        {
            get { return new MvxCommand(TryLoginCommandExecution); }
        }

        public void TryLoginCommandExecution()
        {
            var ok = _loginService.Login(Login, Password);
            if (ok)
                ShowViewModel<OffersViewModel>();
        }

        string _login;
        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                RaisePropertyChanged(() => Login);
            }
        }

        string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }
    }
}
