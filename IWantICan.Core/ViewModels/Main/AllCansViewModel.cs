using System.Threading.Tasks;
using IWantICan.Core.Services;
using IWantICan.Core.Services.Messenger;

namespace IWantICan.Core.ViewModels
{
    public class AllCansViewModel : BaseOfferViewModel
    {
        ICanService _canService;
        
        public AllCansViewModel(ICanService canService)
        {
            _canService = canService;

			Task.Run(LoadData);
		}
		
        protected override async Task LoadData()
        {
            IsRefreshing = true;

			Offers = await _canService.GetCanList(SelectedCategory);

            IsRefreshing = false;
        }
    }
}
