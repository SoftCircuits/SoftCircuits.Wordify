using SoftCircuits.Wordify;

namespace WordifyTests
{
    [TestClass]
    public class TestPluralize
    {
        private static readonly List<(string, string)> Data = new()
        {
            ("cat", "cats"),
            ("dog", "dogs"),
            ("door", "doors"),
            ("engagement", "engagements"),

            ("class", "classes"),
            ("lunch", "lunches"),
            ("push", "pushes"),
            ("fox", "foxes"),
            ("pez", "pezes"),

            ("boy", "boys"),
            ("party", "parties"),
            ("day", "days"),
            ("agony", "agonies"),
            ("y", "ys"),

            ("buffalo", "buffalo"),
            ("glass", "glass"),
            ("pants", "pants"),
            ("sheep", "sheep"),

            ("", ""),
            (" ", " "),
            ("    t    ", "    ts    "),
            (" This is a test! ", " This is a tests! "),
        };

        [TestMethod]
        public void Test()
        {
            foreach ((string Original, string Plural) item in Data)
            {
                Assert.AreEqual(item.Plural, Wordify.Pluralize(item.Original));
                Assert.AreEqual(item.Original, Wordify.Singularize(item.Plural));
            }
        }
    }
}
