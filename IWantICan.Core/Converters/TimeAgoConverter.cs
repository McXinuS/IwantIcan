using System;
using System.Globalization;
using MvvmCross.Platform.Converters;

namespace IWantICan.Core.Converters
{
    public class TimeAgoConverter
        : MvxValueConverter<DateTime>
    {
        protected override object Convert(DateTime when, Type targetType, object parameter, CultureInfo culture)
        {
            var difference = (DateTime.UtcNow - when).TotalSeconds;
            string whichFormat;
            int valueToFormat;
            
            if (difference < 100.0)
            {
                whichFormat = "{0} с";
                valueToFormat = (int)difference;
            }
            else if (difference < 3600.0)
            {
                whichFormat = "{0} мин";
                valueToFormat = (int)(difference / 60);
            }
            else if (difference < 24 * 3600)
            {
                whichFormat = "{0} ч";
                valueToFormat = (int)(difference / (3600));
            }
            else
            {
                return when.ToString("d", culture);
            }

            return string.Format(whichFormat, valueToFormat);
        }
    }
}