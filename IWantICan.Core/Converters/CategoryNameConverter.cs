using System;
using System.Globalization;
using IWantICan.Core.Services;
using MvvmCross.Platform;
using MvvmCross.Platform.Converters;

namespace IWantICan.Core.Converters
{
    public class CategoryNameConverter : MvxValueConverter<int>
    {
        protected override object Convert(int value, Type targetType, object parameter, CultureInfo culture)
        {
            var _cs = Mvx.Resolve<ICategoryService>();
            var cat = _cs.GetCategory(value);
            return string.IsNullOrWhiteSpace(cat?.name) ? Constants.UnknownCategory : cat.name;
        }
    }
}