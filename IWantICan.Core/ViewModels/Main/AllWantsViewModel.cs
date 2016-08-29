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

			Task.Run(LoadData);
		}

        protected override async Task LoadData()
        {
            IsRefreshing = true;

			Offers = await _wantService.GetWantList(SelectedCategory);

            IsRefreshing = false;
        }
    }
}
