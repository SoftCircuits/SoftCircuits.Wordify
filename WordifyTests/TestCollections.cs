// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using SoftCircuits.Wordify;

namespace WordifyTests
{
    [TestClass]
    public class TestCollections
    {
        private class TestResult
        {
            public CollectionOption Options { get; set; }
            public string Result { get; set; }

            public TestResult(CollectionOption options, string result)
            {
                Options = options;
                Result = result;
            }
        }

        private class TestItem
        {
            public IEnumerable<string?> Items { get; set; }
            public TestResult[] Results { get; set; }

            public TestItem(IEnumerable<string?> items, TestResult[] results)
            {
                Items = items;
                Results = results;
            }
        }

        private static readonly TestItem[] TestItems = new[]
        {
            new TestItem(Array.Empty<string?>(),
                new[]
                {
                    new TestResult(CollectionOption.AndConjunction, string.Empty),
                    new TestResult(CollectionOption.OxfordComma, string.Empty),
                    new TestResult(CollectionOption.OrConjunction, string.Empty),
                    new TestResult(CollectionOption.OrConjunction | CollectionOption.OxfordComma, string.Empty),
                }),
            new TestItem(new[] { "one" },
                new[]
                {
                    new TestResult(CollectionOption.AndConjunction, "one"),
                    new TestResult(CollectionOption.OxfordComma, "one"),
                    new TestResult(CollectionOption.OrConjunction, "one"),
                    new TestResult(CollectionOption.OrConjunction | CollectionOption.OxfordComma, "one"),
                }),
            new TestItem(new[] { "one", "two" },
                new[]
                {
                    new TestResult(CollectionOption.AndConjunction, "one and two"),
                    new TestResult(CollectionOption.OxfordComma, "one, and two"),
                    new TestResult(CollectionOption.OrConjunction, "one or two"),
                    new TestResult(CollectionOption.OrConjunction | CollectionOption.OxfordComma, "one, or two"),
                }),
            new TestItem(new[] { "one", "two", "three" },
                new[]
                {
                    new TestResult(CollectionOption.AndConjunction, "one, two and three"),
                    new TestResult(CollectionOption.OxfordComma, "one, two, and three"),
                    new TestResult(CollectionOption.OrConjunction, "one, two or three"),
                    new TestResult(CollectionOption.OrConjunction | CollectionOption.OxfordComma, "one, two, or three"),
                }),
            new TestItem(new[] { "one", null, "three" },
                new[]
                {
                    new TestResult(CollectionOption.AndConjunction, "one and three"),
                    new TestResult(CollectionOption.OxfordComma, "one, and three"),
                    new TestResult(CollectionOption.OrConjunction, "one or three"),
                    new TestResult(CollectionOption.OrConjunction | CollectionOption.OxfordComma, "one, or three"),
                }),
            new TestItem(new[] { "one", null, null },
                new[]
                {
                    new TestResult(CollectionOption.AndConjunction, "one"),
                    new TestResult(CollectionOption.OxfordComma, "one"),
                    new TestResult(CollectionOption.OrConjunction, "one"),
                    new TestResult(CollectionOption.OrConjunction | CollectionOption.OxfordComma, "one"),
                }),
            new TestItem(new string?[] { null, null, null },
                new[]
                {
                    new TestResult(CollectionOption.AndConjunction, string.Empty),
                    new TestResult(CollectionOption.OxfordComma, string.Empty),
                    new TestResult(CollectionOption.OrConjunction, string.Empty),
                    new TestResult(CollectionOption.OrConjunction | CollectionOption.OxfordComma, string.Empty),
                }),
        };

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
