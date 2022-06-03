using SoftCircuits.Wordify;

namespace WordifyTests
{
    [TestClass]
    public class TestStringEditor
    {
        [TestMethod]
        public void TestAppend()
        {
            StringEditor editor = new("abc");

            editor.Append("def");
            Assert.AreEqual("abcdef", editor);

            editor.Append("xyz");
            Assert.AreEqual("abcdefxyz", editor);

            editor.Append("abcdefghijklmnopqrstuvwxyz");
            Assert.AreEqual("abcdefxyzabcdefghijklmnopqrstuvwxyz", editor);
        }

        [TestMethod]
        public void TestDelete()
        {
            StringEditor editor = new("abcdefghijklmnopqrstuvwxyz");

            editor.Delete(editor.Length, 1000);
            Assert.AreEqual("abcdefghijklmnopqrstuvwxyz", editor);

            editor.Delete(editor.Length - 1, 1000);
            Assert.AreEqual("abcdefghijklmnopqrstuvwxy", editor);

            editor.Delete(0, 1);
            Assert.AreEqual("bcdefghijklmnopqrstuvwxy", editor);

            editor.Delete(6, 5);
            Assert.AreEqual("bcdefgmnopqrstuvwxy", editor);

            editor.Delete(3, 1000);
            Assert.AreEqual("bcd", editor);
        }

        [TestMethod]
        public void TestInsert()
        {
            StringEditor editor = new("abc");

            editor.Insert(1000, "xyz");
            Assert.AreEqual("abcxyz", editor);

            editor.Insert(0, "123");
            Assert.AreEqual("123abcxyz", editor);

            editor.Insert(4, "@#$");
            Assert.AreEqual("123a@#$bcxyz", editor);

            editor.Insert(7, "abcdefghijklmnopqrstuvwxyz");
            Assert.AreEqual("123a@#$abcdefghijklmnopqrstuvwxyzbcxyz", editor);

            editor = new("abc");

            editor.Insert(1000, "xyz", 1);
            Assert.AreEqual("abcxyz", editor);

            editor.Insert(0, "123", 2);
            Assert.AreEqual("123cxyz", editor);

            editor.Insert(4, "@#$", 0);
            Assert.AreEqual("123c@#$xyz", editor);

            editor.Insert(7, "abcdefghijklmnopqrstuvwxyz", 1000);
            Assert.AreEqual("123c@#$abcdefghijklmnopqrstuvwxyz", editor);

            editor.Insert(0, "abc", 1000);
            Assert.AreEqual("abc", editor);

            editor = (StringEditor)"abcdefghijklmnopqrstuvwxyz";

            editor.Insert(2, "123", 5);
            Assert.AreEqual("ab123hijklmnopqrstuvwxyz", editor);

            editor.Insert(7, "...", 8);
            Assert.AreEqual("ab123hi...rstuvwxyz", editor);
        }

        [TestMethod]
        public void TestCopy()
        {
            StringEditor editor = new("abcdefghijklmnopqrstuvwxyz");

            editor.Copy(editor, editor.Length - 4);
            Assert.AreEqual("abcdefghijklmnopqrstuvabcd", editor);

            editor.Copy("12345", 4);
            Assert.AreEqual("abcd12345jklmnopqrstuvabcd", editor);

            editor.Copy("@#$%", 10);
            Assert.AreEqual("abcd12345j@#$%opqrstuvabcd", editor);
        }





