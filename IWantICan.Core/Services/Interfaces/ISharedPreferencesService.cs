namespace IWantICan.Core.Services
{
    public interface ISharedPreferencesService
    {
        /// <summary>
        /// Returns a last successeful login if shared preferences contains it or empty string otherwise.
        /// </summary>
        string LastLogin { get; set; }

        /// <summary>
        /// Returns a token if shared preferences contains it or empty string otherwise.
        /// </summary>
        string AuthToken { get; set; }

        /// <summary>
        /// Returns the current user ID if shared preferences contains it or -1 otherwise.
        /// </summary>
        int UserId { get; set; }
    }
}
