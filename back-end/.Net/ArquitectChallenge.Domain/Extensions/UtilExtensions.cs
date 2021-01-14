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

        public static string FirstTag(this string tag)
        {
            if (string.IsNullOrWhiteSpace(tag)
                    || !tag.Contains("."))
            {
                return null;
            }

            return tag.Split(".")[0];
        }

        public static bool IsNumeric(this string value)
        {
            return Regex.IsMatch(value, "^[0-9]+$");
        }

        public static string SecondTag(this string tag)
        {
            if (string.IsNullOrWhiteSpace(tag)
                    || !tag.Contains("."))
            {
                return null;
            }

            var tags = tag.Split(".");
            if (tags.Length == 1)
            {
                return null;
            }

            return string.Join(".", tags[0], tags[1]);
        }

        public static DateTime UnixTimeStampToDateTime(this long unixTimeStamp)
        {
            return DateTimeOffset.FromUnixTimeMilliseconds(unixTimeStamp).ToLocalTime().DateTime;
        }
    }
}