        [TestMethod]
        public void TestIndexOf()
        {
            StringEditor editor = new("abcdefghijklmnopqrstuvwxyz");

            Assert.AreEqual(2, editor.IndexOf('c'));
            Assert.AreEqual(-1, editor.IndexOf('c', 5));
            Assert.AreEqual(9, editor.IndexOf("jkl"));
            Assert.AreEqual(9, editor.IndexOf("jkl", 9));
            Assert.AreEqual(-1, editor.IndexOf("jkl", 14));
            Assert.AreEqual(23, editor.IndexOf("xyz"));
            Assert.AreEqual(-1, editor.IndexOf("yz@"));
            Assert.AreEqual(1, editor.IndexOf(CharExtensions.IsConsonant));
            Assert.AreEqual(-1, editor.IndexOf(CharExtensions.IsConsonant, 26));
            Assert.AreEqual(-1, editor.IndexOf(char.IsWhiteSpace));

            Assert.AreEqual(2, editor.LastIndexOf('c'));
            Assert.AreEqual(-1, editor.LastIndexOf('c', 1));

            Assert.AreEqual(9, editor.LastIndexOf("jkl"));
            Assert.AreEqual(9, editor.LastIndexOf("jkl", 11));
            Assert.AreEqual(-1, editor.LastIndexOf("jkl", 8));
            Assert.AreEqual(0, editor.LastIndexOf("abc"));
            Assert.AreEqual(-1, editor.LastIndexOf("@ab"));

            Assert.AreEqual(25, editor.LastIndexOf(CharExtensions.IsConsonant));
            Assert.AreEqual(-1, editor.LastIndexOf(CharExtensions.IsConsonant, 0));
            Assert.AreEqual(-1, editor.LastIndexOf(char.IsWhiteSpace));

            Assert.IsTrue(editor.Contains('l'));
            Assert.IsFalse(editor.Contains('7'));
            Assert.IsTrue(editor.Contains("lmn"));
            Assert.IsFalse(editor.Contains("lemon"));
            Assert.IsTrue(editor.Contains(CharExtensions.IsVowel));
            Assert.IsFalse(editor.Contains(char.IsWhiteSpace));
        }

        [TestMethod]
        public void TestFind()
        {
            StringEditor editor = new("Now is the time");
            Assert.IsTrue(editor.FindFirstWord(out int startIndex, out int endIndex));
            Assert.AreEqual("Now", editor.Substring(startIndex, endIndex - startIndex));
            Assert.IsTrue(editor.FindLastWord(out startIndex, out endIndex));
            Assert.AreEqual("time", editor.Substring(startIndex, endIndex - startIndex));

            editor = new(">>>Now is the time!<<<");
            Assert.IsTrue(editor.FindFirstWord(out startIndex, out endIndex));
            Assert.AreEqual("Now", editor.Substring(startIndex, endIndex - startIndex));
            Assert.IsTrue(editor.FindLastWord(out startIndex, out endIndex));
            Assert.AreEqual("time", editor.Substring(startIndex, endIndex - startIndex));

            editor = new(">>>!<<<");
            Assert.IsFalse(editor.FindFirstWord(out _, out _));
            Assert.IsFalse(editor.FindLastWord(out _, out _));

            editor = new("abcdefghijklmnopqrstuvwxyz");
            Assert.IsTrue(editor.MatchesEndingAt(25, "xyz"));
            Assert.IsFalse(editor.MatchesEndingAt(25, "XYZ"));
            Assert.IsTrue(editor.MatchesEndingAt(25, "XYZ", true));
            Assert.IsFalse(editor.MatchesEndingAt(25, "xyz#"));

            Assert.IsTrue(editor.MatchesEndingAt(2, "abc"));
            Assert.IsFalse(editor.MatchesEndingAt(2, "ABC"));
            Assert.IsTrue(editor.MatchesEndingAt(2, "ABC", true));
            Assert.IsFalse(editor.MatchesEndingAt(23, "abcd"));
        }




        [TestMethod]
        public void TestEdit()
        {
            StringEditor editor = new("abc");

            editor[0] = char.ToUpper(editor[0]);
            Assert.AreEqual("Abc", editor);

            editor[^1] = char.ToUpper(editor[^1]);
            Assert.AreEqual("AbC", editor);

            editor[^2] = '-';
            Assert.AreEqual("A-C", editor);

            Assert.AreEqual('-', editor[^2]);
        }
    }
}
