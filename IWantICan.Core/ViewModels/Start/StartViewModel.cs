using System;
using System.Windows.Input;
using IWantICan.Core.Interfaces;
using IWantICan.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IWantICan.Core.ViewModels
{
    public class StartViewModel : BaseViewModel
    {
        IAuthService _authService;
        IDialogService _dialogService;

        string _login;
        string _password;
        string _errorLogin;
        string _errorPassword;

        bool _isLoading;
        bool _isLogged;

        // Result of login/password validation
        [Flags] enum ValidationStatus
        {
            Ok,
            LoginEmpty,
            LoginNotValid,
            PasswordEmpty,
            PasswordTooShort
        }

        public StartViewModel(ISharedPreferencesService sharedPreferencesService,
            IAuthService AuthService,
            IDialogService DialogService)
        {
            _authService = AuthService;
            _dialogService = DialogService;
            Login = sharedPreferencesService.LastLogin;
            IsLoading = false;
            IsLogged = false;
        }

        public ICommand TryLoginCommand
        {
            get { return new MvxCommand(() => DoTryLoginCommand()); }
        }

        public async void DoTryLoginCommand()
        {
            var status = ValidateInput();
            if (status.Equals(ValidationStatus.Ok))
            {
                IsLoading = true;
                var ok = await _authService.Login(Login, Password);
                IsLoading = false;

                if (ok)
                {
                    IsLogged = true;
                    ShowViewModel<MainViewModel>();
                    Close(this);
                    return;
                }
                else
                {
                    _dialogService.Alert(Constants.LoginOrRegisterFailed, "Ошибка", "ОК");
                    ErrorLogin = Constants.LoginWarning;
                    ErrorPassword = Constants.LoginWarning;
                    return;
                }
            }

            if (status.HasFlag(ValidationStatus.LoginEmpty))
                ErrorLogin = Constants.LoginRequired;

            if (status.HasFlag(ValidationStatus.LoginNotValid))
                ErrorLogin = Constants.LoginNotValid;

            if (status.HasFlag(ValidationStatus.PasswordEmpty))
                ErrorPassword = Constants.PasswordRequired;

            if (status.HasFlag(ValidationStatus.LoginNotValid))
                ErrorPassword = Constants.PasswordShort;
        }

        // indicates whether login with current
        // parameters is possible
        private bool CanLogin()
        {
            if (ValidateInput() == ValidationStatus.Ok)
                return true;
            return false;
        }

        /*
         * Login:
         * - not empty/whitespaces
         * - only has latin/numeric/'_'
         * Password:
         * - not empty/whitespaces
         * - has not less than Constants.PasswordMinLength length
        */
        private ValidationStatus ValidateInput()
        {
            ValidationStatus st = 0;

            // check login
            if (string.IsNullOrWhiteSpace(Login))
                st |= ValidationStatus.LoginEmpty;
            else
                foreach (var c in Login)
                {
                    if (!((c >= 'a' && c <= 'z')
                        || (c >= 'A' && c <= 'Z')
                        || c == '_'
                        || (c >= '0' && c <= '9')
                       ))
                    {
                        // TODO check why 'testloginx' fails validation
                        //st |= ValidationStatus.LoginNotValid;
                        break;
                    }
                }

            // check password
            if (string.IsNullOrWhiteSpace(Password))
                st |= ValidationStatus.PasswordEmpty;
            else if (Password.Length < Constants.PasswordMinLength)
                st |= ValidationStatus.PasswordTooShort;

            return (st == 0) ? ValidationStatus.Ok : st;
        }

        public ICommand GoSignupViewCommand
        {
            get { return new MvxCommand(() => ShowViewModel<SignupViewModel>()); }
        }

        public string Login
        {
            get { return _login; }
            set
            {
                _login = value;
                RaisePropertyChanged(() => Login);
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }

        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                RaisePropertyChanged(() => IsLoading);
            }
        }

        public bool IsLogged
        {
            get { return _isLogged; }
            set
            {
                _isLogged = value;
                RaisePropertyChanged(() => IsLogged);
            }
        }

        public string ErrorLogin
        {
            get { return _errorLogin; }
            set
            {
                _errorLogin = value;
                RaisePropertyChanged(() => ErrorLogin);
            }
        }

        public string ErrorPassword
        {
            get { return _errorPassword; }
            set
            {
                _errorPassword = value;
                RaisePropertyChanged(() => ErrorPassword);
            }
        }
    }
}
