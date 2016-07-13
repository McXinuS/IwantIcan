using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IWantICan.Core.Models;
using MvvmCross.Platform;

namespace IWantICan.Core.Services.Api
{
    /// <summary>
    /// Handles API service and returns suitable results.
    /// </summary>
	public class RestManager
	{
		IApiService _apiService;
        IApiService ApiService
        {
            get
            {
                _apiService = _apiService ?? Mvx.Resolve<IApiService>();
                return _apiService;
            }
        }
        
		string _token;
        /// <summary>
        /// Contains token. Readonly.
        /// </summary>
        public string Token
        {
            get
            {
                if (String.IsNullOrEmpty(_token))
                {
                    var sharedPreferencesService = Mvx.Resolve<ISharedPreferencesService>();
                    _token = sharedPreferencesService.AuthToken;
                }
                return _token;
            }
        }

        /// <summary>
        /// Login using login-password pair.
        /// </summary>
        /// <returns>TokenResponseModel if success, null otherwise.</returns>
        public async Task<TokenResponseModel> LoginAsync(string login, string pass)
		{
            return await ApiService.LoginAsync(login, pass);
        }

        /* ---USER--- */

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <returns>True if the user is created.</returns>
        public async Task<bool> CreateUserAsync(UserModel user)
        {
            UserModel res = await ApiService.CreateUserAsync(user);
            return res != null;
        }

        /// <summary>
        /// Get information about the user.
        /// </summary>
        /// <param name="id">ID of the user</param>
        /// <returns>User model if success, null otherwise.</returns>
        public async Task<UserModel> GetUserAsync(int id)
        {
            return await ApiService.GetUserAsync(id, Token);
        }

        /// <summary>
        /// Update information about the user.
        /// </summary>
        /// <returns>Token string if success, string.Empty otherwise.</returns>
        public async Task<bool> UpdateUserAsync(UserModel user)
        {
            if (user == null)
                return false;

            UserModelWithToken tokenedModel = user.ToTokenedModel(Token);
            
            return await ApiService.UpdateUserAsync(tokenedModel);
        }
        
        /* ---CAN--- */

        /// <summary>
        /// Create a new can item.
        /// </summary>
        /// <returns>True if the can is created.</returns>
        public async Task<bool> AddCanAsync(CanModel can)
        {
            if (can == null)
                return false;

            CanModelWithToken tokenedModel = can.ToTokenedModel(Token);

            return await ApiService.AddCanAsync(tokenedModel);
        }

        /// <summary>
        /// Get a can by ID.
        /// </summary>
        /// <returns>The can or null if error.</returns>
	    public async Task<CanModel> GetCanAsync(int id)
        {
            return await ApiService.GetCanAsync(id, Token);
        }

        /// <summary>
        /// Get a list of cans by categotyId.
        /// </summary>
        /// <returns>The list of cans or null if error.</returns>
        public async Task<List<CanModel>> GetCanListByCategoryAsync(int catid)
        {
            return await ApiService.GetCanListByCategoryAsync(catid, Token);
        }

        /// <summary>
        /// Get a list of cans by userId.
        /// </summary>
        /// <returns>The list of cans or null if error.</returns>
        public async Task<List<CanModel>> GetCanListByUserAsync(int userId)
        {
            return await ApiService.GetCanListByUserAsync(userId, Token);
        }

        /// <summary>
        /// Update the can.
        /// </summary>
        /// <returns>True if the can is updated.</returns>
        public async Task<bool> UpdateCanAsync(CanModel can)
        {
            if (can == null)
                return false;

            CanModelWithToken tokenedModel = can.ToTokenedModel(Token);

            return await ApiService.UpdateCanAsync(tokenedModel);
        }

        /// <summary>
        /// Detele can by ID.
        /// </summary>
        /// <returns>True if the can is deleted.</returns>
        public async Task<bool> DeleteCanAsync(int id)
        {
            return await ApiService.DeleteCanAsync(id, Token);
        }
        
        /* ---WANT--- */

        /// <summary>
        /// Create a new want item.
        /// </summary>
        /// <returns>True if the want is created.</returns>
        public async Task<bool> AddWantAsync(WantModel want)
        {
            if (want == null)
                return false;

            WantModelWithToken tokenedModel = want.ToTokenedModel(Token);

            return await ApiService.AddWantAsync(tokenedModel);
        }

        /// <summary>
        /// Get a want by ID.
        /// </summary>
        /// <returns>The want or null if error.</returns>
	    public async Task<WantModel> GetWantAsync(int id)
        {
            return await ApiService.GetWantAsync(id, Token);
        }

        /// <summary>
        /// Get a list of wants by categotyId.
        /// </summary>
        /// <returns>The list of wants or empty list if error.</returns>
        public async Task<List<WantModel>> GetWantListByCategoryAsync(int catid)
        {
            return await ApiService.GetWantListByCategoryAsync(catid, Token);
        }

        /// <summary>
        /// Get a list of wants by userId.
        /// </summary>
        /// <returns>The list of wants or null if error.</returns>
        public async Task<List<WantModel>> GetWantListByUserAsync(int userId)
        {
            return await ApiService.GetWantListByUserAsync(userId, Token);
        }

        /// <summary>
        /// Update the want.
        /// </summary>
        /// <returns>True if the want is updated.</returns>
        public async Task<bool> UpdateWantAsync(WantModel want)
        {
            if (want == null)
                return false;

            WantModelWithToken tokenedModel = want.ToTokenedModel(Token);

            return await ApiService.UpdateWantAsync(tokenedModel);
        }

        /// <summary>
        /// Detele want by ID.
        /// </summary>
        /// <returns>True if the want is deleted.</returns>
        public async Task<bool> DeleteWantAsync(int id)
        {
            return await ApiService.DeleteWantAsync(id, Token);
        }

        /* ---CATEGORY--- */

        /// <summary>
        /// Get a list of categories.
        /// </summary>
        /// <returns>The list of categories or empty list if error.</returns>
        public async Task<List<CategoryModel>> GetCategoryListAsync()
        {
            return await ApiService.GetCategoryListAsync();
        }
    }
}
