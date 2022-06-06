// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using SoftCircuits.Wordify;

namespace WordifyTests
{
    [TestClass]
    public class Test
    {
        [TestMethod]
        public void TestTransform()
        {
            Assert.AreEqual("", Wordify.Transform(null));
            Assert.AreEqual("", Wordify.Transform(""));

            Assert.AreEqual("abc", Wordify.Transform("abc"));
            Assert.AreEqual("abc def", Wordify.Transform("abc def"));
            Assert.AreEqual("abc Def", Wordify.Transform("abcDef"));
            Assert.AreEqual("abc def", Wordify.Transform("abc-def"));
            Assert.AreEqual("abc def", Wordify.Transform("abc_def"));
            Assert.AreEqual(" abc def ", Wordify.Transform(" abc def "));
            Assert.AreEqual(" abc Def ", Wordify.Transform(" abcDef "));
            Assert.AreEqual(" abc def ", Wordify.Transform(" abc-def "));
            Assert.AreEqual(" abc def ", Wordify.Transform(" abc_def "));

            Assert.AreEqual("This Is A Test", Wordify.Transform("ThisIsATest"));
            Assert.AreEqual("This Is A Test", Wordify.Transform("This-Is-A-Test"));
            Assert.AreEqual("This Is A Test", Wordify.Transform("This_Is_A_Test"));
            Assert.AreEqual(" This Is A Test ", Wordify.Transform(" ThisIsATest "));
            Assert.AreEqual(" This Is A Test ", Wordify.Transform(" This-Is-A-Test "));
            Assert.AreEqual(" This Is A Test ", Wordify.Transform(" This_Is_A_Test "));

            Assert.AreEqual("This Is A Test", Wordify.Transform("ThisIsATest", TransformOption.CamelCase));
            Assert.AreEqual("This Is A Test", Wordify.Transform("This-Is-A-Test", TransformOption.ReplaceHyphens));
            Assert.AreEqual("This Is A Test", Wordify.Transform("This_Is_A_Test", TransformOption.ReplaceUnderscores));
            Assert.AreEqual(" This Is A Test ", Wordify.Transform(" ThisIsATest ", TransformOption.CamelCase));
            Assert.AreEqual(" This Is A Test ", Wordify.Transform(" This-Is-A-Test ", TransformOption.ReplaceHyphens));
            Assert.AreEqual(" This Is A Test ", Wordify.Transform(" This_Is_A_Test ", TransformOption.ReplaceUnderscores));
        }

        [TestMethod]
        public void TestCamelCase()
        {
            Assert.AreEqual("abc", Wordify.InsertCamelCaseSpaces("abc"));
            Assert.AreEqual("Abc", Wordify.InsertCamelCaseSpaces("Abc"));

            Assert.AreEqual("this Is A Test", Wordify.InsertCamelCaseSpaces("thisIsATest"));
            Assert.AreEqual("This Is A Test", Wordify.InsertCamelCaseSpaces("ThisIsATest"));

            Assert.AreEqual("this Is HTML", Wordify.InsertCamelCaseSpaces("thisIsHTML"));
            Assert.AreEqual("This Is HTML", Wordify.InsertCamelCaseSpaces("ThisIsHTML"));

            Assert.AreEqual("this is a test", Wordify.InsertCamelCaseSpaces("this is a test"));
            Assert.AreEqual("This Is A Test", Wordify.InsertCamelCaseSpaces("This Is A Test"));

            Assert.AreEqual("I Bought An IBMXT", Wordify.InsertCamelCaseSpaces("IBoughtAnIBMXT"));   // No way to know IBM XT
            Assert.AreEqual("I Bought An Ibm XT", Wordify.InsertCamelCaseSpaces("IBoughtAnIbmXT"));
            Assert.AreEqual("I Bought An Ibm Xt", Wordify.InsertCamelCaseSpaces("IBoughtAnIbmXt"));

            Assert.AreEqual("The HTTP Protocol", Wordify.InsertCamelCaseSpaces("TheHTTPProtocol"));
            Assert.AreEqual("HTTP Protocol", Wordify.InsertCamelCaseSpaces("HTTPProtocol"));
        }

        [TestMethod]
        public void TestCountWords()
        {
            Assert.AreEqual(0, Wordify.CountWords(string.Empty));
            Assert.AreEqual(0, Wordify.CountWords("   "));
            Assert.AreEqual(1, Wordify.CountWords("   abc   "));
            Assert.AreEqual(4, Wordify.CountWords("   This is  a  test   "));
            Assert.AreEqual(14, Wordify.CountWords("This is a test of the Emergency Broadcast System. This is only a test."));
            Assert.AreEqual(6, Wordify.CountWords("It's the 44.7 plus another 22.456%!"));
            Assert.AreEqual(5, Wordify.CountWords("10-11 44 inch. 32.77 19!"));
            Assert.AreEqual(1, Wordify.CountWords("  abc.def "));
        }

        [TestMethod]
        public void TestNormalizeWhiteSpace()
        {
            Assert.AreEqual(string.Empty, Wordify.NormalizeWhiteSpace(string.Empty));
            Assert.AreEqual(string.Empty, Wordify.NormalizeWhiteSpace("   "));
            Assert.AreEqual("a", Wordify.NormalizeWhiteSpace("   a   "));
            Assert.AreEqual("a b c", Wordify.NormalizeWhiteSpace("    a   b   c   "));
            Assert.AreEqual("This is a test!", Wordify.NormalizeWhiteSpace("  This\r\n is  a\t\t\ttest!   "));
        }

        [TestMethod]
        public void TestNullAndEmpty()
        {
            Assert.AreEqual("abc", Wordify.EmptyIfNull("abc"));
            Assert.AreEqual(string.Empty, Wordify.EmptyIfNull(null));
            Assert.AreEqual(string.Empty, Wordify.EmptyIfNull(string.Empty));

            Assert.AreEqual("abc", Wordify.NullIfEmpty("abc"));
            Assert.AreEqual(null, Wordify.NullIfEmpty(null));
            Assert.AreEqual(null, Wordify.NullIfEmpty(string.Empty));

            Assert.AreEqual("abc", Wordify.EmptyIfNullOrWhiteSpace("abc"));
            Assert.AreEqual(string.Empty, Wordify.EmptyIfNullOrWhiteSpace(null));
            Assert.AreEqual(string.Empty, Wordify.EmptyIfNullOrWhiteSpace(string.Empty));
            Assert.AreEqual(string.Empty, Wordify.EmptyIfNullOrWhiteSpace("   "));

            Assert.AreEqual("abc", Wordify.NullIfEmptyOrWhiteSpace("abc"));
            Assert.AreEqual(null, Wordify.NullIfEmptyOrWhiteSpace(null));
            Assert.AreEqual(null, Wordify.NullIfEmptyOrWhiteSpace(string.Empty));
            Assert.AreEqual(null, Wordify.NullIfEmptyOrWhiteSpace("   "));
        }
    }
}
