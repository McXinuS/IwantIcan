using System.Collections.Generic;
using System.Threading.Tasks;
using IWantICan.Core.Models;

namespace IWantICan.Core.Services
{
    public interface ICanService
    {
        /// <summary>
        /// Create can item.
        /// </summary>
        /// <returns>True is the can has been created, false otherwse.</returns>
        Task<bool> CreateCan(CanModel can);

        /// <summary>
        /// Get a list of cans.
        /// </summary>
        /// <returns>A list of cans.</returns>
        Task<List<CanModel>> GetCanList(int[] catIds);

        /// <summary>
        /// Get a list of cans of a user.
        /// </summary>
        /// <returns>A list of cans.</returns>
        Task<List<CanModel>> GetCanListByUser(int userId);

        /// <summary>
        /// Get a list of cans of the current user.
        /// </summary>
        /// <returns>A list of cans.</returns>
        Task<List<CanModel>> GetMyCanList();

        /// <summary>
        /// Get the information about the can by its ID.
        /// </summary>
        /// <param name="id">ID of the can.</param>
        /// <returns>The can.</returns>
        Task<CanModel> GetCan(int id);

        /// <summary>
        /// Update can item.
        /// </summary>
        /// <returns>True is the can has been updated, false otherwse.</returns>
        Task<bool> UpdateCan(CanModel can);

        /// <summary>
        /// Delete can item.
        /// </summary>
        /// <returns>True is the can has been deleted, false otherwse.</returns>
        Task<bool> DeleteCan(int id);
    }
}
