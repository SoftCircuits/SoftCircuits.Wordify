// Copyright (c) 2023-2024 Jonathan Wood (www.softcircuits.com)
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
            Assert.AreEqual("", ((string)null!).Wordify());
            Assert.AreEqual("", "".Wordify());

            Assert.AreEqual("abc", "abc".Wordify());
            Assert.AreEqual("abc def", "abc def".Wordify());
            Assert.AreEqual("abc Def", "abcDef".Wordify());
            Assert.AreEqual("abc def", "abc-def".Wordify());
            Assert.AreEqual("abc def", "abc_def".Wordify());
            Assert.AreEqual(" abc def ", " abc def ".Wordify());
            Assert.AreEqual(" abc Def ", " abcDef ".Wordify());
            Assert.AreEqual(" abc def ", " abc-def ".Wordify());
            Assert.AreEqual(" abc def ", " abc_def ".Wordify());

            Assert.AreEqual("This Is A Test", "ThisIsATest".Wordify());
            Assert.AreEqual("This Is A Test", "This-Is-A-Test".Wordify());
            Assert.AreEqual("This Is A Test", "This_Is_A_Test".Wordify());
            Assert.AreEqual(" This Is A Test ", " ThisIsATest ".Wordify());
            Assert.AreEqual(" This Is A Test ", " This-Is-A-Test ".Wordify());
            Assert.AreEqual(" This Is A Test ", " This_Is_A_Test ".Wordify());

            Assert.AreEqual("This Is A Test", "ThisIsATest".Wordify(WordifyOption.CamelCase));
            Assert.AreEqual("This Is A Test", "This-Is-A-Test".Wordify(WordifyOption.ReplaceHyphens));
            Assert.AreEqual("This Is A Test", "This_Is_A_Test".Wordify(WordifyOption.ReplaceUnderscores));
            Assert.AreEqual(" This Is A Test ", " ThisIsATest ".Wordify(WordifyOption.CamelCase));
            Assert.AreEqual(" This Is A Test ", " This-Is-A-Test ".Wordify(WordifyOption.ReplaceHyphens));
            Assert.AreEqual(" This Is A Test ", " This_Is_A_Test ".Wordify(WordifyOption.ReplaceUnderscores));
        }

        [TestMethod]
        public void TestCamelCase()
        {
            Assert.AreEqual("abc", "abc".InsertCamelCaseSpaces());
            Assert.AreEqual("Abc", "Abc".InsertCamelCaseSpaces());

            Assert.AreEqual("this Is A Test", "thisIsATest".InsertCamelCaseSpaces());
            Assert.AreEqual("This Is A Test", "ThisIsATest".InsertCamelCaseSpaces());

            Assert.AreEqual("this Is HTML", "thisIsHTML".InsertCamelCaseSpaces());
            Assert.AreEqual("This Is HTML", "ThisIsHTML".InsertCamelCaseSpaces());

            Assert.AreEqual("this is a test", "this is a test".InsertCamelCaseSpaces());
            Assert.AreEqual("This Is A Test", "This Is A Test".InsertCamelCaseSpaces());

            Assert.AreEqual("I Bought An IBMXT", "IBoughtAnIBMXT".InsertCamelCaseSpaces());   // No way to detect "IBM XT"
            Assert.AreEqual("I Bought An Ibm XT", "IBoughtAnIbmXT".InsertCamelCaseSpaces());
            Assert.AreEqual("I Bought An Ibm Xt", "IBoughtAnIbmXt".InsertCamelCaseSpaces());

            Assert.AreEqual("The HTTP Protocol", "TheHTTPProtocol".InsertCamelCaseSpaces());
            Assert.AreEqual("HTTP Protocol", "HTTPProtocol".InsertCamelCaseSpaces());
        }

        [TestMethod]
        public void TestCountWords()
        {
            Assert.AreEqual(0, string.Empty.CountWords());
            Assert.AreEqual(0, "   ".CountWords());
            Assert.AreEqual(1, "   abc   ".CountWords());
            Assert.AreEqual(4, "   This is  a  test   ".CountWords());
            Assert.AreEqual(14, "This is a test of the Emergency Broadcast System. This is only a test.".CountWords());
            Assert.AreEqual(6, "It's the 44.7 plus another 22.456%!".CountWords());
            Assert.AreEqual(5, "10-11 44 inch. 32.77 19!".CountWords());
            Assert.AreEqual(1, "  abc.def ".CountWords());
        }

        [TestMethod]
        public void TestNormalizeWhiteSpace()
        {
            Assert.AreEqual(string.Empty, string.Empty.NormalizeWhiteSpace());
            Assert.AreEqual(string.Empty, "   ".NormalizeWhiteSpace());
            Assert.AreEqual("a", "   a   ".NormalizeWhiteSpace());
            Assert.AreEqual("a b c", "    a   b   c   ".NormalizeWhiteSpace());
            Assert.AreEqual("This is a test!", "  This\r\n is  a\t\t\ttest!   ".NormalizeWhiteSpace());
        }

        [TestMethod]
        public void TestNullAndEmpty()
        {
            Assert.AreEqual("abc", "abc".EmptyIfNull());
            Assert.AreEqual(string.Empty, ((string)null!).EmptyIfNull());
            Assert.AreEqual(string.Empty, string.Empty.EmptyIfNull());

            Assert.AreEqual("abc", "abc".NullIfEmpty());
            Assert.AreEqual(null, ((string)null!).NullIfEmpty());
            Assert.AreEqual(null, string.Empty.NullIfEmpty());

            Assert.AreEqual("abc", "abc".EmptyIfNullOrWhiteSpace());
            Assert.AreEqual(string.Empty, ((string)null!).EmptyIfNullOrWhiteSpace());
            Assert.AreEqual(string.Empty, string.Empty.EmptyIfNullOrWhiteSpace());
            Assert.AreEqual(string.Empty, "   ".EmptyIfNullOrWhiteSpace());

            Assert.AreEqual("abc", "abc".NullIfEmptyOrWhiteSpace());
            Assert.AreEqual(null, ((string)null!).NullIfEmptyOrWhiteSpace());
            Assert.AreEqual(null, string.Empty.NullIfEmptyOrWhiteSpace());
            Assert.AreEqual(null, "   ".NullIfEmptyOrWhiteSpace());
        }
    }
}
