using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using IWantICan.Core.Interfaces;
using IWantICan.Core.Models;
using IWantICan.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IWantICan.Core.ViewModels
{
    public class AllWantsViewModel : BaseOfferViewModel
    {
        IDialogService _dialogService;
        IWantService _wantService;
        List<WantModel> _wants;

        public AllWantsViewModel(IWantService wantService, IDialogService dialogService)
        {
            _wantService = wantService;
            _dialogService = dialogService;

            Task t = new Task(LoadData);
            t.Start();
        }

        public ICommand ItemSelectedCommand
        {
            get { return new MvxCommand<WantModel>(item => GoDetails(item)); }
        }

        public List<WantModel> Wants
        {
            get { return _wants; }
            set { _wants = value; RaisePropertyChanged(() => Wants); }
        }

        protected override async void LoadData()
        {
            IsRefreshing = true;

            if (SelectedCategory?.Length == 0)
            {
                _dialogService.Alert(Constants.DialogFilterFailed, Constants.DialogTitleError, "ОК");
                return;
            }

            Wants = await _wantService.GetWantList(SelectedCategory);
            IsEmpty = Wants.Count == 0;

            IsRefreshing = false;
        }
    }
}
