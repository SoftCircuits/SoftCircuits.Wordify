// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using System.Diagnostics;

namespace SoftCircuits.Wordify
{
    public static partial class WordifyExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="useUtc"></param>
        /// <returns></returns>
        public static string Wordify(this DateTime dateTime, bool useUtc = false, DateTimeOption options = DateTimeOption.None) =>
            Wordify(dateTime, useUtc ? DateTime.UtcNow : DateTime.Now, options);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="relativeTo"></param>
        /// <param name="precision"></param>
        /// <returns></returns>
        public static string Wordify(this DateTime dateTime, DateTime relativeTo, DateTimeOption options = DateTimeOption.None)
        {
            bool inPast = dateTime < relativeTo;

            IEnumerator<DateTimeDifference> enumerator = GetDateTimeDifferences(dateTime, relativeTo).GetEnumerator();

            if (!enumerator.MoveNext() || enumerator.Current.Type == DateTimeDifferenceType.Millisecond)
                return "now";

            DateTimeDifference difference = enumerator.Current;
            if (difference.Type == DateTimeDifferenceType.Day && difference.Value == 1)
                return inPast ? "yesterday" : "tomorrow";

            string type = difference.Type.ToString();
            string value;
            if (difference.Value == 1)
            {
                // Determine 'indefinite article'
                value = (difference.Type == DateTimeDifferenceType.Hour) ? "an" : "a";
            }
            else if (options.HasFlag(DateTimeOption.UseWords))
                value = difference.Value.Wordify();
            else
                value = difference.Value.ToString();

            string description = type
                .ToLower()
                .Pluralize(difference.Value);

            return $"{value} {description} {(inPast ? "ago" : "from now")}";
        }

        // TODO: Support DateTimeOffset

        //public static string Humanize(this DateTime input, bool utcDate = true, DateTime? dateToCompareAgainst = null, CultureInfo culture = null)
        //public static string Humanize(this DateTimeOffset input, DateTimeOffset? dateToCompareAgainst = null, CultureInfo culture = null)

#if NET6_0_OR_GREATER

        // TODO: Support DateOnly
        // TODO: Support TimeOnly

#endif

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeSpan"></param>
        /// <param name="precision"></param>
        /// <returns></returns>
        public static string Transform(this TimeSpan timeSpan, int precision = 1, DateTimeOption options)
        {
            // TODO: How to handle negative span?

            //bool inPast =  dateTime < relativeTo;

            IEnumerator<DateTimeDifference> enumerator = GetDateTimeDifferences(dateTime, relativeTo).GetEnumerator();

            if (!enumerator.MoveNext() || enumerator.Current.Type == DateTimeDifferenceType.Millisecond)
                return "now";

            DateTimeDifference difference = enumerator.Current;
            if (difference.Type == DateTimeDifferenceType.Day && difference.Value == 1)
                return inPast ? "yesterday" : "tomorrow";

            string type = difference.Type.ToString();
            string value;
            if (difference.Value == 1)
            {
                // Determine 'indefinite article'
                value = (difference.Type == DateTimeDifferenceType.Hour) ? "an" : "a";
            }
            else if (options.HasFlag(DateTimeOption.UseWords))
                value = difference.Value.Wordify();
            else
                value = difference.Value.ToString();

            string description = type
                .ToLower()
                .Pluralize(difference.Value);

            return $"{value} {description}";


            //List<TimeSpanPart> parts = GetTimeSpanParts(timeSpan.Duration(), precision);

            //for (int i = 0; i < parts.Count; i++)
            //{
            //    if (i + 1 >= parts.Count)
            //    {

            //    }
            //    else
            //    {

            //    }
            //}



            //foreach ((PartType Part, double Value) part in GetTimeSpanParts(timeSpan.Duration()))
            //{

            //    precision--;


            //    if (lastItem)
            //        part.Value = Math.Round(part.Value);

            //    parts.Add($"{part.Value} {part.Part.ToString().ToLower().Pluralize(part.Value)}");






            //}

            // There is an optional precision parameter for TimeSpan.Humanize which allows you to specify the precision of the returned
            // value. The default value of precision is 1 which means only the largest time unit is returned like you saw in
            // TimeSpan.FromDays(16).Humanize().Here is a few examples of specifying precision:

            // TimeSpan.FromDays(1).Humanize(precision: 2) => "1 day" // no difference when there is only one unit in the provided TimeSpan
            // TimeSpan.FromDays(16).Humanize(2) => "2 weeks, 2 days"

            // // the same TimeSpan value with different precision returns different results
            // TimeSpan.FromMilliseconds(1299630020).Humanize() => "2 weeks"
            // TimeSpan.FromMilliseconds(1299630020).Humanize(3) => "2 weeks, 1 day, 1 hour"
            // TimeSpan.FromMilliseconds(1299630020).Humanize(4) => "2 weeks, 1 day, 1 hour, 30 seconds"
            // TimeSpan.FromMilliseconds(1299630020).Humanize(5) => "2 weeks, 1 day, 1 hour, 30 seconds, 20 milliseconds"

            return string.Empty;
        }

        #region Private Methods

        private enum DateTimeDifferenceType
        {
            Millisecond,
            Second,
            Minute,
            Hour,
            Day,
            Month,
            Year,
        };

        private class DateTimeDifference
        {
            public DateTimeDifferenceType Type { get; set; }
            public int Value { get; set; }

            public DateTimeDifference(DateTimeDifferenceType partType, int value)
            {
                Type = partType;
                Value = value;
            }
        }

        private static IEnumerable<DateTimeDifference> GetDateTimeDifferences(DateTime startDateTime, DateTime endDateTime)
        {
            // Calculate absoluate difference
            DateTime lowDateTime, highDateTime;
            if (startDateTime < endDateTime)
            {
                lowDateTime = startDateTime;
                highDateTime = endDateTime;
            }
            else if (startDateTime > endDateTime)
            {
                lowDateTime = endDateTime;
                highDateTime = startDateTime;
            }
            else yield break;   // No difference

            DateTime tempDateTime;
            int value;

            // Years
            if ((tempDateTime = lowDateTime.AddYears(1)) <= highDateTime)
            {
                value = 0;
                do
                {
                    lowDateTime = tempDateTime;
                    value++;
                } while ((tempDateTime = lowDateTime.AddYears(1)) <= highDateTime);
                yield return new(DateTimeDifferenceType.Year, value);
            }

            // Months
            if ((tempDateTime = lowDateTime.AddMonths(1)) <= highDateTime)
            {
                value = 0;
                do
                {
                    lowDateTime = tempDateTime;
                    value++;
                } while ((tempDateTime = lowDateTime.AddMonths(1)) <= highDateTime);
                yield return new(DateTimeDifferenceType.Month, value);
            }

            TimeSpan timeSpan = highDateTime - lowDateTime;
            if (timeSpan.Days > 0)
                yield return new(DateTimeDifferenceType.Day, timeSpan.Days);
            if (timeSpan.Hours > 0)
                yield return new(DateTimeDifferenceType.Hour, timeSpan.Hours);
            if (timeSpan.Minutes > 0)
                yield return new(DateTimeDifferenceType.Minute, timeSpan.Minutes);
            if (timeSpan.Seconds > 0)
                yield return new(DateTimeDifferenceType.Second, timeSpan.Seconds);
            if (timeSpan.Milliseconds > 0)
                yield return new(DateTimeDifferenceType.Millisecond, timeSpan.Milliseconds);
        }

        #endregion

    }
}
