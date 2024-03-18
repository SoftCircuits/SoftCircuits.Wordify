// Copyright (c) 2023-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using SoftCircuits.Wordify;

namespace WordifyTests
{
    [TestClass]
    public class TestCollections
    {
        private class TestResult(CollectionOption options, string result)
        {
            public CollectionOption Options { get; set; } = options;
            public string Result { get; set; } = result;
        }

        private class TestItem(IEnumerable<string?> items, TestCollections.TestResult[] results)
        {
            public IEnumerable<string?> Items { get; set; } = items;
            public TestResult[] Results { get; set; } = results;
        }

        private static readonly TestItem[] TestItems =
        [
            new TestItem([],
                [
                    new TestResult(CollectionOption.AndConjunction, string.Empty),
                    new TestResult(CollectionOption.OxfordComma, string.Empty),
                    new TestResult(CollectionOption.OrConjunction, string.Empty),
                    new TestResult(CollectionOption.OrConjunction | CollectionOption.OxfordComma, string.Empty),
                ]),
            new TestItem([ "one" ],
                [
                    new TestResult(CollectionOption.AndConjunction, "one"),
                    new TestResult(CollectionOption.OxfordComma, "one"),
                    new TestResult(CollectionOption.OrConjunction, "one"),
                    new TestResult(CollectionOption.OrConjunction | CollectionOption.OxfordComma, "one"),
                ]),
            new TestItem([ "one", "two" ],
                [
                    new TestResult(CollectionOption.AndConjunction, "one and two"),
                    new TestResult(CollectionOption.OxfordComma, "one, and two"),
                    new TestResult(CollectionOption.OrConjunction, "one or two"),
                    new TestResult(CollectionOption.OrConjunction | CollectionOption.OxfordComma, "one, or two"),
                ]),
            new TestItem([ "one", "two", "three" ],
                [
                    new TestResult(CollectionOption.AndConjunction, "one, two and three"),
                    new TestResult(CollectionOption.OxfordComma, "one, two, and three"),
                    new TestResult(CollectionOption.OrConjunction, "one, two or three"),
                    new TestResult(CollectionOption.OrConjunction | CollectionOption.OxfordComma, "one, two, or three"),
                ]),
            new TestItem([ "one", null, "three" ],
                [
                    new TestResult(CollectionOption.AndConjunction, "one and three"),
                    new TestResult(CollectionOption.OxfordComma, "one, and three"),
                    new TestResult(CollectionOption.OrConjunction, "one or three"),
                    new TestResult(CollectionOption.OrConjunction | CollectionOption.OxfordComma, "one, or three"),
                ]),
            new TestItem([ "one", null, null ],
                [
                    new TestResult(CollectionOption.AndConjunction, "one"),
                    new TestResult(CollectionOption.OxfordComma, "one"),
                    new TestResult(CollectionOption.OrConjunction, "one"),
                    new TestResult(CollectionOption.OrConjunction | CollectionOption.OxfordComma, "one"),
                ]),
            new TestItem([ null, null, null ],
                [
                    new TestResult(CollectionOption.AndConjunction, string.Empty),
                    new TestResult(CollectionOption.OxfordComma, string.Empty),
                    new TestResult(CollectionOption.OrConjunction, string.Empty),
                    new TestResult(CollectionOption.OrConjunction | CollectionOption.OxfordComma, string.Empty),
                ]),
        ];

        [TestMethod]
        public void Test()
        {
            foreach (TestItem item in TestItems)
            {
                foreach (TestResult result in item.Results)
                    Assert.AreEqual(result.Result, item.Items.Wordify(result.Options));
            }
        }
    }
}
