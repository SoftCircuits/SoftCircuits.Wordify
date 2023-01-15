// Copyright (c) 2023 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using SoftCircuits.Wordify;

namespace WordifyTests
{
    [TestClass]
    public class TestCase
    {
        [TestMethod]
        public void TestUpper()
        {
            Assert.AreEqual("", ((string)null!).SetCase(CaseOption.Upper));
            Assert.AreEqual("", "".SetCase(CaseOption.Upper));
            Assert.AreEqual("ABC", "abc".SetCase(CaseOption.Upper));
            Assert.AreEqual("ABC", "ABC".SetCase(CaseOption.Upper));
            Assert.AreEqual("ABC", "Abc".SetCase(CaseOption.Upper));
            Assert.AreEqual("ABC", "aBC".SetCase(CaseOption.Upper));
        }

        [TestMethod]
        public void TestLower()
        {
            Assert.AreEqual("", ((string)null!).SetCase(CaseOption.Lower));
            Assert.AreEqual("", "".SetCase(CaseOption.Lower));
            Assert.AreEqual("abc", "abc".SetCase(CaseOption.Lower));
            Assert.AreEqual("abc", "ABC".SetCase(CaseOption.Lower));
            Assert.AreEqual("abc", "Abc".SetCase(CaseOption.Lower));
            Assert.AreEqual("abc", "aBC".SetCase(CaseOption.Lower));
        }

        [TestMethod]
        public void TestCapitalizeFirstLetter()
        {
            Assert.AreEqual("", ((string)null!).SetCase(CaseOption.Capitalize));
            Assert.AreEqual("", string.Empty.SetCase(CaseOption.Capitalize));
            Assert.AreEqual("Abc", "abc".SetCase(CaseOption.Capitalize));
            Assert.AreEqual("ABC", "ABC".SetCase(CaseOption.Capitalize));
            Assert.AreEqual("Abc", "Abc".SetCase(CaseOption.Capitalize));
            Assert.AreEqual("ABC", "aBC".SetCase(CaseOption.Capitalize));

            Assert.AreEqual(" This is a test! ", " this is a test! ".SetCase(CaseOption.Capitalize));
            Assert.AreEqual("  Abc  ", "  Abc  ".SetCase(CaseOption.Capitalize));
            Assert.AreEqual(" !@#$%Abc&* ", " !@#$%abc&* ".SetCase(CaseOption.Capitalize));
            Assert.AreEqual("123456789Z123456789", "123456789z123456789".SetCase(CaseOption.Capitalize));
            Assert.AreEqual("123", "123".SetCase(CaseOption.Capitalize));
        }

        [TestMethod]
        public void TestSentence()
        {
            Assert.AreEqual("", ((string)null!).SetCase(CaseOption.Sentence));
            Assert.AreEqual("", "".SetCase(CaseOption.Sentence));
            Assert.AreEqual("Abc", "abc".SetCase(CaseOption.Sentence));
            Assert.AreEqual("This is a test. This is only a test.", "this is a test. this is only a test.".SetCase(CaseOption.Sentence));
            Assert.AreEqual("What? Me worry? Not very likely!", "what? me worry? not very likely!".SetCase(CaseOption.Sentence));
            Assert.AreEqual(" This . Is ? A ! Test: Of abc.5 def. ", " this . is ? a ! test: of abc.5 def. ".SetCase(CaseOption.Sentence));
            Assert.AreEqual(" @@@@Abc@@@@ ", " @@@@abc@@@@ ".SetCase(CaseOption.Sentence));
            Assert.AreEqual(" This is another test. ", " this is another test. ".SetCase(CaseOption.Sentence));
            Assert.AreEqual("123", "123".SetCase(CaseOption.Sentence));
        }

        [TestMethod]
        public void TestTitle()
        {
            Assert.AreEqual("", ((string)null!).SetCase(CaseOption.Title));
            Assert.AreEqual("", string.Empty.SetCase(CaseOption.Title));
            Assert.AreEqual(" The End ", " the end ".SetCase(CaseOption.Title));
            Assert.AreEqual(" Cat in the Hat ", " cat in the hat ".SetCase(CaseOption.Title));
            Assert.AreEqual(" Cat in the Hat . What Do You Think about that ? ", " cat in the hat . what do you think about that ? ".SetCase(CaseOption.Title));
            Assert.AreEqual("Now is the Time for All Good Men to Come to the Aid of the Party", "now is the time for all good men to come to the aid of the party".SetCase(CaseOption.Title));
        }
    }
}
