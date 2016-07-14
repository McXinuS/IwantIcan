using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using IWantICan.Core.Models;
using IWantICan.Core.Services;
using MvvmCross.Core.ViewModels;

namespace IWantICan.Core.ViewModels
{
    public class MyCansViewModel : BaseOfferViewModel
    {
        List<CanModel> _cans;
        ICanService _canService;

        public MyCansViewModel(ICanService CanService)
        {
            _canService = CanService;

            Task t = new Task(LoadData);
            t.Start();
        }

        public ICommand ItemSelectedCommand
        {
            get { return new MvxCommand<CanModel>(item => GoDetails(item)); }
        }

        public ICommand ItemEditCommand
        {
            get { return new MvxCommand<CanModel>(item => GoEdit(item)); }
        }

        public List<CanModel> Cans
        {
            get { return _cans; }
            set { _cans = value; RaisePropertyChanged(() => Cans); }
        }

        protected override async void LoadData()
        {
            IsRefreshing = true;

            Cans = await _canService.GetMyCanList();
            IsEmpty = Cans.Count == 0;

            IsRefreshing = false;
        }
    }
}
