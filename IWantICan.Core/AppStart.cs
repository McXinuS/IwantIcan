using System.Threading.Tasks;
using IWantICan.Core.Services;
using IWantICan.Core.ViewModels;
using MvvmCross.Core.ViewModels;
using MvvmCross.Plugins.JsonLocalization;

namespace IWantICan.Core
{
    public class AppStart : MvxNavigatingObject, IMvxAppStart
    {
        readonly IAuthService _authService;

        public AppStart(IAuthService AuthService)
        {
            _authService = AuthService;
        }

        private async Task<bool> TryLogin()
        {
            return await _authService.Login();
        }

        public async void Start(object hint = null)
        {
            if (await TryLogin())
            { 
                ShowViewModel<MainViewModel>();
            }
            else
            {
                ShowViewModel<StartContainerViewModel>();
            }
        }
    }
}
