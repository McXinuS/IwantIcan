using System;
using Android.App;
using Android.Content;
using Android.Preferences;

namespace IWantICan.Core.Services
{
    public class SharedPreferencesService : ISharedPreferencesService
    {
        ISharedPreferences _prefs;
        ISharedPreferencesEditor _editor;

        public SharedPreferencesService()
        {
            _prefs = PreferenceManager.GetDefaultSharedPreferences(Application.Context);
            _editor = _prefs.Edit();
        }

        public string LastLogin
        {
            get
            {
                return _prefs.GetString("LastLogin", String.Empty);
            }

            set
            {
                _editor.PutString("LastLogin", value);
                _editor.Apply();
            }
        }

        public string AuthToken
        {
            get
            {
                return _prefs.GetString("AuthToken", String.Empty);
            }
                set
            {
                _editor.PutString("AuthToken", value);
                _editor.Apply();
            }
        }

        public int UserId
        {
            get
            {
                return _prefs.GetInt("UserId", -1);
            }
                set
            {
                _editor.PutInt("UserId", value);
                _editor.Apply();
            }
        }
    }
}
