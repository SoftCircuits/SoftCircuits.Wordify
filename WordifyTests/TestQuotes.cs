using SoftCircuits.Wordify;

namespace WordifyTests
{
    [TestClass]
    public class TestQuotes
    {
        [TestMethod]
        public void Tests()
        {
            Assert.AreEqual($"\"\"", Wordify.SetInQuotes(null));
            Assert.AreEqual($"\"\"", Wordify.SetInQuotes(""));
            Assert.AreEqual($"\"a\"", Wordify.SetInQuotes("a"));
            Assert.AreEqual($"\"abc\"", Wordify.SetInQuotes("abc"));

            Assert.AreEqual($"''", Wordify.SetInSingleQuotes(null));
            Assert.AreEqual($"''", Wordify.SetInSingleQuotes(""));
            Assert.AreEqual($"'a'", Wordify.SetInSingleQuotes("a"));
            Assert.AreEqual($"'abc'", Wordify.SetInSingleQuotes("abc"));

            Assert.AreEqual($"\"a\"", Wordify.SetInQuotes('a'));
            Assert.AreEqual($"'a'", Wordify.SetInSingleQuotes('a'));
        }
    }
}
