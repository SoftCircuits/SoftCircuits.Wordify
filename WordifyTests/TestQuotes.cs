using SoftCircuits.Wordify;

namespace WordifyTests
{
    [TestClass]
    public class TestQuotes
    {
        [TestMethod]
        public void Tests()
        {
            Assert.AreEqual($"\"\"", Wordify.DoubleQuotes(null));
            Assert.AreEqual($"\"\"", Wordify.DoubleQuotes(""));
            Assert.AreEqual($"\"a\"", Wordify.DoubleQuotes("a"));
            Assert.AreEqual($"\"abc\"", Wordify.DoubleQuotes("abc"));

            Assert.AreEqual($"''", Wordify.SingleQuotes(null));
            Assert.AreEqual($"''", Wordify.SingleQuotes(""));
            Assert.AreEqual($"'a'", Wordify.SingleQuotes("a"));
            Assert.AreEqual($"'abc'", Wordify.SingleQuotes("abc"));

            Assert.AreEqual($"\"a\"", Wordify.DoubleQuotes('a'));
            Assert.AreEqual($"'a'", Wordify.SingleQuotes('a'));
        }
    }
}
