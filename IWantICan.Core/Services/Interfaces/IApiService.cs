using System.Collections.Generic;
using System.Threading.Tasks;
using IWantICan.Core.Models;

namespace IWantICan.Core.Services.Api
{
	public interface IApiService
    {
        /* ---AUTH--- */

        /// <summary>
        /// Login using login+password pair.
        /// </summary>
        /// <returns>Token or null.</returns>
	    Task<TokenResponseModel> LoginAsync(string login, string pass);

        /* ---USER--- */

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <returns>New user info or null.</returns>
	    Task<UserModel> CreateUserAsync(UserModel user);

        /// <summary>
        /// Get information about the user.
        /// </summary>
        /// <returns>User info or null.</returns>
	    Task<UserModel> GetUserAsync(int id, string token);

        /// <summary>
        /// Update information about the user.
        /// </summary>
        /// <returns>True if update was successful, false otherwise.</returns>
	    Task<bool> UpdateUserAsync(UserModelWithToken user);

        /* ---CANS--- */

        /// <summary>
        /// Add the can to DB.
        /// </summary>
        /// <returns>True if the can has been added successfully.</returns>
	    Task<bool> AddCanAsync(OfferModelWithToken can);

        /// <summary>
        /// Get the information about the can from DB by ID.
        /// </summary>
        /// <returns>The can or null.</returns>
	    Task<OfferModel> GetCanAsync(int id, string token);

        /// <summary>
        /// Get all cans.
        /// </summary>
        /// <returns>A list of cans or null.</returns>
        Task<List<OfferModel>> GetCanListAll(string token);

        /// <summary>
        /// Get cans from DB by category ID.
        /// </summary>
        /// <returns>A list of cans or null.</returns>
	    Task<List<OfferModel>> GetCanListByCategoryAsync(int catId, string token);

        /// <summary>
        /// Get cans from DB by user ID.
        /// </summary>
        /// <returns>A list of cans or null.</returns>
	    Task<List<OfferModel>> GetCanListByUserAsync(int catId, string token);

        /// <summary>
        /// Update the can on DB.
        /// </summary>
        /// <returns>True if the can has been added successfully.</returns>
        Task<bool> UpdateCanAsync(OfferModelWithToken can);

        /// <summary>
        /// Removes the can from DB.
        /// </summary>
        /// <returns>True if the can has been deleted successfully.</returns>
	    Task<bool> DeleteCanAsync(int id, string token);

        /* ---WANTS--- */

        /// <summary>
        /// Add the want to DB.
        /// </summary>
        /// <returns>True if the want has been added successfully.</returns>
        Task<bool> AddWantAsync(OfferModelWithToken want);

        /// <summary>
        /// Get the information about the want from DB by ID.
        /// </summary>
        /// <returns>The want or null.</returns>
	    Task<OfferModel> GetWantAsync(int id, string token);

        /// <summary>
        /// Get wants from DB by category ID.
        /// </summary>
        /// <returns>A list of wants or null.</returns>
	    Task<List<OfferModel>> GetWantListByCategoryAsync(int id, string token);

        /// <summary>
        /// Get wants from DB by user ID.
        /// </summary>
        /// <returns>A list of wants or null.</returns>
	    Task<List<OfferModel>> GetWantListByUserAsync(int id, string token);

        /// <summary>
        /// Get all wants.
        /// </summary>
        /// <returns>A list of wants or null.</returns>
        Task<List<OfferModel>> GetWantListAllAsync(string token);

        /// <summary>
        /// Update the want on DB.
        /// </summary>
        /// <returns>True if the want has been added successfully.</returns>
        Task<bool> UpdateWantAsync(OfferModelWithToken want);

        /// <summary>
        /// Removes the want from DB.
        /// </summary>
        /// <returns>True if the want has been deleted successfully.</returns>
	    Task<bool> DeleteWantAsync(int id, string token);

        /// <summary>
        /// Get categories from DB.
        /// </summary>
        /// <returns>A list of categories or null.</returns>
        Task<List<CategoryModel>> GetCategoryListAsync();
    }
}
