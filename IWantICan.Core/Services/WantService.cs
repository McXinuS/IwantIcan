using System.Collections.Generic;
using System.Threading.Tasks;
using IWantICan.Core.Models;
using IWantICan.Core.Services.Api;
using MvvmCross.Platform;

namespace IWantICan.Core.Services
{
    public class WantService : IWantService
    {
        RestManager _restManager;

        public WantService()
        {
            _restManager = new RestManager();
        }

        public async Task<bool> CreateWant(OfferModel want)
        {
            return await _restManager.AddWantAsync(want);
        }
        
        public async Task<List<OfferModel>> GetWantList(int[] catIds)
        {
            return await _restManager.GetWantListByCategoryAsync(catIds);
        }

        public async Task<List<OfferModel>> GetWantListByUser(int userId)
        {
            return await _restManager.GetWantListByUserAsync(userId);
        }

        public async Task<List<OfferModel>> GetMyWantList()
        {
            var sp = Mvx.Resolve<ISharedPreferencesService>();
            var myId = sp.UserId;
            return await _restManager.GetWantListByUserAsync(myId);
        }

        public async Task<OfferModel> GetWant(int id)
        {
            return await _restManager.GetWantAsync(id);
        }

        public async Task<bool> UpdateWant(OfferModel want)
        {
            return await _restManager.UpdateWantAsync(want);
        }

        public async Task<bool> DeleteWant(int id)
        {
            return await _restManager.DeleteWantAsync(id);
        }
    }
}
