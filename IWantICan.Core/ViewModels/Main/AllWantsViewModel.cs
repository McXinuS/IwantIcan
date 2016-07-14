using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using IWantICan.Core.Models;
using IWantICan.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IWantICan.Core.ViewModels
{
    public class AllWantsViewModel : BaseOfferViewModel
    {
        IWantService _wantService;

        List<WantModel> _wants;
        public List<WantModel> Wants
        {
            get { return _wants; }
            set { _wants = value; RaisePropertyChanged(() => Wants); }
        }

        public AllWantsViewModel(IWantService wantService)
        {
            _wantService = wantService;

            Task t = new Task(LoadData);
            t.Start();
        }

        public ICommand ItemSelectedCommand
        {
            get { return new MvxCommand<WantModel>(item => GoDetails(item)); }
        }

        protected override async void LoadData()
        {
            IsRefreshing = true;

            Wants = await _wantService.GetWantList(SelectedCategory);
            IsEmpty = Wants.Count == 0;

            IsRefreshing = false;
        }
    }
}
