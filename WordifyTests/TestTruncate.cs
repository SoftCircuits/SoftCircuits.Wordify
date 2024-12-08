// Copyright (c) 2023-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using SoftCircuits.Wordify;

namespace WordifyTests
{
    [TestClass]
    public class TestTruncate
    {
        private class TruncateTest(string input, TruncateOption options)
        {
            public string Input { get; set; } = input;
            public TruncateOption Options { get; set; } = options;
            public List<TruncateResult> Results { get; set; } = [];
        }

        private class TruncateResult(int maxLength, string result)
        {
            public int MaxLength { get; set; } = maxLength;
            public string Result { get; set; } = result;
        }

        private readonly List<TruncateTest> TruncateTestData =
        [
            new("This is a test.", TruncateOption.TrimPartialWords | TruncateOption.AppendEllipsis)
            {
                Results =
                [
                    new(15, "This is a test."),
                    new(14, "This is a..."),
                    new(13, "This is a..."),
                    new(12, "This is a..."),
                    new(11, "This is..."),
                    new(10, "This is..."),
                    new(9, "This..."),
                    new(8, "This..."),
                    new(7, "This..."),
                    new(6, "Thi..."),
                    new(5, "Th..."),
                    new(4, "T..."),
                    new(3, "Thi"),
                    new(2, "Th"),
                    new(1, "T"),
                    new(0, string.Empty),
                ]
            },
            new("This is a test.", TruncateOption.AppendEllipsis)
            {
                Results =
                [
                    new(15, "This is a test."),
                    new(14, "This is a t..."),
                    new(13, "This is a ..."),
                    new(12, "This is a..."),
                    new(11, "This is ..."),
                    new(10, "This is..."),
                    new(9, "This i..."),
                    new(8, "This ..."),
                    new(7, "This..."),
                    new(6, "Thi..."),
                    new(5, "Th..."),
                    new(4, "T..."),
                    new(3, "Thi"),
                    new(2, "Th"),
                    new(1, "T"),
                    new(0, string.Empty),
                ]
            },
            new("This is a test.", TruncateOption.TrimPartialWords)
            {
                Results =
                [
                    new(15, "This is a test."),
                    new(14, "This is a test"),
                    new(13, "This is a"),
                    new(12, "This is a"),
                    new(11, "This is a"),
                    new(10, "This is a"),
                    new(9, "This is a"),
                    new(8, "This is"),
                    new(7, "This is"),
                    new(6, "This"),
                    new(5, "This"),
                    new(4, "This"),
                    new(3, "Thi"),
                    new(2, "Th"),
                    new(1, "T"),
                    new(0, string.Empty),
                ]
            },
            new("This is a test.", TruncateOption.None)
            {
                Results =
                [
                    new(15, "This is a test."),
                    new(14, "This is a test"),
                    new(13, "This is a tes"),
                    new(12, "This is a te"),
                    new(11, "This is a t"),
                    new(10, "This is a "),
                    new(9, "This is a"),
                    new(8, "This is "),
                    new(7, "This is"),
                    new(6, "This i"),
                    new(5, "This "),
                    new(4, "This"),
                    new(3, "Thi"),
                    new(2, "Th"),
                    new(1, "T"),
                    new(0, string.Empty),
                ]
            }
        ];

        [TestMethod]
        public void Test()
        {
            foreach (TruncateTest test in TruncateTestData)
            {
                foreach (TruncateResult result in test.Results)
                {
                    Assert.AreEqual(result.Result, test.Input.Truncate(result.MaxLength, test.Options));
                }
            }
        }
    }
}
