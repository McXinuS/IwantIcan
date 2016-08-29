using System.Threading.Tasks;
using IWantICan.Core.Services;
using IWantICan.Core.Services.Messenger;

namespace IWantICan.Core.ViewModels
{
    public class MyWantsViewModel : BaseOfferViewModel
    {
	    readonly IWantService _wantService;

        public MyWantsViewModel(IWantService wantService)
        {
            _wantService = wantService;

			Task.Run(LoadData);
		}

        protected override async Task LoadData()
        {
            IsRefreshing = true;

			Offers = await _wantService.GetMyWantList();

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
