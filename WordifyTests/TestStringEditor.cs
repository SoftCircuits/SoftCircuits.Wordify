// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using SoftCircuits.Wordify.Helpers;

namespace WordifyTests
{
    /// <summary>
    /// Tests for private <see cref="StringEditor"/> utility class.
    /// </summary>
    [TestClass]
    public class TestStringEditor
    {
        [TestMethod]
        public void TestGeneral()
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
        }

        [TestMethod]
        public void TestReplace()
        {
            StringEditor editor = new("abc");

            editor.Replace(1000, "xyz", 1);
            Assert.AreEqual("abcxyz", editor);

            editor.Replace(0, "123", 2);
            Assert.AreEqual("123cxyz", editor);

            editor.Replace(4, "@#$", 0);
            Assert.AreEqual("123c@#$xyz", editor);

            editor.Replace(7, "abcdefghijklmnopqrstuvwxyz", 1000);
            Assert.AreEqual("123c@#$abcdefghijklmnopqrstuvwxyz", editor);

            editor.Replace(0, "abc", 1000);
            Assert.AreEqual("abc", editor);

            editor = (StringEditor)"abcdefghijklmnopqrstuvwxyz";

            editor.Replace(2, "123", 5);
            Assert.AreEqual("ab123hijklmnopqrstuvwxyz", editor);

            editor.Replace(7, "...", 8);
            Assert.AreEqual("ab123hi...rstuvwxyz", editor);

            editor.Replace(7, "", 8);
            Assert.AreEqual("ab123hiwxyz", editor);
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
        public void TestMove()
        {
            StringEditor editor = new("abcdefghijklmnopqrstuvwxyz");

            editor.Move(0, 8, 4);
            Assert.AreEqual("abcdefghabcdmnopqrstuvwxyz", editor);

            editor.Move(8, 0, 4);
            Assert.AreEqual("abcdefghabcdmnopqrstuvwxyz", editor);

            editor.Move(25, 2, 7);
            Assert.AreEqual("abzdefghabcdmnopqrstuvwxyz", editor);

            editor.Move(3, 25, 7);
            Assert.AreEqual("abzdefghabcdmnopqrstuvwxyd", editor);
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
    }
}
