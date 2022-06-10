// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using System.Diagnostics;

namespace SoftCircuits.Wordify
{
    [Flags]
    public enum DateTimeOptions
    {
        None = 0,
        UseWords,
    }

    public static partial class Wordify
    {
        private const double DaysPerYear = 365;
        private const double DaysPerMonth = 30.437;

        #region PartType

        private enum PartType
        {
            Year,
            Month,
            Day,
            Hour,
            Minute,
            Second,
            Millisecond
        };

        private class TimeSpanPart
        {
            public PartType PartType { get; set; }
            public double Value { get; set; }
            //public double TotalValue { get; set; }


            public TimeSpanPart(PartType partType, double value/*, double totalValue*/)
            {
                PartType = partType;
                Value = value;
                //TotalValue = totalValue;
            }
        }

        #endregion

        // TODO: Support DateOnly (.NET 6.0 or greater)
        // TODO: Support TimeOnly (.NET 6.0 or greater)

        // TODO: Support DateTimeOffset

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="useUtc"></param>
        /// <returns></returns>
        public static string Transform(DateTime dateTime, bool useUtc = false, int precision = 1) =>
            Transform(dateTime, useUtc ? DateTime.UtcNow : DateTime.Now, precision);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <param name="relativeTo"></param>
        /// <param name="precision"></param>
        /// <returns></returns>
        public static string Transform(this DateTime dateTime, DateTime relativeTo, int precision = 1)
        {
            TimeSpan timeSpan = dateTime - relativeTo;

            if (timeSpan.TotalSeconds < 1.0)
                return "now";

            Debug.Assert(dateTime != relativeTo);
            bool inPast = (dateTime < relativeTo);
            //Debug.Assert(timeSpan != TimeSpan.Zero);
            //bool inPast = timeSpan < TimeSpan.Zero;

            List<string> terms = new();

            foreach (TimeSpanPart part in GetTimeSpanParts(timeSpan))
            {
                int count = (precision == 1) ? (int)Math.Round(part.Value) : (int)Math.Truncate(part.Value);
                terms.Add($"{count} {part.PartType.ToString().ToLower().Pluralize(count)}");
                if (--precision == 0)
                    break;
            }

            return $"{terms.Wordify()} {((dateTime < relativeTo) ? "ago" : "from now")}";

            // if (inPast)
            // + " ago"
            // else
            // + " from now"

            // Handle yesterday
            // Handle tomorrow

            //DateTime.UtcNow.AddHours(30).Humanize() => "tomorrow"
            //DateTime.UtcNow.AddHours(2).Humanize() => "2 hours from now"






            //if (timeSpan >= TimeSpan.Zero)
            //{
            //    List<string> xxx = new();

            //    // In the future
            //    foreach (TimeSpanPart part in GetTimeSpanParts(timeSpan))
            //    {

            //        xxx.Add($"{part.Value} {part.PartType.ToString().ToLower().Capitalize().Pluralize(part.Value)}   ");

            //    }
            //}
            //else
            //{
            //    // In the past

            //    // TODO: Need to pass positive span
            //    timeSpan = timeSpan.Duration();

            //    foreach ((string Unit, double Value) item in GetTimeSpanParts(timeSpan))
            //    {

            //    }
            //}



            //You can Humanize an instance of DateTime or DateTimeOffset and get back a string telling how far back or forward in time that is:

            //DateTime.UtcNow.AddHours(-30).Humanize() => "yesterday"
            //DateTime.UtcNow.AddHours(-2).Humanize() => "2 hours ago"

            //DateTime.UtcNow.AddHours(30).Humanize() => "tomorrow"
            //DateTime.UtcNow.AddHours(2).Humanize() => "2 hours from now"

            //DateTimeOffset.UtcNow.AddHours(1).Humanize() => "an hour from now"
            //Humanizer supports both local and UTC dates as well as dates with offset(DateTimeOffset). You could also provide the date you want the input date to be compared against.If null, it will use the current date as comparison base.Also, culture to use can be specified explicitly.If it is not, current thread's current UI culture is used. Here is the API signature:

            //public static string Humanize(this DateTime input, bool utcDate = true, DateTime? dateToCompareAgainst = null, CultureInfo culture = null)
            //public static string Humanize(this DateTimeOffset input, DateTimeOffset? dateToCompareAgainst = null, CultureInfo culture = null)

            return string.Empty;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeSpan"></param>
        /// <param name="precision"></param>
        /// <returns></returns>
        public static string Transform(this TimeSpan timeSpan, int precision = 1)
        {
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

        /// <summary>
        /// Returns the non-zero parts of the given <see cref="TimeSpan"/> in the order of largest units first.
        /// </summary>
        private static IEnumerable<TimeSpanPart> GetTimeSpanParts(TimeSpan timeSpan)
        {
            timeSpan = timeSpan.Duration();
            Debug.Assert(timeSpan >= TimeSpan.Zero);

            if (timeSpan.Days >= DaysPerYear)
            {

                // If last precision, we should round to nearest year
                // Otherwise, just show complete years

                double years = timeSpan.Days / DaysPerYear;
                yield return new(PartType.Year, years);
                timeSpan = timeSpan.Subtract(TimeSpan.FromDays(Math.Truncate(years) * DaysPerYear));
            }

            if (timeSpan.Days >= DaysPerMonth)
            {
                double months = timeSpan.Days / DaysPerMonth;
                yield return new(PartType.Month, months);
                timeSpan = timeSpan.Subtract(TimeSpan.FromDays(Math.Truncate(months) * DaysPerMonth));
            }

            if (timeSpan.Days > 0)
                yield return new(PartType.Day, timeSpan.Days);

            if (timeSpan.Hours > 0)
                yield return new(PartType.Hour, timeSpan.Hours);

            if (timeSpan.Minutes > 0)
                yield return new(PartType.Minute, timeSpan.Minutes);

            if (timeSpan.Seconds > 0)
                yield return new(PartType.Second, timeSpan.Seconds);

            if (timeSpan.Milliseconds > 0)
                yield return new(PartType.Millisecond, timeSpan.Milliseconds);
        }
    }
}
