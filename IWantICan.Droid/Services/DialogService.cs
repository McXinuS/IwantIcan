using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Widget;
using IWantICan.Core.Interfaces;
using IWantICan.Core.Models;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;

namespace IWantICan.Droid.Services
{
    public class DialogService : IDialogService
    {
        public void Alert(string message, string title, string okbtnText)
        {
            var top = Mvx.Resolve<IMvxAndroidCurrentTopActivity>();
            var act = top.Activity;

            var adb = new AlertDialog.Builder(act);
            adb.SetTitle(title);
            adb.SetMessage(message);
            adb.SetIcon(Resource.Drawable.logo);
            adb.SetPositiveButton(okbtnText, (sender, args) => { });
            adb.Create().Show();
        }

        public void Alert(string message, string title, string okbtnText, Action callback)
        {
            var top = Mvx.Resolve<IMvxAndroidCurrentTopActivity>();
            var act = top.Activity;

            var adb = new AlertDialog.Builder(act);
            adb.SetTitle(title);
            adb.SetMessage(message);
            adb.SetIcon(Resource.Drawable.logo);
            adb.SetPositiveButton(okbtnText, (sender, args) => { callback(); });
            adb.Create().Show();
        }

        public void Alert(string message, string title, string okbtnText, string cancelbtnText, Action callback)
        {
            var top = Mvx.Resolve<IMvxAndroidCurrentTopActivity>();
            var act = top.Activity;

            var adb = new AlertDialog.Builder(act);
            adb.SetTitle(title);
            adb.SetMessage(message);
            adb.SetIcon(Resource.Drawable.logo);
            adb.SetPositiveButton(okbtnText, (sender, args) => { callback(); });
            adb.SetNegativeButton(cancelbtnText, (sender, args) => { });
            adb.Create().Show();
        }

        public void Filter(string[] categories, int selected, Func<int, int> callback)
        {
            AlertDialog ad = null;
            var currentSelection = selected - 1;

            var top = Mvx.Resolve<IMvxAndroidCurrentTopActivity>();
            var act = top.Activity;

            var adb = new AlertDialog.Builder(act);
            adb.SetCancelable(true);
            adb.SetTitle(Resource.String.activity_filter_title);

            if (categories.Length == 0)
            {
                adb.SetMessage(Resource.String.activity_filter_empty);
            }
            else
            {
                adb.SetSingleChoiceItems(categories, currentSelection,
                    (sender, args) =>
                    {
                        callback(args.Which + 1);
                        ad?.Dismiss();
                    });
                //adb.SetPositiveButton("Выбрать",
                //    (sender, args) => callback(currentSelection));
            }
            adb.SetNegativeButton("Назад",
                (sender, args) => { });
            ad = adb.Show();
        }

        public void Filter(string[] categories, int[] selected, Func<int[], int[]> callback)
        {
            var selectedBool = IntArrayToBool(selected, categories.Length);

            var top = Mvx.Resolve<IMvxAndroidCurrentTopActivity>();
            var act = top.Activity;

            var adb = new AlertDialog.Builder(act);
            adb.SetCancelable(true);
            adb.SetTitle(Resource.String.activity_filter_title);

            if (categories.Length == 0)
            {
                adb.SetMessage(Resource.String.activity_filter_empty);
            }
            else
            {
                adb.SetMultiChoiceItems(categories, selectedBool,
                    (sender, args) =>
                    {
                        selectedBool[args.Which] = !selectedBool[args.Which];
                    });
                adb.SetPositiveButton("Выбрать",
                    (sender, args) => callback(BoolArrayToInt(selectedBool, categories.Length)));
            }
            adb.SetNegativeButton("Назад",
                (sender, args) => { });
            adb.Create().Show();
        }

        // Filter dialog's converter
        private bool[] IntArrayToBool(int[] values, int size)
        {
            if (values == null || size == 0)
                return new bool[0];

            var selectedBool = new bool[size];
            for (var i = 0; i < size; i++)
            {
                selectedBool[i] = values.Contains(i + 1);
            }

            return selectedBool;
        }

        // Filter dialog's converter
        private int[] BoolArrayToInt(bool[] values, int size)
        {
            if (values == null || size == 0)
                return new int[0];

            var selectedInt = new List<int>();
            for (var i = 0; i < size; i++)
            {
                if (values[i])
                    selectedInt.Add(i + 1);
            }

            return selectedInt.ToArray();
        }

        public void ContactDialog(UserModel user)
        {
            if (user == null)
                return;

            var top = Mvx.Resolve<IMvxAndroidCurrentTopActivity>();
            var act = top.Activity;

            var adb = new Dialog(act);
            // adb.SetTitle($"Связаться с {user.name} {user.surname}"); // layout width issue
            adb.SetTitle("Связаться");
            adb.SetCancelable(true);
            adb.SetContentView(Resource.Layout.dialog_contact);

            //var title = (TextView) adb.FindViewById(Resource.Id.title);

            var titlePhone = (TextView)adb.FindViewById(Resource.Id.titlePhone);
            var titleEmail = (TextView)adb.FindViewById(Resource.Id.titleEmail);
            var titleVk = (TextView)adb.FindViewById(Resource.Id.titleVk);
            
            var errorMessage = act.Resources.GetString(Resource.String.dialog_connect_empty_field);
            titlePhone.Text = !string.IsNullOrWhiteSpace(user.phone) ? user.phone : errorMessage;
            titleEmail.Text = !string.IsNullOrWhiteSpace(user.email) ? user.email : errorMessage;
            titleVk.Text = !string.IsNullOrWhiteSpace(user.vkLink) ? user.vkLink : errorMessage;

            // close button
            ((Button)adb.FindViewById(Resource.Id.closeButton)).Click += (sender, args) => { adb.Dismiss(); };

            adb.Show();
        }
    }
}