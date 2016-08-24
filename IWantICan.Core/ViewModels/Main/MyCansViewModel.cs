using System.Threading.Tasks;
using IWantICan.Core.Services;

namespace IWantICan.Core.ViewModels
{
    public class MyCansViewModel : BaseOfferViewModel
    {
        ICanService _canService;

        public MyCansViewModel(ICanService CanService)
        {
            _canService = CanService;

            Task t = new Task(LoadData);
            t.Start();
        }

        protected override async void LoadData()
        {
            IsRefreshing = true;

			Offers = await _canService.GetMyCanList();
            IsEmpty = Offers.Count == 0;

            IsRefreshing = false;
        }
    }
}
