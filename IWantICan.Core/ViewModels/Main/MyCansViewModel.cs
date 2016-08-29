using System.Threading.Tasks;
using IWantICan.Core.Services;
using IWantICan.Core.Services.Messenger;

namespace IWantICan.Core.ViewModels
{
    public class MyCansViewModel : BaseOfferViewModel
    {
        ICanService _canService;

        public MyCansViewModel(ICanService CanService)
        {
            _canService = CanService;

			Task.Run(LoadData);
		}

        protected override async Task LoadData()
        {
            IsRefreshing = true;

			Offers = await _canService.GetMyCanList();

            IsRefreshing = false;
		}

		protected override void OnOfferMessage(OfferActionMessage message)
		{
			switch (message.Type)
			{
				case MessengerOfferActionType.Create:
				case MessengerOfferActionType.Update:
				case MessengerOfferActionType.Delete:
					Task.Run(LoadData);
					break;
			}
		}
	}
}
