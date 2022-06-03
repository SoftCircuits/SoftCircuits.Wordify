using SoftCircuits.Wordify;

namespace WordifyTests
{
    [TestClass]
    public class TestCase
    {
        [TestMethod]
        public void Test()
        {
            Assert.AreEqual("", Wordify.SetCase(null, CaseOption.Upper));
            Assert.AreEqual("", Wordify.SetCase("", CaseOption.Upper));
            Assert.AreEqual("ABC", Wordify.SetCase("abc", CaseOption.Upper));
            Assert.AreEqual("ABC", Wordify.SetCase("Abc", CaseOption.Upper));
            Assert.AreEqual("ABC", "aBC".SetCase(CaseOption.Upper));

            Assert.AreEqual("", Wordify.SetCase(null, CaseOption.Lower));
            Assert.AreEqual("", Wordify.SetCase("", CaseOption.Lower));
            Assert.AreEqual("abc", Wordify.SetCase("ABC", CaseOption.Lower));
            Assert.AreEqual("abc", Wordify.SetCase("Abc", CaseOption.Lower));
            Assert.AreEqual("abc", Wordify.SetCase("aBC", CaseOption.Lower));

            Assert.AreEqual("Abc", Wordify.SetCase("abc", CaseOption.CapitalizeFirstLetter));
            Assert.AreEqual("ABC", Wordify.SetCase("ABC", CaseOption.CapitalizeFirstLetter));
            Assert.AreEqual("Abc", Wordify.SetCase("Abc", CaseOption.CapitalizeFirstLetter));
            Assert.AreEqual("ABC", Wordify.SetCase("aBC", CaseOption.CapitalizeFirstLetter));

            Assert.AreEqual("  Abc  ", Wordify.SetCase("  Abc  ", CaseOption.CapitalizeFirstLetter));

            Assert.AreEqual("This is a test. This is only a test.", Wordify.SetCase("this is a test. this is only a test.", CaseOption.Sentence));
            Assert.AreEqual("What? Me worry? Not very likely!", Wordify.SetCase("what? me worry? not very likely!", CaseOption.Sentence));
            Assert.AreEqual("This. Is? A! Test: Of abc.5 def.", Wordify.SetCase("this. is? a! test: of abc.5 def.", CaseOption.Sentence));

            Assert.AreEqual("Now is the Time for All Good Men to Come to the Aid of the Party", Wordify.SetCase("now is the time for all good men to come to the aid of the party", CaseOption.Title));
        }
    }
}
