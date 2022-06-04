// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using SoftCircuits.Wordify;

namespace WordifyTests
{
    [TestClass]
    public class TestQuotes
    {
        [TestMethod]
        public void Tests()
        {
            Assert.AreEqual($"\"\"", Wordify.WrapInQuotes(null));
            Assert.AreEqual($"\"\"", Wordify.WrapInQuotes(""));
            Assert.AreEqual($"\"a\"", Wordify.WrapInQuotes("a"));
            Assert.AreEqual($"\"abc\"", Wordify.WrapInQuotes("abc"));

            Assert.AreEqual($"''", Wordify.WrapInSingleQuotes(null));
            Assert.AreEqual($"''", Wordify.WrapInSingleQuotes(""));
            Assert.AreEqual($"'a'", Wordify.WrapInSingleQuotes("a"));
            Assert.AreEqual($"'abc'", Wordify.WrapInSingleQuotes("abc"));

            Assert.AreEqual($"\"a\"", Wordify.WrapInQuotes('a'));
            Assert.AreEqual($"'a'", Wordify.WrapInSingleQuotes('a'));
        }
    }
}
