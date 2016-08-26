using System.Threading.Tasks;
using IWantICan.Core.Services;

namespace IWantICan.Core.ViewModels
{
    public class AllCansViewModel : BaseOfferViewModel
    {
        ICanService _canService;
        
        public AllCansViewModel(ICanService canService)
        {
            _canService = canService;

            Task t = new Task(LoadData);
            t.Start();
        }
		
        protected override async void LoadData()
        {
            IsRefreshing = true;

			Offers = await _canService.GetCanList(SelectedCategory);

            IsRefreshing = false;
        }
    }
}
