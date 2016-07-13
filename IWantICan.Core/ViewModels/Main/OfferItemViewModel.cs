using System;
using System.Threading.Tasks;
using System.Windows.Input;
using IWantICan.Core.Helpers;
using IWantICan.Core.Interfaces;
using IWantICan.Core.Models;
using IWantICan.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IWantICan.Core.ViewModels
{
    public class OfferItemViewModel : BaseViewModel
    {
        IUserService _userService;
        IDialogService _dialogService;

        private string _name;
        private string _description;
        private string _imgurl;
        private DateTime _createdAt;

        public string Name
        {
            get { return _name; }
            set { _name = value; RaisePropertyChanged(() => Name); }
        }

        public string Description
        {
            get { return _description; }
            set { _description = value; RaisePropertyChanged(() => Description); }
        }

        public string Imgurl
        {
            get { return _imgurl; }
            set { _imgurl = value; RaisePropertyChanged(() => Imgurl); }
        }

        public DateTime CreatedAt
        {
            get { return _createdAt; }
            set { _createdAt = value; RaisePropertyChanged(() => CreatedAt); }
        }

        UserModel _user;

        public OfferItemViewModel(
            IUserService UserService,
            IDialogService DialogService)
        {
            _userService = UserService;
            _dialogService = DialogService;
        }

        public void Init(string offer, string type)
        {
            Task t = new Task(() => LoadData(offer, type));
            t.Start();
        }

        private async void LoadData(string offer, string type)
        {
            if (type.Contains("CanModel"))
            {
                var Can = offer.Deserialize<CanModel>();
                if (Can != null)
                {
                    Name = Can.name;
                    Description = Can.description;
                    Imgurl = Can.imgurl;
                    CreatedAt = Can.createdAt;
                    User = await _userService.GetUser(Can.UserModelId);
                }
            }
            else
            {
                var Want = offer.Deserialize<WantModel>();
                if (Want != null)
                {
                    Name = Want.name;
                    Description = Want.description;
                    Imgurl = Want.imgurl;
                    CreatedAt = Want.createdAt;
                    User = await _userService.GetUser(Want.UserModelId);
                }
            }
        }

        public UserModel User
        {
            get { return _user; }
            set { _user = value; RaisePropertyChanged(() => User); }
        }

        public IMvxCommand GoProfileViewCommand
        {
            get
            {
                return new MvxCommand(GoProfileView);
            }
        }

        private void GoProfileView()
        {
            object param = new { user = User.Serialize() };
            ShowViewModel<OffererProfileViewModel>(param);
        }

        public ICommand ShowContactsCommand
        {
            get { return new MvxCommand(() => _dialogService.ContactDialog(User)); }
        }
    }
}
