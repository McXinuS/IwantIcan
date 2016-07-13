using System.Threading.Tasks;

namespace IWantICan.Core.Services
{
    public interface IAuthService
    {
        /// <summary>
        /// Login using internally saved token.
        /// </summary>
        /// <returns>True if the operation is successed.</returns>
        Task<bool> Login();

        /// <summary>
        /// Login using login-password pair.
        /// </summary>
        /// <returns>True if the operation is successed.</returns>
        Task<bool> Login(string userName, string password);

        /// <summary>
        /// Logout from the system.
        /// </summary>
        /// <returns>True if the operation is successed.</returns>
        Task<bool> Logout();

        /// <summary>
        /// Shows whether the user is logged to the system.
        /// </summary>
        bool IsLogged { get; set; }
    }
}