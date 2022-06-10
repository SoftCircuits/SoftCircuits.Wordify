// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using SoftCircuits.Wordify;

namespace WordifyTests
{
    [TestClass]
    public class TestRomanNumerals
    {
        private static readonly List<(int, string)> TestData = new()
        {
            (0, "N"),
            (1, "I"),
            (2, "II"),
            (3, "III"),
            (4, "IV"),
            (5, "V"),
            (6, "VI"),
            (7, "VII"),
            (8, "VIII"),
            (9, "IX"),
            (10, "X"),
            (11, "XI"),
            (12, "XII"),
            (13, "XIII"),
            (14, "XIV"),
            (15, "XV"),
            (16, "XVI"),
            (17, "XVII"),
            (18, "XVIII"),
            (19, "XIX"),
            (20, "XX"),
            (30, "XXX"),
            (40, "XL"),
            (50, "L"),
            (60, "LX"),
            (70, "LXX"),
            (80, "LXXX"),
            (90, "XC"),
            (100, "C"),
            (200, "CC"),
            (300, "CCC"),
            (400, "CD"),
            (500, "D"),
            (600, "DC"),
            (700, "DCC"),
            (800, "DCCC"),
            (900, "CM"),
            (1000, "M"),
            (2000, "MM"),
            (3000, "MMM"),

            (1900, "MCM"),
            (1912, "MCMXII"),
            (2000, "MM"),
            (2022, "MMXXII"),
        };

        [TestMethod]
        public void Test()
        {
            foreach ((int Value, string RomanNumerals) item in TestData)
            {
                Assert.AreEqual(item.RomanNumerals, item.Value.ToRomanNumerals());
                Assert.AreEqual(item.Value, item.RomanNumerals.ParseRomanNumerals());
            }

            Assert.AreEqual(0, " N ".ParseRomanNumerals());
            Assert.AreEqual(1, "  I  ".ParseRomanNumerals());
            Assert.AreEqual(2, " II ".ParseRomanNumerals());
            Assert.AreEqual(3, "  III  ".ParseRomanNumerals());
            Assert.AreEqual(4, "   IV   ".ParseRomanNumerals());
            Assert.AreEqual(5, " V ".ParseRomanNumerals());

            // Test out-of-range values
            Assert.AreEqual("MMMMDCCCLXXVI", 4876.ToRomanNumerals());
            Assert.AreEqual(4876, "MMMMDCCCLXXVI".ParseRomanNumerals());
        }
    }
}
