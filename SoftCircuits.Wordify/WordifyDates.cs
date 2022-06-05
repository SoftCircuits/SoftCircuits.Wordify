// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using System.Diagnostics;

namespace SoftCircuits.Wordify
{
    public static partial class Wordify
    {
        private const double DaysPerYear = 365;
        private const double DaysPerMonth = 30.437;

        //private class TimeSpanPart
        //{
        //    public PartType PartType { get; set; }
        //    public double Value { get; set; }

        //    public TimeSpanPart(PartType partType, double value)
        //    {
        //        PartType = partType;
        //        Value = value;
        //    }
        //}

        //private enum PartType
        //{
        //    Year,
        //    Month,
        //    Day,
        //    Hour,
        //    Minute,
        //    Second,
        //    Millisecond
        //};

        //// TODO: Support DateTimeOffset, DateOnly, TimeOnly

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="dateTime"></param>
        ///// <param name="useUtc"></param>
        ///// <returns></returns>
        //public static string Transform(DateTime dateTime, int precision = 1, bool useUtc = false) =>
        //    Transform(dateTime, useUtc ? DateTime.UtcNow : DateTime.Now, precision);

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="dateTime"></param>
        ///// <param name="relativeTo"></param>
        ///// <param name="precision"></param>
        ///// <returns></returns>
        //public static string Transform(this DateTime dateTime, DateTime relativeTo, int precision = 1)
        //{
        //    TimeSpan span = dateTime - relativeTo;

        //    // TODO: Need precision 1 - 7 (default = 1)

        //    // What constitutes now ???
        //    // - 7 - milliseconds
        //    // - 6 - seconds
        //    // - 5 - minutes
        //    // - 4 - hours
        //    // - 3 - days

        //    string.Join

        //    if (span.TotalSeconds < 1.0)    // Precision ???
        //        return "now";

        //    if (span >= TimeSpan.Zero)
        //    {
        //        // In the future

        //        foreach ((string Unit, double Value) item in GetTimeSpanParts(span))
        //        {

        //        }
        //    }
        //    else
        //    {
        //        // In the past

        //        // TODO: Need to pass positive span
        //        span = span.Duration();

        //        foreach ((string Unit, double Value) item in GetTimeSpanParts(span))
        //        {






        //        }
        //    }





        //    if (dateTime > relativeTo)
        //    {
        //        // TODO: If precision limits to months, we should round month. Otherwise, we only count complete months

        //        //if ()

        //    }
        //    else
        //    {

        //    }


        //    //You can Humanize an instance of DateTime or DateTimeOffset and get back a string telling how far back or forward in time that is:

        //    //DateTime.UtcNow.AddHours(-30).Humanize() => "yesterday"
        //    //DateTime.UtcNow.AddHours(-2).Humanize() => "2 hours ago"

        //    //DateTime.UtcNow.AddHours(30).Humanize() => "tomorrow"
        //    //DateTime.UtcNow.AddHours(2).Humanize() => "2 hours from now"

        //    //DateTimeOffset.UtcNow.AddHours(1).Humanize() => "an hour from now"
        //    //Humanizer supports both local and UTC dates as well as dates with offset(DateTimeOffset). You could also provide the date you want the input date to be compared against.If null, it will use the current date as comparison base.Also, culture to use can be specified explicitly.If it is not, current thread's current UI culture is used. Here is the API signature:

        //    //public static string Humanize(this DateTime input, bool utcDate = true, DateTime? dateToCompareAgainst = null, CultureInfo culture = null)
        //    //public static string Humanize(this DateTimeOffset input, DateTimeOffset? dateToCompareAgainst = null, CultureInfo culture = null)

        //    return string.Empty;
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="timeSpan"></param>
        ///// <param name="precision"></param>
        ///// <returns></returns>
        //public static string Transform(this TimeSpan timeSpan, int precision = 1)
        //{
        //    List<TimeSpanPart> parts = GetTimeSpanParts(timeSpan.Duration(), precision);

        //    for (int i = 0; i < parts.Count; i++)
        //    {
        //        if (i + 1 >= parts.Count)
        //        {

        //        }
        //        else
        //        {

