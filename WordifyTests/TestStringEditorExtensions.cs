// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using SoftCircuits.Wordify.Extensions;
using SoftCircuits.Wordify.Helpers;

namespace WordifyTests
{
    /// <summary>
    /// Tests for private <see cref="StringEditor_Old"/> utility class.
    /// </summary>
    [TestClass]
    public class TestStringEditorExtensions
    {
        [TestMethod]
        public void TestCharacterTypes()
        {
            StringEditor editor = new(" This is a test! ");

            Assert.IsFalse(editor.IsWordCharacter(0));
            Assert.IsTrue(editor.IsWordCharacter(1)); // T
            Assert.IsTrue(editor.IsWordCharacter(2)); // h
            Assert.IsTrue(editor.IsWordCharacter(3)); // i
            Assert.IsTrue(editor.IsWordCharacter(4)); // s
            Assert.IsFalse(editor.IsWordCharacter(5));
            Assert.IsTrue(editor.IsWordCharacter(6)); // i
            Assert.IsTrue(editor.IsWordCharacter(7)); // s
            Assert.IsFalse(editor.IsWordCharacter(8));
            Assert.IsTrue(editor.IsWordCharacter(9)); // a
            Assert.IsFalse(editor.IsWordCharacter(10));
            Assert.IsTrue(editor.IsWordCharacter(11)); // t
            Assert.IsTrue(editor.IsWordCharacter(12)); // e
            Assert.IsTrue(editor.IsWordCharacter(13)); // s
            Assert.IsTrue(editor.IsWordCharacter(14)); // t
            Assert.IsFalse(editor.IsWordCharacter(15)); // !
            Assert.IsFalse(editor.IsWordCharacter(16));

            Assert.IsFalse(editor.IsEndOfSentenceCharacter(0));
            Assert.IsFalse(editor.IsEndOfSentenceCharacter(1)); // T
            Assert.IsFalse(editor.IsEndOfSentenceCharacter(2)); // h
            Assert.IsFalse(editor.IsEndOfSentenceCharacter(3)); // i
            Assert.IsFalse(editor.IsEndOfSentenceCharacter(4)); // s
            Assert.IsFalse(editor.IsEndOfSentenceCharacter(5));
            Assert.IsFalse(editor.IsEndOfSentenceCharacter(6)); // i
            Assert.IsFalse(editor.IsEndOfSentenceCharacter(7)); // s
            Assert.IsFalse(editor.IsEndOfSentenceCharacter(8));
            Assert.IsFalse(editor.IsEndOfSentenceCharacter(9)); // a
            Assert.IsFalse(editor.IsEndOfSentenceCharacter(10));
            Assert.IsFalse(editor.IsEndOfSentenceCharacter(11)); // t
            Assert.IsFalse(editor.IsEndOfSentenceCharacter(12)); // e
            Assert.IsFalse(editor.IsEndOfSentenceCharacter(13)); // s
            Assert.IsFalse(editor.IsEndOfSentenceCharacter(14)); // t
            Assert.IsTrue(editor.IsEndOfSentenceCharacter(15)); // !
            Assert.IsFalse(editor.IsEndOfSentenceCharacter(16));
        }

        [TestMethod]
        public void TestFind()
        {
            StringEditor editor = new(" This is a test! ");

            Assert.IsTrue(editor.FindFirstWord(out int startIndex, out int endIndex));
            Assert.AreEqual(1, startIndex);
            Assert.AreEqual(5, endIndex);

            Assert.IsTrue(editor.FindLastWord(out startIndex, out endIndex));
            Assert.AreEqual(11, startIndex);
            Assert.AreEqual(15, endIndex);

            editor.Initialize(" ");
            Assert.IsFalse(editor.FindFirstWord(out _, out _));
            Assert.IsFalse(editor.FindLastWord(out _, out _));
        }

