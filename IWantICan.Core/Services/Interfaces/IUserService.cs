using System.Threading.Tasks;
using IWantICan.Core.Models;

namespace IWantICan.Core.Services
{
    public interface IUserService
    {
        /// <summary>
        /// Get the user by his ID.
        /// </summary>
        /// <param name="id">The ID of the user.</param>
        /// <returns>The user.</returns>
        Task<UserModel> GetUser(int id);

        /// <summary>
        /// Create a new user.
        /// </summary>
        /// <param name="user">Information about the user.</param>
        /// <returns>True if the operation is successed.</returns>
        Task<bool> CreateUser(UserModel user);

        /// <summary>
        /// Update information about the user.
        /// </summary>
        /// <param name="user">The new information about the user.
        /// Must contain only changed fields.</param>
        /// <returns></returns>
        Task<bool> UpdateUser(UserModel user);

        /// <summary>
        /// Get the information about the current user
        /// </summary>
        /// <returns></returns>
        Task<UserModel> GetCurrentUser();
    }
}
