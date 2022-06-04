// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
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
            Assert.AreEqual("", Wordify.SetCase(null, CaseOption.Upper));
            Assert.AreEqual("", Wordify.SetCase("", CaseOption.Upper));
            Assert.AreEqual("ABC", Wordify.SetCase("abc", CaseOption.Upper));
            Assert.AreEqual("ABC", Wordify.SetCase("ABC", CaseOption.Upper));
            Assert.AreEqual("ABC", Wordify.SetCase("Abc", CaseOption.Upper));
            Assert.AreEqual("ABC", Wordify.SetCase("aBC", CaseOption.Upper));
        }

        [TestMethod]
        public void TestLower()
        {
            Assert.AreEqual("", Wordify.SetCase(null, CaseOption.Lower));
            Assert.AreEqual("", Wordify.SetCase("", CaseOption.Lower));
            Assert.AreEqual("abc", Wordify.SetCase("abc", CaseOption.Lower));
            Assert.AreEqual("abc", Wordify.SetCase("ABC", CaseOption.Lower));
            Assert.AreEqual("abc", Wordify.SetCase("Abc", CaseOption.Lower));
            Assert.AreEqual("abc", Wordify.SetCase("aBC", CaseOption.Lower));
        }

        [TestMethod]
        public void TestCapitalizeFirstLetter()
        {
            Assert.AreEqual("", Wordify.SetCase(null, CaseOption.CapitalizeFirstLetter));
            Assert.AreEqual("", Wordify.SetCase("", CaseOption.CapitalizeFirstLetter));
            Assert.AreEqual("Abc", Wordify.SetCase("abc", CaseOption.CapitalizeFirstLetter));
            Assert.AreEqual("ABC", Wordify.SetCase("ABC", CaseOption.CapitalizeFirstLetter));
            Assert.AreEqual("Abc", Wordify.SetCase("Abc", CaseOption.CapitalizeFirstLetter));
            Assert.AreEqual("ABC", Wordify.SetCase("aBC", CaseOption.CapitalizeFirstLetter));

            Assert.AreEqual(" This is a test! ", Wordify.SetCase(" this is a test! ", CaseOption.CapitalizeFirstLetter));
            Assert.AreEqual("  Abc  ", Wordify.SetCase("  Abc  ", CaseOption.CapitalizeFirstLetter));
            Assert.AreEqual(" !@#$%Abc&* ", Wordify.SetCase(" !@#$%abc&* ", CaseOption.CapitalizeFirstLetter));
            Assert.AreEqual("123456789Z123456789", Wordify.SetCase("123456789z123456789", CaseOption.CapitalizeFirstLetter));
            Assert.AreEqual("123", Wordify.SetCase("123", CaseOption.CapitalizeFirstLetter));
        }

        [TestMethod]
        public void TestSentence()
        {
            Assert.AreEqual("", Wordify.SetCase(null, CaseOption.Sentence));
            Assert.AreEqual("", Wordify.SetCase("", CaseOption.Sentence));
            Assert.AreEqual("Abc", Wordify.SetCase("abc", CaseOption.Sentence));
            Assert.AreEqual("This is a test. This is only a test.", Wordify.SetCase("this is a test. this is only a test.", CaseOption.Sentence));
            Assert.AreEqual("What? Me worry? Not very likely!", Wordify.SetCase("what? me worry? not very likely!", CaseOption.Sentence));
            Assert.AreEqual(" This . Is ? A ! Test: Of abc.5 def. ", Wordify.SetCase(" this . is ? a ! test: of abc.5 def. ", CaseOption.Sentence));
            Assert.AreEqual(" @@@@Abc@@@@ ", Wordify.SetCase(" @@@@abc@@@@ ", CaseOption.Sentence));
            Assert.AreEqual(" This is another test. ", Wordify.SetCase(" this is another test. ", CaseOption.Sentence));
            Assert.AreEqual("123", Wordify.SetCase("123", CaseOption.Sentence));
        }

        [TestMethod]
        public void TestTitle()
        {
            Assert.AreEqual("", Wordify.SetCase(null, CaseOption.Title));
            Assert.AreEqual("", Wordify.SetCase("", CaseOption.Title));
            Assert.AreEqual(" The End ", Wordify.SetCase(" the end ", CaseOption.Title));
            Assert.AreEqual(" Cat in the Hat ", Wordify.SetCase(" cat in the hat ", CaseOption.Title));
            Assert.AreEqual(" Cat in the Hat . What Do You Think about that ? ", Wordify.SetCase(" cat in the hat . what do you think about that ? ", CaseOption.Title));
            Assert.AreEqual("Now is the Time for All Good Men to Come to the Aid of the Party", Wordify.SetCase("now is the time for all good men to come to the aid of the party", CaseOption.Title));
        }
    }
}
