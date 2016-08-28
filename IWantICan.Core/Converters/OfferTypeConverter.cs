using System;
using System.Globalization;
using IWantICan.Core.Models;
using MvvmCross.Platform.Converters;

namespace IWantICan.Core.Converters
{
	public class OfferTypeToBoolConverter : MvxValueConverter<OfferType>
	{
		protected override object Convert(OfferType value, Type targetType, object parameter, CultureInfo culture)
		{
			return value == OfferType.Want;
		}

		protected override OfferType TypedConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return (bool)value ? OfferType.Want : OfferType.Can;
		}
	}

	public class OfferTypeToStringConverter : MvxValueConverter<OfferType>
	{
		protected override object Convert(OfferType value, Type targetType, object parameter, CultureInfo culture)
		{
			return value == OfferType.Can ? Constants.OfferCreateCan : Constants.OfferCreateWant;
		}
	}
}
