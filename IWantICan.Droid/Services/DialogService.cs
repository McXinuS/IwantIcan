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
using IWantICan.Core.ViewModels;
using IWantICan.Droid.Fragments;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Droid.Support.V7.RecyclerView;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid.Platform;

namespace IWantICan.Droid.Services
{
    public class DialogService : IDialogService
    {
	    const int _alertDialogThemeId = Resource.Style.AlertDialogStyle;
		
		public void Alert(string message, string title, string okbtnText)
        {
            var top = Mvx.Resolve<IMvxAndroidCurrentTopActivity>();
            var act = top.Activity;

			var adb = new AlertDialog.Builder(act, _alertDialogThemeId);
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

			var adb = new AlertDialog.Builder(act, _alertDialogThemeId);
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

            var adb = new AlertDialog.Builder(act, _alertDialogThemeId);

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

			var adb = new AlertDialog.Builder(act, _alertDialogThemeId);
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
                //adb.SetPositiveButton("�������",
                //    (sender, args) => callback(currentSelection));
            }
            adb.SetNegativeButton("�����",
                (sender, args) => { });
            ad = adb.Show();
        }

        public void Filter(string[] categories, int[] selected, Func<int[], int[]> callback)
        {
            var selectedBool = IntArrayToBool(selected, categories.Length);

            var top = Mvx.Resolve<IMvxAndroidCurrentTopActivity>();
            var act = top.Activity;

			var adb = new AlertDialog.Builder(act, _alertDialogThemeId);
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
                adb.SetPositiveButton("�������",
                    (sender, args) => callback(BoolArrayToInt(selectedBool, categories.Length)));
            }
            adb.SetNegativeButton("�����",
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
	}
}