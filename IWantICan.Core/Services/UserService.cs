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

        private UserModel _currentUser;

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
			// get user info
	        if (_currentUser == null)
			{
				var sharedPref = Mvx.Resolve<ISharedPreferencesService>();
				var id = sharedPref.UserId;
				if (id < 0)
					return null;
				_currentUser = await _restManager.GetUserAsync(id);
			}

			// clone loaded user
			if (_currentUser != null)
			{
				return new UserModel
				{
					avatar = _currentUser.avatar,
					email = _currentUser.email,
					id = _currentUser.id,
					login = _currentUser.login,
					name = _currentUser.name,
					password = _currentUser.password,
					phone = _currentUser.phone,
					surname = _currentUser.surname,
					vkLink = _currentUser.vkLink
				};
			}

			return null;
        }

	    public void OnLogout()
	    {
		    _currentUser = null;
	    }
    }
}
