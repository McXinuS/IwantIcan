using System.Threading.Tasks;
using IWantICan.Core.Helpers;
using IWantICan.Core.Models;
using IWantICan.Core.Services.Api;
using MvvmCross.Platform;

namespace IWantICan.Core.Services
{
    public class UserService : IUserService
    {
        RestManager _restManager;

        UserModel _currentUser;

        public UserService()
        {
            _restManager = new RestManager();
            _currentUser = null;
        }

        public async Task<UserModel> GetUser(int id)
        {
            return await _restManager.GetUserAsync(id);
        }

        public async Task<bool> CreateUser(UserModel user)
        {
            var okCreate = await _restManager.CreateUserAsync(user);

            if (!okCreate)
                return false;
            _currentUser = user;

            var authService = Mvx.Resolve <IAuthService>();
            var okLogin = await authService.Login(user.login, user.password);
            return okLogin;
        }

        public async Task<bool> UpdateUser(UserModel user)
        {
            var ok = await _restManager.UpdateUserAsync(user);

            if (ok)
            {
                _currentUser = user;
                await _currentUser.UpdateAvatar();
            }

            return ok;
        }

        public async Task<UserModel> GetCurrentUser()
        {
            if (_currentUser != null)
                return _currentUser;

            var sharedPref = Mvx.Resolve<ISharedPreferencesService>();
            var id = sharedPref.UserId;
            if (id < 0)
                return null;

            _currentUser = await _restManager.GetUserAsync(id);
            return _currentUser;
        }
    }
}
