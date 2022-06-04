// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftCircuits.Wordify
{
    public static partial class Wordify
    {

        // DateTime

        public static string Transform(this DateTime dateTime, DateTime? relativeTo = null)
        {

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

        // TimeSpan

        public static string Transform(this TimeSpan span, int precision = 1)
        {
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
    }
}
