using System;
using System.Globalization;
using MvvmCross.Platform.Converters;

namespace IWantICan.Core.Converters
{

    public class StringLengthConverter : MvxValueConverter<string>
    {
        protected override object Convert(string value, Type targetType, object parameter, CultureInfo culture)
        {
            value = value ?? string.Empty;
            return value.Length;
        }
    }
}