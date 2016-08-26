using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using MvvmCross.Core.ViewModels;
using IWantICan.Core.Helpers;
using IWantICan.Core.Interfaces;
using IWantICan.Core.Models;
using IWantICan.Core.Services;
using MvvmCross.Platform;
using MvvmCross.Plugins.Messenger;

namespace IWantICan.Core.ViewModels
{
    public abstract class BaseOfferViewModel : BaseViewModel
    {
	    readonly IMvxMessenger _messenger;
		private readonly MvxSubscriptionToken _token;

	    readonly ICategoryService _categoryService;
        protected int[] SelectedCategory { get { return _categoryService.Selected; } }

        private bool _isRefreshing;
        private bool _isEmpty;
		private List<OfferModel> _offers;

		protected int MyId;

        protected BaseOfferViewModel()
        {
            _categoryService = Mvx.Resolve<ICategoryService>();

			_messenger = Mvx.Resolve<IMvxMessenger>();
            _token = _messenger.Subscribe<FilterDoneMessage>(ReloadCommand.Execute);

			Task.Run(LoadUserId);

			IsEmpty = true;
        }

	    private async Task LoadUserId()
		{
			var us = Mvx.Resolve<IUserService>();
			var curUser = await us.GetCurrentUser();
			MyId = curUser.id;
		}

        protected abstract void LoadData();
        public ICommand ReloadCommand
        {
            get
            {
                return new MvxCommand(() =>
                {
                    Task t = new Task(LoadData);
                    t.Start();
                });
            }
		}

		public ICommand ItemSelectedCommand
		{
			get { return new MvxCommand<OfferModel>(item => GoDetails(item)); }
		}
		protected void GoDetails(OfferModel item)
        {
	        if (item == null)
		        return;
			
			var param = new { offer = item.Serialize() };
			ShowViewModel<OfferDetailsContainerViewModel>(param);
        }

		public ICommand ItemEditCommand
		{
			get { return new MvxCommand<OfferModel>(item => GoEdit(item)); }
		}
		protected void GoEdit(OfferModel item)
		{
			if (item?.UserModelId != MyId)
				return;

			var param = new {offer = item.Serialize()};
			ShowViewModel<EditOfferViewModel>(param);
        }

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set { _isRefreshing = value; RaisePropertyChanged(() => IsRefreshing); }
		}

        public bool IsEmpty
        {
            get { return _isEmpty; }
            set { _isEmpty = value; RaisePropertyChanged(() => IsEmpty); }
        }

		public List<OfferModel> Offers
		{
			get { return _offers; }
			set
			{
				_offers = value;
				RaisePropertyChanged(() => Offers);
				IsEmpty = Offers.Count == 0;
			}
		}

		public ICommand MessengerUnsibscribeCommand
		{
			get { return new MvxCommand(() => _messenger.Unsubscribe<FilterDoneMessage>(_token)); }
		}
	}
}
