// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using SoftCircuits.Wordify;

namespace WordifyTests
{
    [TestClass]
    public class TestDates
    {
        [Flags]
        enum DateTimeOption
        {
            UseUtc, // Only valid with DateTime version
            UseWords,
        }

        [TestMethod]
        public void TestDateTime()
        {
            DateTime now = DateTime.Now;

            // TODO: Fix broken calls
            // TODO: Handle tomorrow/yesterday
            // TODO: Implement DateTimeOptions

            // Now
            Assert.AreEqual("now", Wordify.Transform(now));
            Assert.AreEqual("now", Wordify.Transform(DateTime.Now));

            // In the future
            Assert.AreEqual("13 seconds from now", Wordify.Transform(now.AddSeconds(13), now, 10));
            Assert.AreEqual("24 minutes and 13 seconds from now", Wordify.Transform(now.AddMinutes(24).AddSeconds(13), now, 10));
            Assert.AreEqual("1 day, 24 minutes and 13 seconds from now", Wordify.Transform(now.AddHours(24).AddMinutes(24).AddSeconds(13), now, 10));
            Assert.AreEqual("2 years, 1 day, 24 minutes and 13 seconds from now", Wordify.Transform(now.AddYears(2).AddHours(24).AddMinutes(24).AddSeconds(13), now, 10));

            Assert.AreEqual("2 years from now", Wordify.Transform(now.AddYears(2).AddHours(24).AddMinutes(24).AddSeconds(13), now, 1));
            Assert.AreEqual("2 years and 1 day from now", Wordify.Transform(now.AddYears(2).AddHours(24).AddMinutes(24).AddSeconds(13), now, 2));
            Assert.AreEqual("2 years, 1 day and 24 minutes from now", Wordify.Transform(now.AddYears(2).AddHours(24).AddMinutes(24).AddSeconds(13), now, 3));
            Assert.AreEqual("2 years, 1 day, 24 minutes and 13 seconds from now", Wordify.Transform(now.AddYears(2).AddHours(24).AddMinutes(24).AddSeconds(13), now, 4));

            // In the past
            Assert.AreEqual("13 seconds ago", Wordify.Transform(now, now.AddSeconds(13), 10));
            Assert.AreEqual("24 minutes and 13 seconds ago", Wordify.Transform(now, now.AddMinutes(24).AddSeconds(13), 10));
            Assert.AreEqual("1 day, 24 minutes and 13 seconds ago", Wordify.Transform(now, now.AddHours(24).AddMinutes(24).AddSeconds(13), 10));
            Assert.AreEqual("2 years, 1 day, 24 minutes and 13 seconds ago", Wordify.Transform(now, now.AddYears(2).AddHours(24).AddMinutes(24).AddSeconds(13), 10));

            Assert.AreEqual("2 years ago", Wordify.Transform(now, now.AddYears(2).AddHours(24).AddMinutes(24).AddSeconds(13), 1));
            Assert.AreEqual("2 years and 1 day ago", Wordify.Transform(now, now.AddYears(2).AddHours(24).AddMinutes(24).AddSeconds(13), 2));
            Assert.AreEqual("2 years, 1 day and 24 minutes ago", Wordify.Transform(now, now.AddYears(2).AddHours(24).AddMinutes(24).AddSeconds(13), 3));
            Assert.AreEqual("2 years, 1 day, 24 minutes and 13 seconds ago", Wordify.Transform(now, now.AddYears(2).AddHours(24).AddMinutes(24).AddSeconds(13), 4));

            // Rounding
            Assert.AreEqual("1 year from now", Wordify.Transform(now.AddMonths(11), now, 1));

        }

        [TestMethod]
        public void TestTimeSpan()
        {

        }
    }
}
