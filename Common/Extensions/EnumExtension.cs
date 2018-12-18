namespace Common.Extensions
{
    using System;
    using System.ComponentModel;
    using System.Linq;

    /// <summary>
    /// Extension of enums
    /// </summary>
    public static class EnumExtension
    {
        /// <summary>
        /// Returns value of attribute "Description" or .toString().
        /// </summary>
        public static string GetDescription(this Enum sourceEnum)
        {
            object[] nAttributes = sourceEnum.GetType().GetField(sourceEnum.ToString())?.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return (nAttributes?.FirstOrDefault() as DescriptionAttribute)?.Description ?? sourceEnum.ToString();
        }
    }
}