        [TestMethod]
        public void TestIndexOf()
        {
            StringEditor editor = new(" This is a test! ");

            Assert.AreEqual(0, editor.IndexOf(' '));
            Assert.AreEqual(1, editor.IndexOf('T'));
            Assert.AreEqual(2, editor.IndexOf('h'));
            Assert.AreEqual(3, editor.IndexOf('i'));
            Assert.AreEqual(4, editor.IndexOf('s'));
            Assert.AreEqual(1, editor.IndexOf('t', -1, true));

            Assert.AreEqual(-1, editor.IndexOf('E'));
            Assert.AreEqual(12, editor.IndexOf('E', -1, true));

            Assert.AreEqual(-1, editor.IndexOf("EST"));
            Assert.AreEqual(12, editor.IndexOf("EST", -1, true));

            Assert.AreEqual(-1, editor.IndexOf(c => c == 'E'));
            Assert.AreEqual(12, editor.IndexOf(c => char.ToUpper(c) == 'E'));


            Assert.AreEqual(16, editor.LastIndexOf(' '));
            Assert.AreEqual(1, editor.LastIndexOf('T'));
            Assert.AreEqual(2, editor.LastIndexOf('h'));
            Assert.AreEqual(6, editor.LastIndexOf('i'));
            Assert.AreEqual(13, editor.LastIndexOf('s'));
            Assert.AreEqual(14, editor.LastIndexOf('T', -1, true));

            Assert.AreEqual(-1, editor.LastIndexOf('E'));
            Assert.AreEqual(12, editor.LastIndexOf('E', -1, true));

            Assert.AreEqual(-1, editor.LastIndexOf("EST"));
            Assert.AreEqual(12, editor.LastIndexOf("EST", -1, true));

            Assert.AreEqual(-1, editor.LastIndexOf(c => c == 'E'));
            Assert.AreEqual(12, editor.LastIndexOf(c => char.ToUpper(c) == 'E'));


            string s = editor;
            for (int i = 0; i < s.Length; i++)
            {
                int pos1 = s.IndexOf(s[i]);
                int pos2 = editor.IndexOf(s[i]);
                Assert.AreEqual(pos1, pos2);

                pos1 = s.IndexOf(char.ToUpper(s[i]));
                pos2 = editor.IndexOf(char.ToUpper(s[i]));
                Assert.AreEqual(pos1, pos2);

                pos1 = s.IndexOf(char.ToUpper(s[i]).ToString(), StringComparison.OrdinalIgnoreCase);
                pos2 = editor.IndexOf(char.ToUpper(s[i]).ToString(), -1, true);
                Assert.AreEqual(pos1, pos2);

                pos1 = s.LastIndexOf(s[i]);
                pos2 = editor.LastIndexOf(s[i]);
                Assert.AreEqual(pos1, pos2);

                pos1 = s.LastIndexOf(char.ToUpper(s[i]));
                pos2 = editor.LastIndexOf(char.ToUpper(s[i]));
                Assert.AreEqual(pos1, pos2);

                pos1 = s.LastIndexOf(char.ToUpper(s[i]).ToString(), StringComparison.OrdinalIgnoreCase);
                pos2 = editor.LastIndexOf(char.ToUpper(s[i]).ToString(), -1, true);
                Assert.AreEqual(pos1, pos2);
            }
        }

        class TestInfo
        {
            public string Term { get; set; }
            public string Text { get; set; }
            public int[] Matches { get; set; }
            public int[] IgnoreCaseMatches { get; set; }

            public TestInfo(string term, string text, int[] matches, int[]? ignoreCaseMatches = null)
            {
                Term = term;
                Text = text;
                Matches = matches;
                IgnoreCaseMatches = ignoreCaseMatches ?? matches;
            }
        }

        [TestMethod]
        public void TestMatchesAt()
        {
            TestInfo[] tests =
            {
                new("is", " This is a test! ", new[] { 3, 6 }, new[] { 3, 6 }),
                new("IS", " This is a test! ", Array.Empty<int>(), new[] { 3, 6 }),
                new("test", " This is a test! ", new[] { 11 }, new[] { 11 }),
                new("TEST", " This is a test! ", Array.Empty<int>(), new[] { 11 }),
            };

            StringEditor editor = new(null);
            foreach (TestInfo test in tests)
            {
                editor.Initialize(test.Text);
                for (int i = 0; i < test.Text.Length; i++)
                {
                    bool match = test.Matches.Contains(i);
                    Assert.AreEqual(match, editor.MatchesAt(test.Term, i, false));

                    match = test.Matches.Contains(i - (test.Term.Length - 1));
                    Assert.AreEqual(match, editor.MatchesEndingAt(test.Term, i, false));

                    match = test.IgnoreCaseMatches.Contains(i);
                    Assert.AreEqual(match, editor.MatchesAt(test.Term, i, true));

                    match = test.IgnoreCaseMatches.Contains(i - (test.Term.Length - 1));
                    Assert.AreEqual(match, editor.MatchesEndingAt(test.Term, i, true));
                }
            }
        }

        [TestMethod]
        public void TestContains()
        {
            StringEditor editor = new(" This is a test! ");

            Assert.IsTrue(editor.Contains('T'));
            Assert.IsTrue(editor.Contains('t'));
            Assert.IsFalse(editor.Contains('H'));
            Assert.IsTrue(editor.Contains('h'));
            Assert.IsFalse(editor.Contains('I'));
            Assert.IsTrue(editor.Contains('i'));
            Assert.IsFalse(editor.Contains('S'));
            Assert.IsTrue(editor.Contains('s'));

            Assert.IsFalse(editor.Contains("IS"));
            Assert.IsTrue(editor.Contains("is"));

            Assert.IsFalse(editor.Contains(c => c == 'H'));
            Assert.IsTrue(editor.Contains(c => c == 'h'));
        }
    }
}
