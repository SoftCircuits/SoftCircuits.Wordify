// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using SoftCircuits.Wordify;

namespace WordifyTests
{
    [TestClass]
    public class TestSpreadsheet
    {
        [TestMethod]
        public void Test()
        {
            Assert.AreEqual("A", (-1).ToSpreadsheetColumn());
            Assert.AreEqual("A", 0.ToSpreadsheetColumn());
            Assert.AreEqual("A", 1.ToSpreadsheetColumn());
            Assert.AreEqual("B", 2.ToSpreadsheetColumn());
            Assert.AreEqual("C", 3.ToSpreadsheetColumn());
            Assert.AreEqual("Z", 26.ToSpreadsheetColumn());
            Assert.AreEqual("AA", 27.ToSpreadsheetColumn());
            Assert.AreEqual("AB", 28.ToSpreadsheetColumn());

            Assert.AreEqual("DKJ", 3000.ToSpreadsheetColumn());
            Assert.AreEqual("HVT", 6000.ToSpreadsheetColumn());
            Assert.AreEqual("ECCN", 90000.ToSpreadsheetColumn());
        }

        [TestMethod]
        public void TestParse()
        {
            Assert.IsFalse(((string)null!).TryParseSpreadsheetColumn(out int _));
            Assert.IsFalse(string.Empty.TryParseSpreadsheetColumn(out int _));
            Assert.IsFalse("@@@".TryParseSpreadsheetColumn(out int _));

            Assert.AreEqual(1, "A".ParseSpreadsheetColumn());
            Assert.AreEqual(2, "B".ParseSpreadsheetColumn());
            Assert.AreEqual(3, " C ".ParseSpreadsheetColumn());
            Assert.AreEqual(26, " z ".ParseSpreadsheetColumn());
            Assert.AreEqual(27, " aa ".ParseSpreadsheetColumn());
            Assert.AreEqual(28, "ab".ParseSpreadsheetColumn());

            Assert.AreEqual(3000, " dkj ".ParseSpreadsheetColumn());
            Assert.AreEqual(6000, "HVT".ParseSpreadsheetColumn());
            Assert.AreEqual(90000, "eccn".ParseSpreadsheetColumn());
        }
    }
}
