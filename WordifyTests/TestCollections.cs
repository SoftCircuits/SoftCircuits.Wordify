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
            public JoinOption Options { get; set; }
            public string Result { get; set; }

            public TestResult(JoinOption options, string result)
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
                    new TestResult(JoinOption.AndConjunction, string.Empty),
                    new TestResult(JoinOption.OxfordComma, string.Empty),
                    new TestResult(JoinOption.OrConjunction, string.Empty),
                    new TestResult(JoinOption.OrConjunction | JoinOption.OxfordComma, string.Empty),
                }),
            new TestItem(new[] { "one" },
                new[]
                {
                    new TestResult(JoinOption.AndConjunction, "one"),
                    new TestResult(JoinOption.OxfordComma, "one"),
                    new TestResult(JoinOption.OrConjunction, "one"),
                    new TestResult(JoinOption.OrConjunction | JoinOption.OxfordComma, "one"),
                }),
            new TestItem(new[] { "one", "two" },
                new[]
                {
                    new TestResult(JoinOption.AndConjunction, "one and two"),
                    new TestResult(JoinOption.OxfordComma, "one, and two"),
                    new TestResult(JoinOption.OrConjunction, "one or two"),
                    new TestResult(JoinOption.OrConjunction | JoinOption.OxfordComma, "one, or two"),
                }),
            new TestItem(new[] { "one", "two", "three" },
                new[]
                {
                    new TestResult(JoinOption.AndConjunction, "one, two and three"),
                    new TestResult(JoinOption.OxfordComma, "one, two, and three"),
                    new TestResult(JoinOption.OrConjunction, "one, two or three"),
                    new TestResult(JoinOption.OrConjunction | JoinOption.OxfordComma, "one, two, or three"),
                }),
            new TestItem(new[] { "one", null, "three" },
                new[]
                {
                    new TestResult(JoinOption.AndConjunction, "one and three"),
                    new TestResult(JoinOption.OxfordComma, "one, and three"),
                    new TestResult(JoinOption.OrConjunction, "one or three"),
                    new TestResult(JoinOption.OrConjunction | JoinOption.OxfordComma, "one, or three"),
                }),
            new TestItem(new[] { "one", null, null },
                new[]
                {
                    new TestResult(JoinOption.AndConjunction, "one"),
                    new TestResult(JoinOption.OxfordComma, "one"),
                    new TestResult(JoinOption.OrConjunction, "one"),
                    new TestResult(JoinOption.OrConjunction | JoinOption.OxfordComma, "one"),
                }),
            new TestItem(new string?[] { null, null, null },
                new[]
                {
                    new TestResult(JoinOption.AndConjunction, string.Empty),
                    new TestResult(JoinOption.OxfordComma, string.Empty),
                    new TestResult(JoinOption.OrConjunction, string.Empty),
                    new TestResult(JoinOption.OrConjunction | JoinOption.OxfordComma, string.Empty),
                }),
        };

        [TestMethod]
        public void Test()
        {
            foreach (TestItem item in TestItems)
            {
                foreach (TestResult result in item.Results)
                    Assert.AreEqual(result.Result, Wordify.Join(item.Items, result.Options));
            }
        }
    }
}
