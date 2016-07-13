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

        public async Task<bool> CreateWant(WantModel want)
        {
            return await _restManager.AddWantAsync(want);
        }

        // TODO allow multiple categories
        public async Task<List<WantModel>> GetWantList(int[] catIds)
        {
            return await _restManager.GetWantListByCategoryAsync(catIds[0]);
        }

        public async Task<List<WantModel>> GetWantListByUser(int userId)
        {
            return await _restManager.GetWantListByUserAsync(userId);
        }

        public async Task<List<WantModel>> GetMyWantList()
        {
            var sp = Mvx.Resolve<ISharedPreferencesService>();
            var myId = sp.UserId;
            return await _restManager.GetWantListByUserAsync(myId);
        }

        public async Task<WantModel> GetWant(int id)
        {
            return await _restManager.GetWantAsync(id);
        }
    }
}
