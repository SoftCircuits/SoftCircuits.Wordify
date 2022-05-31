using SoftCircuits.Wordify;

namespace WordifyTests
{
    [TestClass]
    public class TestNumbers
    {
        private static readonly List<(long, string)> IntegerData = new()
        {
            (0, "zero"),
            (1, "one"),
            (10, "ten"),
            (100, "one hundred"),
            (1000, "one thousand"),
            (10000, "ten thousand"),
            (100000, "one hundred thousand"),
            (1000000, "one million"),
            (10000000, "ten million"),
            (100000000, "one hundred million"),
            (1000000000, "one billion"),
            (10000000000, "ten billion"),
            (100000000000, "one hundred billion"),
            (1000000000000, "one trillion"),
            (10000000000000, "ten trillion"),
            (100000000000000, "one hundred trillion"),
            (1000000000000000, "one quadrillion"),
            (10000000000000000, "ten quadrillion"),
            (100000000000000000, "one hundred quadrillion"),
            (1000000000000000000, "one quintillion"),
            (12, "twelve"),
            (123, "one hundred twenty-three"),
            (1234, "one thousand, two hundred thirty-four"),
            (12345, "twelve thousand, three hundred forty-five"),
            (123456, "one hundred twenty-three thousand, four hundred fifty-six"),
            (1234567, "one million, two hundred thirty-four thousand, five hundred sixty-seven"),
            (12345678, "twelve million, three hundred forty-five thousand, six hundred seventy-eight"),
            (123456789, "one hundred twenty-three million, four hundred fifty-six thousand, seven hundred eighty-nine"),
            (1234567890, "one billion, two hundred thirty-four million, five hundred sixty-seven thousand, eight hundred ninety"),
            (12345678901, "twelve billion, three hundred forty-five million, six hundred seventy-eight thousand, nine hundred one"),
            (123456789012, "one hundred twenty-three billion, four hundred fifty-six million, seven hundred eighty-nine thousand, twelve"),
            (1234567890123, "one trillion, two hundred thirty-four billion, five hundred sixty-seven million, eight hundred ninety thousand, one hundred twenty-three"),
            (12345678901234, "twelve trillion, three hundred forty-five billion, six hundred seventy-eight million, nine hundred one thousand, two hundred thirty-four"),
            (123456789012345, "one hundred twenty-three trillion, four hundred fifty-six billion, seven hundred eighty-nine million, twelve thousand, three hundred forty-five"),
            (1234567890123456, "one quadrillion, two hundred thirty-four trillion, five hundred sixty-seven billion, eight hundred ninety million, one hundred twenty-three thousand, four hundred fifty-six"),
            (12345678901234567, "twelve quadrillion, three hundred forty-five trillion, six hundred seventy-eight billion, nine hundred one million, two hundred thirty-four thousand, five hundred sixty-seven"),
            (123456789012345678, "one hundred twenty-three quadrillion, four hundred fifty-six trillion, seven hundred eighty-nine billion, twelve million, three hundred forty-five thousand, six hundred seventy-eight"),
            (1234567890123456789, "one quintillion, two hundred thirty-four quadrillion, five hundred sixty-seven trillion, eight hundred ninety billion, one hundred twenty-three million, four hundred fifty-six thousand, seven hundred eighty-nine"),
            (-1, "negative one"),
            (-10, "negative ten"),
            (-100, "negative one hundred"),
            (-1000, "negative one thousand"),
            (-100000000000000000, "negative one hundred quadrillion"),
            (-1000000000000000000, "negative one quintillion"),
            (-12, "negative twelve"),
            (-123, "negative one hundred twenty-three"),
            (-1234, "negative one thousand, two hundred thirty-four"),
            (-12345, "negative twelve thousand, three hundred forty-five"),
            (-123456789012345678, "negative one hundred twenty-three quadrillion, four hundred fifty-six trillion, seven hundred eighty-nine billion, twelve million, three hundred forty-five thousand, six hundred seventy-eight"),
            (-1234567890123456789, "negative one quintillion, two hundred thirty-four quadrillion, five hundred sixty-seven trillion, eight hundred ninety billion, one hundred twenty-three million, four hundred fifty-six thousand, seven hundred eighty-nine"),
        };

