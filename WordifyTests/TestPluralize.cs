// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

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
            ("WMD", "WMDs"),

            // Ends in S
            ("class", "classes"),
            ("lunch", "lunches"),
            ("push", "pushes"),
            ("fox", "foxes"),
            ("pez", "pezes"),
            ("Pez", "Pezes"),

            // Ends in Y
            ("boy", "boys"),
            ("party", "parties"),
            ("day", "days"),
            ("agony", "agonies"),
            ("y", "ys"),
            ("Boy", "Boys"),

            // Irregular nouns
            ("child", "children"),
            ("person", "people"),
            ("potato", "potatoes"),
            ("ellipsis", "ellipses"),
            ("life", "lives"),
            ("Life", "Lives"),

            // Defective nouns
            ("buffalo", "buffalo"),
            ("glass", "glass"),
            ("pants", "pants"),
            ("sheep", "sheep"),
            ("Sheep", "Sheep"),

            // Non-letter characters
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