        //        }
        //    }



        //    foreach ((PartType Part, double Value) part in GetTimeSpanParts(timeSpan.Duration()))
        //    {

        //        precision--;


        //        if (lastItem)
        //            part.Value = Math.Round(part.Value);

        //        parts.Add($"{part.Value} {part.Part.ToString().ToLower().Pluralize(part.Value)}");






        //    }

        //    // There is an optional precision parameter for TimeSpan.Humanize which allows you to specify the precision of the returned
        //    // value. The default value of precision is 1 which means only the largest time unit is returned like you saw in
        //    // TimeSpan.FromDays(16).Humanize().Here is a few examples of specifying precision:

        //    // TimeSpan.FromDays(1).Humanize(precision: 2) => "1 day" // no difference when there is only one unit in the provided TimeSpan
        //    // TimeSpan.FromDays(16).Humanize(2) => "2 weeks, 2 days"

        //    // // the same TimeSpan value with different precision returns different results
        //    // TimeSpan.FromMilliseconds(1299630020).Humanize() => "2 weeks"
        //    // TimeSpan.FromMilliseconds(1299630020).Humanize(3) => "2 weeks, 1 day, 1 hour"
        //    // TimeSpan.FromMilliseconds(1299630020).Humanize(4) => "2 weeks, 1 day, 1 hour, 30 seconds"
        //    // TimeSpan.FromMilliseconds(1299630020).Humanize(5) => "2 weeks, 1 day, 1 hour, 30 seconds, 20 milliseconds"

        //    return string.Empty;
        //}

        //private static List<TimeSpanPart> GetTimeSpanParts(TimeSpan timeSpan, int precision)
        //{
        //    Debug.Assert(timeSpan >= TimeSpan.Zero);
        //    List<TimeSpanPart> parts = new();

        //    if (timeSpan.TotalDays >= DaysPerYear)
        //    {
        //        double years = timeSpan.TotalDays / DaysPerYear;
        //        parts.Add(new(PartType.Year, years));
        //        if (--precision <= 0)
        //            return parts;
        //        timeSpan.Subtract(TimeSpan.FromDays(Math.Truncate(years) * DaysPerYear));
        //    }

        //    if (timeSpan.TotalDays >= DaysPerMonth)
        //    {
        //        double months = timeSpan.TotalDays / DaysPerMonth;
        //        parts.Add(new(PartType.Month, months));
        //        if (--precision <= 0)
        //            return parts;
        //        timeSpan.Subtract(TimeSpan.FromDays(Math.Truncate(months) * DaysPerMonth));
        //    }

        //    if (timeSpan.TotalDays > 0)
        //    {
        //        parts.Add(new(PartType.Day, timeSpan.TotalDays));
        //        if (--precision <= 0)
        //            return parts;
        //        timeSpan.Subtract(TimeSpan.FromDays(Math.Truncate(timeSpan.TotalDays)));
        //    }

        //    if (timeSpan.TotalHours > 0)
        //    {
        //        parts.Add(new(PartType.Hour, timeSpan.TotalHours));
        //        if (--precision <= 0)
        //            return parts;
        //        timeSpan.Subtract(TimeSpan.FromHours(Math.Truncate(timeSpan.TotalHours)));
        //    }

        //    if (timeSpan.TotalMinutes > 0)
        //    {
        //        parts.Add(new(PartType.Minute, timeSpan.TotalMinutes));
        //        if (--precision <= 0)
        //            return parts;
        //        timeSpan.Subtract(TimeSpan.FromMinutes(Math.Truncate(timeSpan.TotalMinutes)));
        //    }

        //    if (timeSpan.TotalSeconds > 0)
        //    {
        //        parts.Add(new(PartType.Second, timeSpan.TotalSeconds));
        //        if (--precision <= 0)
        //            return parts;
        //        timeSpan.Subtract(TimeSpan.FromMinutes(Math.Truncate(timeSpan.TotalSeconds)));
        //    }

        //    if (timeSpan.TotalMilliseconds > 0)
        //        parts.Add(new(PartType.Millisecond, timeSpan.TotalMilliseconds));

        //    return parts;
        //}
    }
}
