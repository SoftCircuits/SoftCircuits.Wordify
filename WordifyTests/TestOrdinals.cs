﻿// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using SoftCircuits.Wordify;

namespace WordifyTests
{
    [TestClass]
    public class TestOrdinals
    {
        [TestMethod]
        public void TestOrdinal()
        {
            List<(int, string)> data = new()
            {
                (0, "zeroth"),
                (1, "first"),
                (2, "second"),
                (3, "third"),
                (4, "fourth"),
                (5, "fifth"),
                (6, "sixth"),
                (7, "seventh"),
                (8, "eighth"),
                (9, "ninth"),
                (10, "tenth"),
                (11, "eleventh"),
                (12, "twelfth"),
                (13, "thirteenth"),
                (14, "fourteenth"),
                (15, "fifteenth"),
                (16, "sixteenth"),
                (17, "seventeenth"),
                (18, "eighteenth"),
                (19, "nineteenth"),
                (20, "twentieth"),
                (21, "twenty-first"),
                (22, "twenty-second"),
                (23, "twenty-third"),
                (24, "twenty-fourth"),
                (25, "twenty-fifth"),
                (26, "twenty-sixth"),
                (27, "twenty-seventh"),
                (28, "twenty-eighth"),
                (29, "twenty-ninth"),
                (30, "thirtieth"),
                (100, "one hundredth"),
                (101, "one hundred first"),
                (102, "one hundred second"),
                (103, "one hundred third"),
                (104, "one hundred fourth"),
                (105, "one hundred fifth"),
                (106, "one hundred sixth"),
                (107, "one hundred seventh"),
                (108, "one hundred eighth"),
                (109, "one hundred ninth"),
                (110, "one hundred tenth"),
                (111, "one hundred eleventh"),
                (112, "one hundred twelfth"),
                (113, "one hundred thirteenth"),
                (114, "one hundred fourteenth"),
                (115, "one hundred fifteenth"),
                (116, "one hundred sixteenth"),
                (2000, "two thousandth"),
                (2001, "two thousand first"),
                (2002, "two thousand second"),
                (2003, "two thousand third"),
                (2004, "two thousand fourth"),
                (2022, "two thousand twenty-second"),
                (2023, "two thousand twenty-third"),
                (2024, "two thousand twenty-fourth"),
                (1000000, "one millionth"),
                (1000001, "one million first"),
                (1000002, "one million second"),
                (1000003, "one million third"),
                (1000004, "one million fourth"),
                (-7, "negative seventh"),
                (-8, "negative eighth"),
                (-9, "negative ninth"),
                (-10, "negative tenth"),
                (-11, "negative eleventh"),
                (-12, "negative twelfth"),
                (-13, "negative thirteenth"),
            };

            foreach ((int input, string output) in data)
                Assert.AreEqual(output, input.MakeOrdinal());
        }

        [TestMethod]
        public void TestOrdinalDigits()
        {
            List<(int, string)> data = new()
            {
                (0, "0th"),
                (1, "1st"),
                (2, "2nd"),
                (3, "3rd"),
                (4, "4th"),
                (5, "5th"),
                (6, "6th"),
                (7, "7th"),
                (8, "8th"),
                (9, "9th"),
                (10, "10th"),
                (11, "11th"),
                (12, "12th"),
                (13, "13th"),
                (14, "14th"),
                (15, "15th"),
                (16, "16th"),
                (17, "17th"),
                (18, "18th"),
                (19, "19th"),
                (20, "20th"),
                (21, "21st"),
                (22, "22nd"),
                (23, "23rd"),
                (24, "24th"),
                (25, "25th"),
                (26, "26th"),
                (27, "27th"),
                (28, "28th"),
                (29, "29th"),
                (30, "30th"),
                (100, "100th"),
                (101, "101st"),
                (102, "102nd"),
                (103, "103rd"),
                (104, "104th"),
                (105, "105th"),
                (106, "106th"),
                (107, "107th"),
                (108, "108th"),
                (109, "109th"),
                (110, "110th"),
                (111, "111th"),
                (112, "112th"),
                (113, "113th"),
                (114, "114th"),
                (115, "115th"),
                (116, "116th"),
                (2000, "2,000th"),
                (2001, "2,001st"),
                (2002, "2,002nd"),
                (2003, "2,003rd"),
                (2004, "2,004th"),
                (2022, "2,022nd"),
                (2023, "2,023rd"),
                (2024, "2,024th"),
                (1000000, "1,000,000th"),
                (1000001, "1,000,001st"),
                (1000002, "1,000,002nd"),
                (1000003, "1,000,003rd"),
                (1000004, "1,000,004th"),
                (-30, "-30th"),
                (-100, "-100th"),
                (-101, "-101st"),
                (-102, "-102nd"),
                (-103, "-103rd"),
                (-104, "-104th"),
            };

            foreach ((int input, string output) in data)
                Assert.AreEqual(output, input.MakeOrdinalDigits());
        }

        [TestMethod]
        public void TestOrdinalDigitsStrings()
        {
            List<(string?, string)> data = new()
            {
                ("0", "0th"),
                ("1", "1st"),
                ("2", "2nd"),
                ("3", "3rd"),
                ("4", "4th"),
                ("5", "5th"),
                ("6", "6th"),
                ("7", "7th"),
                ("8", "8th"),
                ("9", "9th"),
                ("10", "10th"),
                ("11", "11th"),
                ("12", "12th"),
                ("13", "13th"),
                ("14", "14th"),
                ("15", "15th"),
                ("16", "16th"),
                ("17", "17th"),
                ("18", "18th"),
                ("19", "19th"),
                ("20", "20th"),
                ("21", "21st"),
                ("22", "22nd"),
                ("23", "23rd"),
                ("24", "24th"),
                ("25", "25th"),
                ("26", "26th"),
                ("27", "27th"),
                ("28", "28th"),
                ("29", "29th"),
                ("30", "30th"),
                ("100", "100th"),
                ("101", "101st"),
                ("102", "102nd"),
                ("103", "103rd"),
                ("104", "104th"),
                ("105", "105th"),
                ("106", "106th"),
                ("107", "107th"),
                ("108", "108th"),
                ("109", "109th"),
                ("-110", "-110th"),
                ("-111", "-111th"),
                ("-112", "-112th"),
                ("-113", "-113th"),
                ("-114", "-114th"),
                ("115", "115th"),
                ("116", "116th"),
                ("2000", "2000th"),
                ("2001", "2001st"),
                ("2002", "2002nd"),
                ("2003", "2003rd"),
                ("2004", "2004th"),
                ("2022", "2022nd"),
                ("2023", "2023rd"),
                ("2024", "2024th"),
                ("1000000", "1000000th"),
                ("1000001", "1000001st"),
                ("1000002", "1000002nd"),
                ("1000003", "1000003rd"),
                ("1,000,004", "1,000,004th"),
                (null, ""),
                ("", ""),
                (" 0 ", " 0th "),
                (">>>1<<<", ">>>1st<<<"),
                ("2!", "2nd!"),
                ("f(3)", "f(3rd)"),
                ("...100...", "...100th..."),
            };

            foreach ((string? input, string output) in data)
                Assert.AreEqual(output, input.MakeOrdinalDigits());
        }
    }
}
