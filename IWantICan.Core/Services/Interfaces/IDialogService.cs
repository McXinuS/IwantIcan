using System;
using System.Threading.Tasks;
using IWantICan.Core.Models;

namespace IWantICan.Core.Interfaces
{
    /// <summary>
    /// Provides a service to any code that requires a dialog to be shown to the user for more complex situations
    /// where responses and richer user interaction is required use the IInteractionRequest service.
    /// </summary>
    public interface IDialogService
    {
        /// <summary>Alerts the user with a simple OK dialog and provides a <paramref name="message"/>.</summary>
        void Alert(string message, string title, string okbtnText);

        /// <summary>Alerts the user with a simple OK dialog with an action on close and provides a <paramref name="message"/>.</summary>
        void Alert(string message, string title, string okbtnText, Action callback);

        /// <summary>
        /// Shows a dialog with single choice filter (RadioButton).
        /// </summary>
        void Filter(string[] categories, int selected, Func<int, int> callback);

        /// <summary>
        /// Shows a dialog with mutliple choice filter (CheckBox).
        /// </summary>
        void Filter(string[] categories, int[] selected, Func<int[], int[]> callback);

        /// <summary>
        /// Shows a dialog with contact information of the user.
        /// </summary>
        void ContactDialog(UserModel user);
    }
}