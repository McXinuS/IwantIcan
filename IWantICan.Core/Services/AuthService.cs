using System.Threading.Tasks;
using IWantICan.Core.Services.Api;
using MvvmCross.Platform;

namespace IWantICan.Core.Services
{
    public class AuthService : IAuthService
    {
        RestManager _restManager;
        ISharedPreferencesService _sharedPreferencesService;
		IUserService _userService;
        private bool _isLogged;

        public string ErrorMessage { get; private set; }

        public AuthService()
        {
            _restManager = new RestManager();
        }

        public async Task<bool> Login()
        {
            var token = SharedPreferencesService.AuthToken;
            IsLogged = !string.IsNullOrWhiteSpace(token);
            return IsLogged;
        }
        
        public async Task<bool> Login(string userName, string password)
        {
            var resp = await _restManager.LoginAsync(userName, password);
            IsLogged = !string.IsNullOrWhiteSpace(resp?.token);
            if (!IsLogged)
                return false;
            SharedPreferencesService.LastLogin = userName;
            SharedPreferencesService.AuthToken = resp.token;
            SharedPreferencesService.UserId = resp.user_id;
            return true;
        }
        
        public async Task<bool> Logout()
        {
            SharedPreferencesService.AuthToken = string.Empty;
            SharedPreferencesService.UserId = 0;
			UserService.OnLogout();
			IsLogged = false;
            return true;
        }

        public bool IsLogged
        {
            get { return _isLogged; }
            set { _isLogged = value; }
        }

        ISharedPreferencesService SharedPreferencesService
        {
            get
            {
                _sharedPreferencesService = _sharedPreferencesService ?? Mvx.Resolve<ISharedPreferencesService>();
                return _sharedPreferencesService;
            }
        }

        IUserService UserService
		{
            get
            {
				_userService = _userService ?? Mvx.Resolve<IUserService>();
                return _userService;
            }
        }
    }
}
