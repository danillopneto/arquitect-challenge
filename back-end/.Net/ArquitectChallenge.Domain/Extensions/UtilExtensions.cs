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

        public static DateTime UnixTimeStampToDateTime(this double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(unixTimeStamp);
            var newDateTime = new DateTime(dtDateTime.Year, dtDateTime.Month, dtDateTime.Day, dtDateTime.Hour, 0, 0);
            return newDateTime;
        }
    }
}