// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using SoftCircuits.Wordify;

namespace WordifyTests
{
    // Helper class
    internal static class DateTimeExtensions
    {
        public static DateTime Offset(this DateTime dateTime, int years = 0, int months = 0, double days = 0, double hours = 0, double minutes = 0, double seconds = 0, double milliseconds = 0)
        {
            if (years != 0)
                dateTime = dateTime.AddYears(years);
            if (months != 0)
                dateTime = dateTime.AddMonths(months);
            if (days != 0)
                dateTime = dateTime.AddDays(days);
            if (hours != 0)
                dateTime = dateTime.AddHours(hours);
            if (minutes != 0)
                dateTime = dateTime.AddMinutes(minutes);
            if (seconds != 0)
                dateTime = dateTime.AddSeconds(seconds);
            if (milliseconds != 0)
                dateTime.AddMilliseconds(milliseconds);
            return dateTime;
        }
    }

    [TestClass]
    public class TestDates
    {
        [TestMethod]
        public void TestDateTime()
        {
            DateTime now = DateTime.Now;

            // Same date
            Assert.AreEqual("now", now.Wordify(now));
            Assert.AreEqual("now", now.Wordify(now.Offset(milliseconds: 80)));

            Assert.AreEqual("now", now.Wordify());
            Assert.AreEqual("now", DateTime.UtcNow.Wordify(true));

            // In the future
            Assert.AreEqual("13 seconds from now", now.Offset(seconds: 13).Wordify(now));
            Assert.AreEqual("24 minutes from now", now.Offset(minutes: 24, seconds: 13).Wordify(now));
            Assert.AreEqual("tomorrow", now.Offset(hours: 24, minutes: 24, seconds: 13).Wordify(now));
            Assert.AreEqual("2 years from now", now.Offset(years: 2, hours: 24, minutes: 24, seconds: 13).Wordify(now));

            Assert.AreEqual("2 years from now", now.Offset(years: 2, hours: 24, minutes: 24, seconds: 13).Wordify(now));
            Assert.AreEqual("2 years from now", now.Offset(years: 2, hours: 24, minutes: 24, seconds: 13).Wordify(now));
            Assert.AreEqual("2 years from now", now.Offset(years: 2, hours: 24, minutes: 24, seconds: 13).Wordify(now));
            Assert.AreEqual("2 years from now", now.Offset(years: 2, hours: 24, minutes: 24, seconds: 13).Wordify(now));

            // In the past
            Assert.AreEqual("13 seconds ago", now.Offset(seconds: -13).Wordify(now));
            Assert.AreEqual("24 minutes ago", now.Offset(minutes: -24, seconds: -13).Wordify(now));
            Assert.AreEqual("yesterday", now.Offset(hours: -24, minutes: -24, seconds: -13).Wordify(now));
            Assert.AreEqual("2 years ago", now.Offset(years: -2, hours: -24, minutes: -24, seconds: -13).Wordify(now));

            Assert.AreEqual("2 years ago", now.Offset(years: -2, hours: -24, minutes: -24, seconds: -13).Wordify(now));
            Assert.AreEqual("2 years ago", now.Offset(years: -2, hours: -24, minutes: -24, seconds: -13).Wordify(now));
            Assert.AreEqual("2 years ago", now.Offset(years: -2, hours: -24, minutes: -24, seconds: -13).Wordify(now));
            Assert.AreEqual("2 years ago", now.Offset(years: -2, hours: -24, minutes: -24, seconds: -13).Wordify(now));

            // Words
            Assert.AreEqual("two years from now", now.Offset(years: 2, hours: 24, minutes: 24, seconds: 13).Wordify(now, DateTimeOption.UseWords));
            Assert.AreEqual("two years from now", now.Offset(years: 2, hours: 24, minutes: 24, seconds: 13).Wordify(now, DateTimeOption.UseWords));
            Assert.AreEqual("two years ago", now.Offset(years: -2, hours: -24, minutes: -24, seconds: -13).Wordify(now, DateTimeOption.UseWords));
            Assert.AreEqual("two years ago", now.Offset(years: -2, hours: -24, minutes: -24, seconds: -13).Wordify(now, DateTimeOption.UseWords));
            Assert.AreEqual("fifty-nine seconds ago", now.Offset(seconds: -59).Wordify(now, DateTimeOption.UseWords));

            Assert.AreEqual("a year from now", now.Offset(years: 1).Wordify(now, DateTimeOption.UseWords));
            Assert.AreEqual("a month from now", now.Offset(months: 1).Wordify(now, DateTimeOption.UseWords));
            Assert.AreEqual("tomorrow", now.Offset(days: 1).Wordify(now, DateTimeOption.UseWords));
            Assert.AreEqual("an hour from now", now.Offset(hours: 1).Wordify(now, DateTimeOption.UseWords));
            Assert.AreEqual("a minute from now", now.Offset(minutes: 1).Wordify(now, DateTimeOption.UseWords));
            Assert.AreEqual("a second from now", now.Offset(seconds: 1).Wordify(now, DateTimeOption.UseWords));
            Assert.AreEqual("now", now.Offset(milliseconds: 1).Wordify(now, DateTimeOption.UseWords));
            Assert.AreEqual("now", now.Wordify(now, DateTimeOption.UseWords));
            Assert.AreEqual("a year ago", now.Offset(years: -1).Wordify(now, DateTimeOption.UseWords));
            Assert.AreEqual("a month ago", now.Offset(months: -1).Wordify(now, DateTimeOption.UseWords));
            Assert.AreEqual("yesterday", now.Offset(days: -1).Wordify(now, DateTimeOption.UseWords));
            Assert.AreEqual("an hour ago", now.Offset(hours: -1).Wordify(now, DateTimeOption.UseWords));
            Assert.AreEqual("a minute ago", now.Offset(minutes: -1).Wordify(now, DateTimeOption.UseWords));
            Assert.AreEqual("a second ago", now.Offset(seconds: -1).Wordify(now, DateTimeOption.UseWords));
            Assert.AreEqual("now", now.Offset(milliseconds: -1).Wordify(now, DateTimeOption.UseWords));


            // Rounding
            //Assert.AreEqual("1 year from now", Wordify.Transform(now.AddMonths(11), now, 1));

            //// Same date
            //Assert.AreEqual("now", now.Wordify(now));
            //Assert.AreEqual("now", now.Wordify(now.Offset(milliseconds: 80)));

            //// In the future
            //Assert.AreEqual("13 seconds from now", now.Offset(seconds: 13).Wordify(now, 10));
            //Assert.AreEqual("24 minutes and 13 seconds from now", now.Offset(minutes: 24, seconds: 13).Wordify(now, 10));
            //Assert.AreEqual("1 day, 24 minutes and 13 seconds from now", now.Offset(hours: 24, minutes: 24, seconds: 13).Wordify(now, 10));
            //Assert.AreEqual("2 years, 1 day, 24 minutes and 13 seconds from now", now.Offset(years: 2, hours: 24, minutes: 24, seconds: 13).Wordify(now, 10));

            //Assert.AreEqual("2 years from now", now.Offset(years: 2, hours: 24, minutes: 24, seconds: 13).Wordify(now, 1));
            //Assert.AreEqual("2 years and 1 day from now", now.Offset(years: 2, hours: 24, minutes: 24, seconds: 13).Wordify(now, 2));
            //Assert.AreEqual("2 years, 1 day and 24 minutes from now", now.Offset(years: 2, hours: 24, minutes: 24, seconds: 13).Wordify(now, 3));
            //Assert.AreEqual("2 years, 1 day, 24 minutes and 13 seconds from now", now.Offset(years: 2, hours: 24, minutes: 24, seconds: 13).Wordify(now, 4));

            //// In the past
            //Assert.AreEqual("13 seconds ago", now.Offset(seconds: -13).Wordify(now, 10));
            //Assert.AreEqual("24 minutes and 13 seconds ago", now.Offset(minutes: -24, seconds: -13).Wordify(now, 10));
            //Assert.AreEqual("1 day, 24 minutes and 13 seconds ago", now.Offset(hours: -24, minutes: -24, seconds: -13).Wordify(now, 10));
            //Assert.AreEqual("2 years, 1 day, 24 minutes and 13 seconds ago", now.Offset(years: -2, hours: -24, minutes: -24, seconds: -13).Wordify(now, 10));

            //Assert.AreEqual("2 years ago", now.Offset(years: -2, hours: -24, minutes: -24, seconds: -13).Wordify(now, 1));
            //Assert.AreEqual("2 years and 1 day ago", now.Offset(years: -2, hours: -24, minutes: -24, seconds: -13).Wordify(now, 2));
            //Assert.AreEqual("2 years, 1 day and 24 minutes ago", now.Offset(years: -2, hours: -24, minutes: -24, seconds: -13).Wordify(now, 3));
            //Assert.AreEqual("2 years, 1 day, 24 minutes and 13 seconds ago", now.Offset(years: -2, hours: -24, minutes: -24, seconds: -13).Wordify(now, 4));

            //// Rounding
            ////Assert.AreEqual("1 year from now", Wordify.Transform(now.AddMonths(11), now, 1));

        }


        [TestMethod]
        public void TestTimeSpan()
        {

        }
    }
}
