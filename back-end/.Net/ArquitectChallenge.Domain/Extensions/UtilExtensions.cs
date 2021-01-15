using ArquitectChallenge.Domain.Events;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public static long DateTimeToUnixTimestamp(this DateTime dateTime)
        {
            DateTime unixStart = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            long unixTimeStampInTicks = (dateTime.ToLocalTime() - unixStart).Ticks;
            return unixTimeStampInTicks / TimeSpan.TicksPerSecond;
        }

        public static string FirstTag(this string tag)
        {
            if (string.IsNullOrWhiteSpace(tag))
            {
                return null;
            }

            return tag.Split(".")[0];
        }

        public static IList<NumericEventsData> GetNumericEventsData(this IList<IGrouping<string, EventData>> groupped)
        {
            var grouppedByNumber = new List<NumericEventsData>();
            foreach (var tag in groupped)
            {
                var group = new NumericEventsData
                {
                    Start = Convert.ToInt32(tag.First(x => x.Timestamp == tag.Min(t => t.Timestamp)).Valor),
                    Maximum = tag.Max(x => Convert.ToInt32(x.Valor)),
                    Minimum = tag.Min(x => Convert.ToInt32(x.Valor)),
                    End = Convert.ToInt32(tag.First(x => x.Timestamp == tag.Max(t => t.Timestamp)).Valor),
                    Tag = tag.Key,
                };

                grouppedByNumber.Add(group);
            }

            return grouppedByNumber;
        }

        public static bool IsNumeric(this string value)
        {
            return Regex.IsMatch(value, "^-?[0-9]+$");
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