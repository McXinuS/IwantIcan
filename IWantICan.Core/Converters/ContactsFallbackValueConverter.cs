using System;
using System.Globalization;
using MvvmCross.Platform.Converters;

namespace IWantICan.Core.Converters
{
    public class ContactsFallbackValueConverter : MvxValueConverter<string>
    {
        protected override object Convert(string value, Type targetType, object parameter, CultureInfo culture)
        {
            return !string.IsNullOrWhiteSpace(value) ? value : Constants.FallbackContacts;
        }
    }
}
