using System.Windows.Input;
using IWantICan.Core.Interfaces;
using IWantICan.Core.Models;
using IWantICan.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IWantICan.Core.ViewModels
{
    public class SignupViewModel : BaseViewModel
    {
        IUserService _userService;
        IDialogService _dialogService;

        bool _isLoading;

        public SignupViewModel(IUserService UserService, IDialogService DialogService)
        {
            _userService = UserService;
            _dialogService = DialogService;
            IsLoading = false;
        }

        public IMvxCommand SignupCommand
        {
            get { return new MvxCommand(DoSignupCommand); }
        }

        private async void DoSignupCommand()
        {
            IsLoading = true;

            var user = new UserModel
            {
                avatar = Avatar,
                email = Email,
                login = Login,
                name = Name,
                surname = Surname,
                password = Password,
                phone = Phone,
                vkLink = VkLink
            };

            var ok = await _userService.CreateUser(user);
            IsLoading = false;

            if (ok)
            {
                ShowViewModel<MainViewModel>();
                Close(this);
            }
            else
                _dialogService.Alert(Constants.LoginOrRegisterFailed, "Ошибка", "ОК");
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

        string _avatar;
        public string Avatar
        {
            get { return _avatar; }
            set
            {
                _avatar = value;
                RaisePropertyChanged(() => Avatar);
            }
        }

        string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        string _surname;
        public string Surname
        {
            get { return _surname; }
            set
            {
                _surname = value;
                RaisePropertyChanged(() => Surname);
            }
        }

        string _email;
        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                RaisePropertyChanged(() => Email);
            }
        }

        string _phone;
        public string Phone
        {
            get { return _phone; }
            set
            {
                _phone = value;
                RaisePropertyChanged(() => Phone);
            }
        }

        string _vklink;
        public string VkLink
        {
            get { return _vklink; }
            set
            {
                _vklink = value;
                RaisePropertyChanged(() => VkLink);
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
    }
}
