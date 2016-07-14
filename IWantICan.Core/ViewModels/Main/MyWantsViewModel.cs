using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using IWantICan.Core.Models;
using IWantICan.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IWantICan.Core.ViewModels
{
    public class MyWantsViewModel : BaseOfferViewModel
    {
        List<WantModel> _wants;
        IWantService _wantService;

        public MyWantsViewModel(IWantService WantService)
        {
            _wantService = WantService;
            Task t = new Task(LoadData);
            t.Start();
        }

        public ICommand ItemSelectedCommand
        {
            get { return new MvxCommand<WantModel>(item => GoDetails(item)); }
        }

        public ICommand ItemEditCommand
        {
            get { return new MvxCommand<WantModel>(item => GoEdit(item)); }
        }

        public List<WantModel> Wants
        {
            get { return _wants; }
            set { _wants = value; RaisePropertyChanged(() => Wants); }
        }

        protected override async void LoadData()
        {
            IsRefreshing = true;
            
            Wants = await _wantService.GetMyWantList();
            IsEmpty = Wants.Count == 0;

            IsRefreshing = false;
        }
    }
}
