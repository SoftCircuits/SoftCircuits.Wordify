// Copyright (c) 2023 Jonathan Wood (www.softcircuits.com)
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
            Assert.AreEqual($"\"\"", ((string)null!).WrapInQuotes());
            Assert.AreEqual($"\"\"", "".WrapInQuotes());
            Assert.AreEqual($"\"a\"", "a".WrapInQuotes());
            Assert.AreEqual($"\"abc\"", "abc".WrapInQuotes());

            Assert.AreEqual($"''", ((string)null!).WrapInSingleQuotes());
            Assert.AreEqual($"''", "".WrapInSingleQuotes());
            Assert.AreEqual($"'a'", "a".WrapInSingleQuotes());
            Assert.AreEqual($"'abc'", "abc".WrapInSingleQuotes());

            Assert.AreEqual($"\"a\"", 'a'.WrapInQuotes());
            Assert.AreEqual($"'a'", 'a'.WrapInSingleQuotes());
        }
    }
}
