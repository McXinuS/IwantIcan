using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using IWantICan.Core.Interfaces;
using IWantICan.Core.Models;
using IWantICan.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IWantICan.Core.ViewModels
{
    public class AllCansViewModel : BaseOfferViewModel
    {
        IDialogService _dialogService;
        ICanService _canService;
        List<CanModel> _cans;

        public AllCansViewModel(ICanService canService, IDialogService dialogService)
        {
            _canService = canService;
            _dialogService = dialogService;

            Task t = new Task(LoadData);
            t.Start();
        }

        public ICommand ItemSelectedCommand
        {
            get { return new MvxCommand<CanModel>(item => GoDetails(item)); }
        }

        public List<CanModel> Cans
        {
            get { return _cans; }
            set { _cans = value; RaisePropertyChanged(() => Cans); }
        }

        protected override async void LoadData()
        {
            IsRefreshing = true;

            /*if (SelectedCategory?.Length == 0)
            {
                _dialogService.Alert(Constants.DialogFilterFailed, Constants.DialogTitleError, "ОК");
                return;
            }*/

            Cans = await _canService.GetCanList(SelectedCategory);
            IsEmpty = Cans.Count == 0;

            IsRefreshing = false;
        }
    }
}
