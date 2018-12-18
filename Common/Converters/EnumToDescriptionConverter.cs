namespace Common.Converters
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Windows.Data;

    using Extensions;

    /// <summary>
    /// Converts enum to description or name.
    /// </summary>
    [ValueConversion(typeof(Enum), typeof(string))]
    public class EnumToDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value as Enum)?.GetDescription();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Type enumType = targetType.IsEnum ? targetType : targetType.GenericTypeArguments.FirstOrDefault(o => o.IsEnum);

            return enumType == null ? null : Enum.GetValues(enumType).Cast<Enum>().FirstOrDefault(o => o.GetDescription().Equals(value?.ToString(), StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
