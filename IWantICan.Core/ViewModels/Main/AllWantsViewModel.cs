using System.Threading.Tasks;
using IWantICan.Core.Services;

namespace IWantICan.Core.ViewModels
{
    public class AllWantsViewModel : BaseOfferViewModel
    {
        IWantService _wantService;
		
        public AllWantsViewModel(IWantService wantService)
        {
            _wantService = wantService;

            Task t = new Task(LoadData);
            t.Start();
        }

        protected override async void LoadData()
        {
            IsRefreshing = true;

			Offers = await _wantService.GetWantList(SelectedCategory);
            IsEmpty = Offers.Count == 0;

            IsRefreshing = false;
        }
    }
}
