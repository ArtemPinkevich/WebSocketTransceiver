namespace Common.Converters
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Data;

    using Extensions;

    /// <summary>
    /// Converts an enum to a list, that contains all the enum's description or name.
    /// It should be used to pair with "EnumToDescriptionConverter"!!!
    /// </summary>
    [ValueConversion(typeof(Enum), typeof(IEnumerable<string>))]
    public class EnumToCollectionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Enum))
                return null;

            List<Enum> allEnumItems = Enum.GetValues(value.GetType()).Cast<Enum>().ToList();

            return allEnumItems.Select(o => o.GetDescription());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
