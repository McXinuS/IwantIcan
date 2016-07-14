using System.Collections.Generic;
using System.Threading.Tasks;
using IWantICan.Core.Models;
using IWantICan.Core.Services.Api;
using MvvmCross.Platform;

namespace IWantICan.Core.Services.Offers
{
    class CanService : ICanService
    {
        RestManager _restManager;

        public CanService()
        {
            _restManager = new RestManager();
        }

        public async Task<bool> CreateCan(CanModel can)
        {
            return await _restManager.AddCanAsync(can);
        }
        
        public async Task<List<CanModel>> GetCanList(int[] catIds)
        {
            return await _restManager.GetCanListByCategoryAsync(catIds);
        }

        public async Task<List<CanModel>> GetCanListByUser(int userId)
        {
            return await _restManager.GetCanListByUserAsync(userId);
        }

        public async Task<List<CanModel>> GetMyCanList()
        {
            var sp = Mvx.Resolve<ISharedPreferencesService>();
            var myId = sp.UserId;
            return await _restManager.GetCanListByUserAsync(myId);
        }

        public async Task<CanModel> GetCan(int id)
        {
            return await _restManager.GetCanAsync(id);
        }

        public async Task<bool> UpdateCan(CanModel can)
        {
            return await _restManager.UpdateCanAsync(can);
        }

        public async Task<bool> DeleteCan(int id)
        {
            return await _restManager.DeleteCanAsync(id);
        }
    }
}
