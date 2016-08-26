using System.Threading.Tasks;
using IWantICan.Core.Services;

namespace IWantICan.Core.ViewModels
{
    public class MyWantsViewModel : BaseOfferViewModel
    {
	    readonly IWantService _wantService;

        public MyWantsViewModel(IWantService wantService)
        {
            _wantService = wantService;

            Task t = new Task(LoadData);
            t.Start();
        }

        protected override async void LoadData()
        {
            IsRefreshing = true;

			Offers = await _wantService.GetMyWantList();

            IsRefreshing = false;
        }
    }
}
