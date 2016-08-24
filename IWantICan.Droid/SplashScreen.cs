using Android.App;
using Android.OS;
using MvvmCross.Droid.Views;

namespace IWantICan.Droid
{
    [Activity(Label = "I Want I Can",
        Theme = "@style/AppTheme.Splash",
        MainLauncher = true,
        NoHistory = true)]
    public class SplashScreenView : MvxSplashScreenActivity
    {
        public SplashScreenView()
            : base(Resource.Layout.SplashScreen)
        {
        }
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
               base.OnCreate(savedInstanceState);
        }
    }
}