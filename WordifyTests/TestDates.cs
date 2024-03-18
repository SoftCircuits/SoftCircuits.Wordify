// Copyright (c) 2023-2024 Jonathan Wood (www.softcircuits.com)
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
        }

        [TestMethod]
        public void TestTimeSpan()
        {
            DateTime now = DateTime.Now;

            Assert.AreEqual("0 milliseconds", (now - now).Wordify(10));

            Assert.AreEqual("20 seconds", (now.Offset(seconds: 20) - now).Wordify(10));
            Assert.AreEqual("23 minutes and 20 seconds", (now.Offset( minutes: 23, seconds: 20) - now).Wordify(10));
            Assert.AreEqual("2 hours, 23 minutes and 20 seconds", (now.Offset(hours: 2, minutes: 23, seconds: 20) - now).Wordify(10));
            Assert.AreEqual("2 weeks and 4 days", (now.Offset(days: 18) - now).Wordify(10));
            Assert.AreEqual("2 weeks, 4 days and 4 hours", (now.Offset(days: 18, hours: 4) - now).Wordify(10));
            //Assert.AreEqual("4 weeks and 2 days", (now.Offset(months: 1) - now).Wordify(10)); // Depends on length of month
            //Assert.AreEqual("104 weeks and 3 days", (now.Offset(years: 2) - now).Wordify(10));  // Depends on if leap year

            Assert.AreEqual("twenty seconds", (now.Offset(seconds: 20) - now).Wordify(10, DateTimeOption.UseWords));
            Assert.AreEqual("twenty-three minutes and twenty seconds", (now.Offset(minutes: 23, seconds: 20) - now).Wordify(10, DateTimeOption.UseWords));
            Assert.AreEqual("two hours, twenty-three minutes and twenty seconds", (now.Offset(hours: 2, minutes: 23, seconds: 20) - now).Wordify(10, DateTimeOption.UseWords));

            Assert.AreEqual("2 hours", (now.Offset(hours: 2, minutes: 23, seconds: 20) - now).Wordify(1));
            Assert.AreEqual("2 hours and 23 minutes", (now.Offset(hours: 2, minutes: 23, seconds: 20) - now).Wordify(2));
            Assert.AreEqual("2 hours, 23 minutes and 20 seconds", (now.Offset(hours: 2, minutes: 23, seconds: 20) - now).Wordify(3));

            Assert.AreEqual("two hours", (now.Offset(hours: 2, minutes: 23, seconds: 20) - now).Wordify(1, DateTimeOption.UseWords));
            Assert.AreEqual("two hours and twenty-three minutes", (now.Offset(hours: 2, minutes: 23, seconds: 20) - now).Wordify(2, DateTimeOption.UseWords));
            Assert.AreEqual("two hours, twenty-three minutes and twenty seconds", (now.Offset(hours: 2, minutes: 23, seconds: 20) - now).Wordify(3, DateTimeOption.UseWords));
        }
    }
}
