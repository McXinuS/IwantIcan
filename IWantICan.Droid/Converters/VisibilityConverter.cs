using System;
using System.Globalization;
using Android.Views;
using MvvmCross.Platform.Converters;

namespace IWantICan.Droid.Converters
{
    public class VisibilityConverter : MvxValueConverter<bool>
    {
        protected override object Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            return value ? ViewStates.Visible : ViewStates.Gone;
        }
    }
}