        private static readonly List<(decimal, string)> TestData = new()
        {
            (0m, "Zero and 00/100"),
            (1m, "One and 00/100"),
            (10m, "Ten and 00/100"),
            (100m, "One hundred and 00/100"),
            (1000m, "One thousand and 00/100"),
            (10000m, "Ten thousand and 00/100"),
            (100000m, "One hundred thousand and 00/100"),
            (1000000m, "One million and 00/100"),
            (10000000m, "Ten million and 00/100"),
            (100000000m, "One hundred million and 00/100"),
            (1000000000m, "One billion and 00/100"),
            (10000000000m, "Ten billion and 00/100"),
            (100000000000m, "One hundred billion and 00/100"),
            (1000000000000m, "One trillion and 00/100"),
            (10000000000000m, "Ten trillion and 00/100"),
            (100000000000000m, "One hundred trillion and 00/100"),
            (1000000000000000m, "One quadrillion and 00/100"),
            (10000000000000000m, "Ten quadrillion and 00/100"),
            (100000000000000000m, "One hundred quadrillion and 00/100"),
            (1000000000000000000m, "One quintillion and 00/100"),
            (10000000000000000000m, "Ten quintillion and 00/100"),
            (100000000000000000000m, "One hundred quintillion and 00/100"),
            (1000000000000000000000m, "One sextillion and 00/100"),
            (10000000000000000000000m, "Ten sextillion and 00/100"),
            (100000000000000000000000m, "One hundred sextillion and 00/100"),
            (1000000000000000000000000m, "One septillion and 00/100"),
            (10000000000000000000000000m, "Ten septillion and 00/100"),
            (100000000000000000000000000m, "One hundred septillion and 00/100"),
            (1000000000000000000000000000m, "One octillion and 00/100"),
            (10000000000000000000000000000m, "Ten octillion and 00/100"),

            (12m, "Twelve and 00/100"),
            (123m, "One hundred twenty-three and 00/100"),
            (1234m, "One thousand, two hundred thirty-four and 00/100"),
            (12345m, "Twelve thousand, three hundred forty-five and 00/100"),
            (123456m, "One hundred twenty-three thousand, four hundred fifty-six and 00/100"),
            (1234567m, "One million, two hundred thirty-four thousand, five hundred sixty-seven and 00/100"),
            (12345678m, "Twelve million, three hundred forty-five thousand, six hundred seventy-eight and 00/100"),
            (123456789m, "One hundred twenty-three million, four hundred fifty-six thousand, seven hundred eighty-nine and 00/100"),
            (1234567890m, "One billion, two hundred thirty-four million, five hundred sixty-seven thousand, eight hundred ninety and 00/100"),
            (12345678901m, "Twelve billion, three hundred forty-five million, six hundred seventy-eight thousand, nine hundred one and 00/100"),
            (123456789012m, "One hundred twenty-three billion, four hundred fifty-six million, seven hundred eighty-nine thousand, twelve and 00/100"),
            (1234567890123m, "One trillion, two hundred thirty-four billion, five hundred sixty-seven million, eight hundred ninety thousand, one hundred twenty-three and 00/100"),
            (12345678901234m, "Twelve trillion, three hundred forty-five billion, six hundred seventy-eight million, nine hundred one thousand, two hundred thirty-four and 00/100"),
            (123456789012345m, "One hundred twenty-three trillion, four hundred fifty-six billion, seven hundred eighty-nine million, twelve thousand, three hundred forty-five and 00/100"),
            (1234567890123456m, "One quadrillion, two hundred thirty-four trillion, five hundred sixty-seven billion, eight hundred ninety million, one hundred twenty-three thousand, four hundred fifty-six and 00/100"),
            (12345678901234567m, "Twelve quadrillion, three hundred forty-five trillion, six hundred seventy-eight billion, nine hundred one million, two hundred thirty-four thousand, five hundred sixty-seven and 00/100"),
            (123456789012345678m, "One hundred twenty-three quadrillion, four hundred fifty-six trillion, seven hundred eighty-nine billion, twelve million, three hundred forty-five thousand, six hundred seventy-eight and 00/100"),
            (1234567890123456789m, "One quintillion, two hundred thirty-four quadrillion, five hundred sixty-seven trillion, eight hundred ninety billion, one hundred twenty-three million, four hundred fifty-six thousand, seven hundred eighty-nine and 00/100"),
            (12345678901234567890m, "Twelve quintillion, three hundred forty-five quadrillion, six hundred seventy-eight trillion, nine hundred one billion, two hundred thirty-four million, five hundred sixty-seven thousand, eight hundred ninety and 00/100"),
            (123456789012345678901m, "One hundred twenty-three quintillion, four hundred fifty-six quadrillion, seven hundred eighty-nine trillion, twelve billion, three hundred forty-five million, six hundred seventy-eight thousand, nine hundred one and 00/100"),
            (1234567890123456789012m, "One sextillion, two hundred thirty-four quintillion, five hundred sixty-seven quadrillion, eight hundred ninety trillion, one hundred twenty-three billion, four hundred fifty-six million, seven hundred eighty-nine thousand, twelve and 00/100"),
            (12345678901234567890123m, "Twelve sextillion, three hundred forty-five quintillion, six hundred seventy-eight quadrillion, nine hundred one trillion, two hundred thirty-four billion, five hundred sixty-seven million, eight hundred ninety thousand, one hundred twenty-three and 00/100"),
            (123456789012345678901234m, "One hundred twenty-three sextillion, four hundred fifty-six quintillion, seven hundred eighty-nine quadrillion, twelve trillion, three hundred forty-five billion, six hundred seventy-eight million, nine hundred one thousand, two hundred thirty-four and 00/100"),
            (1234567890123456789012345m, "One septillion, two hundred thirty-four sextillion, five hundred sixty-seven quintillion, eight hundred ninety quadrillion, one hundred twenty-three trillion, four hundred fifty-six billion, seven hundred eighty-nine million, twelve thousand, three hundred forty-five and 00/100"),
            (12345678901234567890123456m, "Twelve septillion, three hundred forty-five sextillion, six hundred seventy-eight quintillion, nine hundred one quadrillion, two hundred thirty-four trillion, five hundred sixty-seven billion, eight hundred ninety million, one hundred twenty-three thousand, four hundred fifty-six and 00/100"),
            (123456789012345678901234567m, "One hundred twenty-three septillion, four hundred fifty-six sextillion, seven hundred eighty-nine quintillion, twelve quadrillion, three hundred forty-five trillion, six hundred seventy-eight billion, nine hundred one million, two hundred thirty-four thousand, five hundred sixty-seven and 00/100"),
            (1234567890123456789012345678m, "One octillion, two hundred thirty-four septillion, five hundred sixty-seven sextillion, eight hundred ninety quintillion, one hundred twenty-three quadrillion, four hundred fifty-six trillion, seven hundred eighty-nine billion, twelve million, three hundred forty-five thousand, six hundred seventy-eight and 00/100"),
            (12345678901234567890123456789m, "Twelve octillion, three hundred forty-five septillion, six hundred seventy-eight sextillion, nine hundred one quintillion, two hundred thirty-four quadrillion, five hundred sixty-seven trillion, eight hundred ninety billion, one hundred twenty-three million, four hundred fifty-six thousand, seven hundred eighty-nine and 00/100"),

            (1.1m, "One and 10/100"),
            (12.12m, "Twelve and 12/100"),
            (123.123m, "One hundred twenty-three and 12/100"),
            (1234.1234m, "One thousand, two hundred thirty-four and 12/100"),
            (12345.12345m, "Twelve thousand, three hundred forty-five and 12/100"),
            (123456.123456m, "One hundred twenty-three thousand, four hundred fifty-six and 12/100"),
            (1234567.1234567m, "One million, two hundred thirty-four thousand, five hundred sixty-seven and 12/100"),
            (12345678.12345678m, "Twelve million, three hundred forty-five thousand, six hundred seventy-eight and 12/100"),
            (123456789.123456789m, "One hundred twenty-three million, four hundred fifty-six thousand, seven hundred eighty-nine and 12/100"),
            (1234567890.1234567890m, "One billion, two hundred thirty-four million, five hundred sixty-seven thousand, eight hundred ninety and 12/100"),
            (12345678901.12345678901m, "Twelve billion, three hundred forty-five million, six hundred seventy-eight thousand, nine hundred one and 12/100"),
            (123456789012.123456789012m, "One hundred twenty-three billion, four hundred fifty-six million, seven hundred eighty-nine thousand, twelve and 12/100"),
            (1234567890123.1234567890123m, "One trillion, two hundred thirty-four billion, five hundred sixty-seven million, eight hundred ninety thousand, one hundred twenty-three and 12/100"),
            (12345678901234.12345678901234m, "Twelve trillion, three hundred forty-five billion, six hundred seventy-eight million, nine hundred one thousand, two hundred thirty-four and 12/100"),
            (123456789012345.123456789012345m, "One hundred twenty-three trillion, four hundred fifty-six billion, seven hundred eighty-nine million, twelve thousand, three hundred forty-five and 12/100"),
            (1234567890123456.1234567890123456m, "One quadrillion, two hundred thirty-four trillion, five hundred sixty-seven billion, eight hundred ninety million, one hundred twenty-three thousand, four hundred fifty-six and 12/100"),
            (12345678901234567.12345678901234567m, "Twelve quadrillion, three hundred forty-five trillion, six hundred seventy-eight billion, nine hundred one million, two hundred thirty-four thousand, five hundred sixty-seven and 12/100"),
            (123456789012345678.123456789012345678m, "One hundred twenty-three quadrillion, four hundred fifty-six trillion, seven hundred eighty-nine billion, twelve million, three hundred forty-five thousand, six hundred seventy-eight and 12/100"),
            (1234567890123456789.1234567890123456789m, "One quintillion, two hundred thirty-four quadrillion, five hundred sixty-seven trillion, eight hundred ninety billion, one hundred twenty-three million, four hundred fifty-six thousand, seven hundred eighty-nine and 12/100"),
            (12345678901234567890.12345678901234567890m, "Twelve quintillion, three hundred forty-five quadrillion, six hundred seventy-eight trillion, nine hundred one billion, two hundred thirty-four million, five hundred sixty-seven thousand, eight hundred ninety and 12/100"),
            (123456789012345678901.123456789012345678901m, "One hundred twenty-three quintillion, four hundred fifty-six quadrillion, seven hundred eighty-nine trillion, twelve billion, three hundred forty-five million, six hundred seventy-eight thousand, nine hundred one and 12/100"),
            (1234567890123456789012.1234567890123456789012m, "One sextillion, two hundred thirty-four quintillion, five hundred sixty-seven quadrillion, eight hundred ninety trillion, one hundred twenty-three billion, four hundred fifty-six million, seven hundred eighty-nine thousand, twelve and 12/100"),
            (12345678901234567890123.12345678901234567890123m, "Twelve sextillion, three hundred forty-five quintillion, six hundred seventy-eight quadrillion, nine hundred one trillion, two hundred thirty-four billion, five hundred sixty-seven million, eight hundred ninety thousand, one hundred twenty-three and 12/100"),
            (123456789012345678901234.123456789012345678901234m, "One hundred twenty-three sextillion, four hundred fifty-six quintillion, seven hundred eighty-nine quadrillion, twelve trillion, three hundred forty-five billion, six hundred seventy-eight million, nine hundred one thousand, two hundred thirty-four and 12/100"),
            (1234567890123456789012345.1234567890123456789012345m, "One septillion, two hundred thirty-four sextillion, five hundred sixty-seven quintillion, eight hundred ninety quadrillion, one hundred twenty-three trillion, four hundred fifty-six billion, seven hundred eighty-nine million, twelve thousand, three hundred forty-five and 12/100"),
            (12345678901234567890123456.12345678901234567890123456m, "Twelve septillion, three hundred forty-five sextillion, six hundred seventy-eight quintillion, nine hundred one quadrillion, two hundred thirty-four trillion, five hundred sixty-seven billion, eight hundred ninety million, one hundred twenty-three thousand, four hundred fifty-six and 12/100"),
            (123456789012345678901234567.123456789012345678901234567m, "One hundred twenty-three septillion, four hundred fifty-six sextillion, seven hundred eighty-nine quintillion, twelve quadrillion, three hundred forty-five trillion, six hundred seventy-eight billion, nine hundred one million, two hundred thirty-four thousand, five hundred sixty-seven and 12/100"),
            (1234567890123456789012345678.1234567890123456789012345678m, "One octillion, two hundred thirty-four septillion, five hundred sixty-seven sextillion, eight hundred ninety quintillion, one hundred twenty-three quadrillion, four hundred fifty-six trillion, seven hundred eighty-nine billion, twelve million, three hundred forty-five thousand, six hundred seventy-eight and 10/100"),
            (12345678901234567890123456789.12345678901234567890123456789m, "Twelve octillion, three hundred forty-five septillion, six hundred seventy-eight sextillion, nine hundred one quintillion, two hundred thirty-four quadrillion, five hundred sixty-seven trillion, eight hundred ninety billion, one hundred twenty-three million, four hundred fifty-six thousand, seven hundred eighty-nine and 00/100"),

            (1.12m, "One and 12/100"),
            (1.123456789m, "One and 12/100"),
            (1.126m, "One and 13/100"),

            (456m, "Four hundred fifty-six and 00/100"),
            (-456m, "Four hundred fifty-six and 00/100"),

            (decimal.MaxValue, "Seventy-nine octillion, two hundred twenty-eight septillion, one hundred sixty-two sextillion, five hundred fourteen quintillion, two hundred sixty-four quadrillion, three hundred thirty-seven trillion, five hundred ninety-three billion, five hundred forty-three million, nine hundred fifty thousand, three hundred thirty-five and 00/100"),
            (decimal.MinValue, "Seventy-nine octillion, two hundred twenty-eight septillion, one hundred sixty-two sextillion, five hundred fourteen quintillion, two hundred sixty-four quadrillion, three hundred thirty-seven trillion, five hundred ninety-three billion, five hundred forty-three million, nine hundred fifty thousand, three hundred thirty-five and 00/100"),
        };

