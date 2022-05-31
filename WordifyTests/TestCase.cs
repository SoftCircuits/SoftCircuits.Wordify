using SoftCircuits.Wordify;

namespace WordifyTests
{
    [TestClass]
    public class TestCase
    {
        [TestMethod]
        public void Test()
        {
            Assert.AreEqual("", Wordify.SetCase(null, CaseType.Upper));
            Assert.AreEqual("", Wordify.SetCase("", CaseType.Upper));
            Assert.AreEqual("ABC", Wordify.SetCase("abc", CaseType.Upper));
            Assert.AreEqual("ABC", Wordify.SetCase("Abc", CaseType.Upper));
            Assert.AreEqual("ABC", "aBC".SetCase(CaseType.Upper));

            Assert.AreEqual("", Wordify.SetCase(null, CaseType.Lower));
            Assert.AreEqual("", Wordify.SetCase("", CaseType.Lower));
            Assert.AreEqual("abc", Wordify.SetCase("ABC", CaseType.Lower));
            Assert.AreEqual("abc", Wordify.SetCase("Abc", CaseType.Lower));
            Assert.AreEqual("abc", Wordify.SetCase("aBC", CaseType.Lower));

            Assert.AreEqual("Abc", Wordify.SetCase("abc", CaseType.CapitalizeFirstCharacter));
            Assert.AreEqual("ABC", Wordify.SetCase("ABC", CaseType.CapitalizeFirstCharacter));
            Assert.AreEqual("Abc", Wordify.SetCase("Abc", CaseType.CapitalizeFirstCharacter));
            Assert.AreEqual("ABC", Wordify.SetCase("aBC", CaseType.CapitalizeFirstCharacter));

            // TODO:
            Assert.AreEqual(" abc", Wordify.SetCase(" abc", CaseType.CapitalizeFirstCharacter));

            Assert.AreEqual("This is a test. This is only a test.", Wordify.SetCase("this is a test. this is only a test.", CaseType.Sentence));
            Assert.AreEqual("What? Me worry? Not very likely!", Wordify.SetCase("what? me worry? not very likely!", CaseType.Sentence));
            Assert.AreEqual("This. Is? A! Test: Of abc.5 def.", Wordify.SetCase("this. is? a! test: of abc.5 def.", CaseType.Sentence));

            Assert.AreEqual("Now is the Time for All Good Men to Come to the Aid of the Party", Wordify.SetCase("now is the time for all good men to come to the aid of the party", CaseType.Title));
        }
    }
}
