using System.Collections.Generic;
using System.Threading.Tasks;
using IWantICan.Core.Models;

namespace IWantICan.Core.Services
{
    public interface IWantService
    {
        /// <summary>
        /// Create want item.
        /// </summary>
        /// <returns>True is the want has been created, false otherwse.</returns>
        Task<bool> CreateWant(WantModel want);

        /// <summary>
        /// Get a list of wants.
        /// </summary>
        /// <returns>A list of wants.</returns>
        Task<List<WantModel>> GetWantList(int[] catIds);

        /// <summary>
        /// Get a list of wants of a user.
        /// </summary>
        /// <returns>A list of wants.</returns>
        Task<List<WantModel>> GetWantListByUser(int userId);

        /// <summary>
        /// Get a list of wants of the current user.
        /// </summary>
        /// <returns>A list of wants.</returns>
        Task<List<WantModel>> GetMyWantList();

        /// <summary>
        /// Get the information about the want by its ID.
        /// </summary>
        /// <param name="id">ID of the want.</param>
        /// <returns>The want.</returns>
        Task<WantModel> GetWant(int id);

        /// <summary>
        /// Update want item.
        /// </summary>
        /// <returns>True is the want has been updated, false otherwse.</returns>
        Task<bool> UpdateWant(WantModel want);

        /// <summary>
        /// Delete want item.
        /// </summary>
        /// <returns>True is the want has been deleted, false otherwse.</returns>
        Task<bool> DeleteWant(int id);
    }
}
