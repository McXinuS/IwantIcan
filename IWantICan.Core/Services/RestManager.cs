using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IWantICan.Core.Models;
using MvvmCross.Platform;
using System.Linq;
using MvvmCross.Platform.Core;

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
		
		ISharedPreferencesService _sharedPreferencesService;
        ISharedPreferencesService SharedPreferencesService
        {
            get
            {
                _sharedPreferencesService = _sharedPreferencesService ?? Mvx.Resolve<ISharedPreferencesService>();
                return _sharedPreferencesService;
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
                return SharedPreferencesService.AuthToken;
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
        public async Task<bool> AddCanAsync(OfferModel can)
        {
            if (can == null)
                return false;

            OfferModelWithToken tokenedModel = can.ToTokenedModel(Token);

            return await ApiService.AddCanAsync(tokenedModel);
        }

        /// <summary>
        /// Get a can by ID.
        /// </summary>
        /// <returns>The can or null if error.</returns>
	    public async Task<OfferModel> GetCanAsync(int id)
        {
			var can = await ApiService.GetCanAsync(id, Token);
			can.type = OfferType.Can;

	        return can;
        }

        /// <summary>
        /// Get a list of all cans.
        /// </summary>
        /// <returns>The list of cans or null if error.</returns>
        public async Task<List<OfferModel>> GetCanListAll()
        {
            var list = await ApiService.GetCanListAll(Token);

            list = list
				.OrderByDescending(o => o.createdAt)
				.Select(c => { c.type = OfferType.Can; return c; })
				.ToList();

            return list;
        }

        /// <summary>
        /// Get a list of cans by categotyId.
        /// </summary>
        /// <returns>The list of cans or null if error.</returns>
        public async Task<List<OfferModel>> GetCanListByCategoryAsync(int[] catIds)
        {
            if (catIds == null || catIds.Length == 0)
                return await GetCanListAll();

	        var size = catIds.Length;

			// load items asyncroniously
			Task[] tasks = new Task[size];

	        var results = new List<OfferModel>[size];

			for (var i = 0; i < size; i++)
			{
				var index = i;

				var task = Task.Run(async () =>
				{
					var tempList = await ApiService.GetCanListByCategoryAsync(catIds[index], Token);
					results[index] = tempList;
				});
				tasks[index] = task;
			}

			await Task.WhenAll(tasks);

	        var list = results.SelectMany(x => x).ToList();
			list = list
				.OrderByDescending(o => o.createdAt)
				.Select(c => { c.type = OfferType.Can; return c; })
				.ToList();

            return list;
        }

        /// <summary>
        /// Get a list of cans by userId.
        /// </summary>
        /// <returns>The list of cans or null if error.</returns>
        public async Task<List<OfferModel>> GetCanListByUserAsync(int userId)
        {
            var list = await ApiService.GetCanListByUserAsync(userId, Token);

            list = list
				.OrderByDescending(o => o.createdAt)
				.Select(c => { c.type = OfferType.Can; return c; })
				.ToList();

            return list;
        }

        /// <summary>
        /// Update the can.
        /// </summary>
        /// <returns>True if the can is updated.</returns>
        public async Task<bool> UpdateCanAsync(OfferModel can)
        {
            if (can == null)
                return false;

            OfferModelWithToken tokenedModel = can.ToTokenedModel(Token);

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
        public async Task<bool> AddWantAsync(OfferModel want)
        {
            if (want == null)
                return false;

			OfferModelWithToken tokenedModel = want.ToTokenedModel(Token);

            return await ApiService.AddWantAsync(tokenedModel);
        }

        /// <summary>
        /// Get a want by ID.
        /// </summary>
        /// <returns>The want or null if error.</returns>
	    public async Task<OfferModel> GetWantAsync(int id)
        {
            var want =  await ApiService.GetWantAsync(id, Token);
			want.type = OfferType.Want;
	        return want;
        }

        /// <summary>
        /// Get a list of all wants.
        /// </summary>
        /// <returns>The list of wants or null if error.</returns>
        public async Task<List<OfferModel>> GetWantListAll()
        {
            var list = await ApiService.GetWantListAllAsync(Token);

            list = list
				.OrderByDescending(o => o.createdAt)
				.Select(c => { c.type = OfferType.Want; return c; })
				.ToList();
			
			return list;
        }

        /// <summary>
        /// Get a list of wants by categotyId.
        /// </summary>
        /// <returns>The list of wants or empty list if error.</returns>
        public async Task<List<OfferModel>> GetWantListByCategoryAsync(int[] catIds)
		{
			if (catIds == null || catIds.Length == 0)
				return await GetWantListAll();

			var size = catIds.Length;

			// load items asyncroniously
			Task[] tasks = new Task[size];

			var results = new List<OfferModel>[size];

			for (var i = 0; i < size; i++)
			{
				var index = i;

				var task = Task.Run(async () =>
				{
					var tempList = await ApiService.GetWantListByCategoryAsync(catIds[index], Token);

					results[index] = tempList;
				});
				tasks[index] = task;
			}

			await Task.WhenAll(tasks);

			var list = results.SelectMany(x => x).ToList();
			list = list
				.OrderByDescending(o => o.createdAt)
				.Select(c => { c.type = OfferType.Want; return c; })
				.ToList();

			return list;
		}

        /// <summary>
        /// Get a list of wants by userId.
        /// </summary>
        /// <returns>The list of wants or null if error.</returns>
        public async Task<List<OfferModel>> GetWantListByUserAsync(int userId)
        {
            var list = await ApiService.GetWantListByUserAsync(userId, Token);

			list = list
				.OrderByDescending(o => o.createdAt)
				.Select(c => { c.type = OfferType.Want; return c; })
				.ToList();

			return list;
        }

        /// <summary>
        /// Update the want.
        /// </summary>
        /// <returns>True if the want is updated.</returns>
        public async Task<bool> UpdateWantAsync(OfferModel want)
        {
            if (want == null)
                return false;

			OfferModelWithToken tokenedModel = want.ToTokenedModel(Token);

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
