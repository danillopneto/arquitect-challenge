using System;
using System.Text.RegularExpressions;

namespace ArquitectChallenge.Domain.Utilities
{
    public static class UtilExtensions
    {
        public static T ConvertTo<T>(this object model)
        {
            if (model == null)
            {
                return default;
            }

            return (T)Convert.ChangeType(model, typeof(T));
        }

        public static bool IsNumeric(this string value)
        {
            return Regex.IsMatch(value, "^[0-9]+$");
        }

        public static DateTime UnixTimeStampToDateTime(this long unixTimeStamp)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(unixTimeStamp).ToLocalTime().DateTime;
        }
    }
}