        [TestMethod]
        public void RunTestData()
        {
            foreach ((long input, string output) in IntegerData)
                Assert.AreEqual(output, Wordify.Transform(input));

            Assert.AreEqual("one hundred twenty-three", Wordify.Transform(123.4, FractionFormat.Round));

            Assert.AreEqual("one hundred twenty-three", Wordify.Transform(123.4m, FractionFormat.Round));
            Assert.AreEqual("one hundred twenty-three", Wordify.Transform(123.4m, FractionFormat.Truncate));
            Assert.AreEqual("one hundred twenty-three and .4", Wordify.Transform(123.4m, FractionFormat.Decimal));
            Assert.AreEqual("one hundred twenty-three and 2/5", Wordify.Transform(123.4m, FractionFormat.Fraction));
            //Assert.AreEqual("one hundred twenty-three and two fifths", Stringify.Transform(123.4m, FractionFormat.Words));
            Assert.AreEqual("one hundred twenty-three and 40/100", Wordify.Transform(123.4m, FractionFormat.Check));

            //NumberToText converter = new NumberToText();
            //foreach ((decimal input, string output) in TestData)
            //    Assert.AreEqual(output, converter.Transform(input));



            decimal d = .1m;
            Assert.AreEqual(null, Wordify.FormatFraction(ref d, FractionFormat.Round));
            d = .1m;
            Assert.AreEqual(null, Wordify.FormatFraction(ref d, FractionFormat.Truncate));
            d = .1m;
            Assert.AreEqual("one tenth", Wordify.FormatFraction(ref d, FractionFormat.Words));
            d = .1m;
            Assert.AreEqual(".1", Wordify.FormatFraction(ref d, FractionFormat.Decimal));
            d = .1m;
            Assert.AreEqual("1/10", Wordify.FormatFraction(ref d, FractionFormat.Fraction));
            d = .1m;
            Assert.AreEqual("10/100", Wordify.FormatFraction(ref d, FractionFormat.Check));
        }
    }
}
