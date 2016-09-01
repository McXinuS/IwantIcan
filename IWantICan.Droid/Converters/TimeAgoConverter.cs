using System;
using System.Globalization;
using Android.App;
using MvvmCross.Platform.Converters;
using Android.Text.Format;

namespace IWantICan.Droid.Converters
{
    public class TimeAgoConverter
        : MvxValueConverter<DateTime>
    {
        protected override object Convert(DateTime when, Type targetType, object parameter, CultureInfo culture)
        {
            /*var whenMillis = (long)TimeSpan.FromTicks(when.Ticks).TotalMilliseconds;
            var nowMillis = (long)TimeSpan.FromTicks(DateTime.Now.Ticks).TotalMilliseconds;
            return DateUtils.GetRelativeTimeSpanString(whenMillis,
                  nowMillis,
                  DateUtils.SecondInMillis,
                  FormatStyleFlags.AbbrevRelative);*/

            var difference = (DateTime.UtcNow - when).TotalSeconds;
            string whichFormat;
            int valueToFormat;

	        if (difference < 2.0)
			{
				return "Только что";
			}
            else if (difference < 100.0)
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
                valueToFormat = (int)(difference / 3600);
            }
            else
            {
                return when.ToString("d", culture);
            }

            return string.Format(whichFormat, valueToFormat);
        }
    }
}