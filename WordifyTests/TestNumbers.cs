﻿// Copyright (c) 2023-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using SoftCircuits.Wordify;

namespace WordifyTests
{
    [TestClass]
    public class TestNumbers
    {
        private static readonly List<(long, string)> IntegerData =
        [
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
            (1234, "one thousand two hundred thirty-four"),
            (12345, "twelve thousand three hundred forty-five"),
            (123456, "one hundred twenty-three thousand four hundred fifty-six"),
            (1234567, "one million two hundred thirty-four thousand five hundred sixty-seven"),
            (12345678, "twelve million three hundred forty-five thousand six hundred seventy-eight"),
            (123456789, "one hundred twenty-three million four hundred fifty-six thousand seven hundred eighty-nine"),
            (1234567890, "one billion two hundred thirty-four million five hundred sixty-seven thousand eight hundred ninety"),
            (12345678901, "twelve billion three hundred forty-five million six hundred seventy-eight thousand nine hundred one"),
            (123456789012, "one hundred twenty-three billion four hundred fifty-six million seven hundred eighty-nine thousand twelve"),
            (1234567890123, "one trillion two hundred thirty-four billion five hundred sixty-seven million eight hundred ninety thousand one hundred twenty-three"),
            (12345678901234, "twelve trillion three hundred forty-five billion six hundred seventy-eight million nine hundred one thousand two hundred thirty-four"),
            (123456789012345, "one hundred twenty-three trillion four hundred fifty-six billion seven hundred eighty-nine million twelve thousand three hundred forty-five"),
            (1234567890123456, "one quadrillion two hundred thirty-four trillion five hundred sixty-seven billion eight hundred ninety million one hundred twenty-three thousand four hundred fifty-six"),
            (12345678901234567, "twelve quadrillion three hundred forty-five trillion six hundred seventy-eight billion nine hundred one million two hundred thirty-four thousand five hundred sixty-seven"),
            (123456789012345678, "one hundred twenty-three quadrillion four hundred fifty-six trillion seven hundred eighty-nine billion twelve million three hundred forty-five thousand six hundred seventy-eight"),
            (1234567890123456789, "one quintillion two hundred thirty-four quadrillion five hundred sixty-seven trillion eight hundred ninety billion one hundred twenty-three million four hundred fifty-six thousand seven hundred eighty-nine"),
            (-1, "negative one"),
            (-10, "negative ten"),
            (-100, "negative one hundred"),
            (-1000, "negative one thousand"),
            (-100000000000000000, "negative one hundred quadrillion"),
            (-1000000000000000000, "negative one quintillion"),
            (-12, "negative twelve"),
            (-123, "negative one hundred twenty-three"),
            (-1234, "negative one thousand two hundred thirty-four"),
            (-12345, "negative twelve thousand three hundred forty-five"),
            (-123456789012345678, "negative one hundred twenty-three quadrillion four hundred fifty-six trillion seven hundred eighty-nine billion twelve million three hundred forty-five thousand six hundred seventy-eight"),
            (-1234567890123456789, "negative one quintillion two hundred thirty-four quadrillion five hundred sixty-seven trillion eight hundred ninety billion one hundred twenty-three million four hundred fifty-six thousand seven hundred eighty-nine"),
            (long.MinValue, "negative nine quintillion two hundred twenty-three quadrillion three hundred seventy-two trillion thirty-six billion eight hundred fifty-four million seven hundred seventy-five thousand eight hundred eight"),
            (long.MaxValue, "nine quintillion two hundred twenty-three quadrillion three hundred seventy-two trillion thirty-six billion eight hundred fifty-four million seven hundred seventy-five thousand eight hundred seven"),
        ];

        [TestMethod]
        public void TestIntegers()
        {
            foreach ((long input, string output) in IntegerData)
                Assert.AreEqual(output, input.Wordify());
        }

        private static readonly List<(decimal, string)> FloatingPointData =
        [
            (0m, "zero and 00/100"),
            (1m, "one and 00/100"),
            (10m, "ten and 00/100"),
            (100m, "one hundred and 00/100"),
            (1000m, "one thousand and 00/100"),
            (10000m, "ten thousand and 00/100"),
            (100000m, "one hundred thousand and 00/100"),
            (1000000m, "one million and 00/100"),
            (10000000m, "ten million and 00/100"),
            (100000000m, "one hundred million and 00/100"),
            (1000000000m, "one billion and 00/100"),
            (10000000000m, "ten billion and 00/100"),
            (100000000000m, "one hundred billion and 00/100"),
            (1000000000000m, "one trillion and 00/100"),
            (10000000000000m, "ten trillion and 00/100"),
            (100000000000000m, "one hundred trillion and 00/100"),
            (1000000000000000m, "one quadrillion and 00/100"),
            (10000000000000000m, "ten quadrillion and 00/100"),
            (100000000000000000m, "one hundred quadrillion and 00/100"),
            (1000000000000000000m, "one quintillion and 00/100"),
            (10000000000000000000m, "ten quintillion and 00/100"),
            (100000000000000000000m, "one hundred quintillion and 00/100"),
            (1000000000000000000000m, "one sextillion and 00/100"),
            (10000000000000000000000m, "ten sextillion and 00/100"),
            (100000000000000000000000m, "one hundred sextillion and 00/100"),
            (1000000000000000000000000m, "one septillion and 00/100"),
            (10000000000000000000000000m, "ten septillion and 00/100"),
            (100000000000000000000000000m, "one hundred septillion and 00/100"),
            (1000000000000000000000000000m, "one octillion and 00/100"),
            (10000000000000000000000000000m, "ten octillion and 00/100"),

            (12m, "twelve and 00/100"),
            (123m, "one hundred twenty-three and 00/100"),
            (1234m, "one thousand two hundred thirty-four and 00/100"),
            (12345m, "twelve thousand three hundred forty-five and 00/100"),
            (123456m, "one hundred twenty-three thousand four hundred fifty-six and 00/100"),
            (1234567m, "one million two hundred thirty-four thousand five hundred sixty-seven and 00/100"),
            (12345678m, "twelve million three hundred forty-five thousand six hundred seventy-eight and 00/100"),
            (123456789m, "one hundred twenty-three million four hundred fifty-six thousand seven hundred eighty-nine and 00/100"),
            (1234567890m, "one billion two hundred thirty-four million five hundred sixty-seven thousand eight hundred ninety and 00/100"),
            (12345678901m, "twelve billion three hundred forty-five million six hundred seventy-eight thousand nine hundred one and 00/100"),
            (123456789012m, "one hundred twenty-three billion four hundred fifty-six million seven hundred eighty-nine thousand twelve and 00/100"),
            (1234567890123m, "one trillion two hundred thirty-four billion five hundred sixty-seven million eight hundred ninety thousand one hundred twenty-three and 00/100"),
            (12345678901234m, "twelve trillion three hundred forty-five billion six hundred seventy-eight million nine hundred one thousand two hundred thirty-four and 00/100"),
            (123456789012345m, "one hundred twenty-three trillion four hundred fifty-six billion seven hundred eighty-nine million twelve thousand three hundred forty-five and 00/100"),
            (1234567890123456m, "one quadrillion two hundred thirty-four trillion five hundred sixty-seven billion eight hundred ninety million one hundred twenty-three thousand four hundred fifty-six and 00/100"),
            (12345678901234567m, "twelve quadrillion three hundred forty-five trillion six hundred seventy-eight billion nine hundred one million two hundred thirty-four thousand five hundred sixty-seven and 00/100"),
            (123456789012345678m, "one hundred twenty-three quadrillion four hundred fifty-six trillion seven hundred eighty-nine billion twelve million three hundred forty-five thousand six hundred seventy-eight and 00/100"),
            (1234567890123456789m, "one quintillion two hundred thirty-four quadrillion five hundred sixty-seven trillion eight hundred ninety billion one hundred twenty-three million four hundred fifty-six thousand seven hundred eighty-nine and 00/100"),
            (12345678901234567890m, "twelve quintillion three hundred forty-five quadrillion six hundred seventy-eight trillion nine hundred one billion two hundred thirty-four million five hundred sixty-seven thousand eight hundred ninety and 00/100"),
            (123456789012345678901m, "one hundred twenty-three quintillion four hundred fifty-six quadrillion seven hundred eighty-nine trillion twelve billion three hundred forty-five million six hundred seventy-eight thousand nine hundred one and 00/100"),
            (1234567890123456789012m, "one sextillion two hundred thirty-four quintillion five hundred sixty-seven quadrillion eight hundred ninety trillion one hundred twenty-three billion four hundred fifty-six million seven hundred eighty-nine thousand twelve and 00/100"),
            (12345678901234567890123m, "twelve sextillion three hundred forty-five quintillion six hundred seventy-eight quadrillion nine hundred one trillion two hundred thirty-four billion five hundred sixty-seven million eight hundred ninety thousand one hundred twenty-three and 00/100"),
            (123456789012345678901234m, "one hundred twenty-three sextillion four hundred fifty-six quintillion seven hundred eighty-nine quadrillion twelve trillion three hundred forty-five billion six hundred seventy-eight million nine hundred one thousand two hundred thirty-four and 00/100"),
            (1234567890123456789012345m, "one septillion two hundred thirty-four sextillion five hundred sixty-seven quintillion eight hundred ninety quadrillion one hundred twenty-three trillion four hundred fifty-six billion seven hundred eighty-nine million twelve thousand three hundred forty-five and 00/100"),
            (12345678901234567890123456m, "twelve septillion three hundred forty-five sextillion six hundred seventy-eight quintillion nine hundred one quadrillion two hundred thirty-four trillion five hundred sixty-seven billion eight hundred ninety million one hundred twenty-three thousand four hundred fifty-six and 00/100"),
            (123456789012345678901234567m, "one hundred twenty-three septillion four hundred fifty-six sextillion seven hundred eighty-nine quintillion twelve quadrillion three hundred forty-five trillion six hundred seventy-eight billion nine hundred one million two hundred thirty-four thousand five hundred sixty-seven and 00/100"),
            (1234567890123456789012345678m, "one octillion two hundred thirty-four septillion five hundred sixty-seven sextillion eight hundred ninety quintillion one hundred twenty-three quadrillion four hundred fifty-six trillion seven hundred eighty-nine billion twelve million three hundred forty-five thousand six hundred seventy-eight and 00/100"),
            (12345678901234567890123456789m, "twelve octillion three hundred forty-five septillion six hundred seventy-eight sextillion nine hundred one quintillion two hundred thirty-four quadrillion five hundred sixty-seven trillion eight hundred ninety billion one hundred twenty-three million four hundred fifty-six thousand seven hundred eighty-nine and 00/100"),

            (1.1m, "one and 10/100"),
            (12.12m, "twelve and 12/100"),
            (123.123m, "one hundred twenty-three and 12/100"),
            (1234.1234m, "one thousand two hundred thirty-four and 12/100"),
            (12345.12345m, "twelve thousand three hundred forty-five and 12/100"),
            (123456.123456m, "one hundred twenty-three thousand four hundred fifty-six and 12/100"),
            (1234567.1234567m, "one million two hundred thirty-four thousand five hundred sixty-seven and 12/100"),
            (12345678.12345678m, "twelve million three hundred forty-five thousand six hundred seventy-eight and 12/100"),
            (123456789.123456789m, "one hundred twenty-three million four hundred fifty-six thousand seven hundred eighty-nine and 12/100"),
            (1234567890.1234567890m, "one billion two hundred thirty-four million five hundred sixty-seven thousand eight hundred ninety and 12/100"),
            (12345678901.12345678901m, "twelve billion three hundred forty-five million six hundred seventy-eight thousand nine hundred one and 12/100"),
            (123456789012.123456789012m, "one hundred twenty-three billion four hundred fifty-six million seven hundred eighty-nine thousand twelve and 12/100"),
            (1234567890123.1234567890123m, "one trillion two hundred thirty-four billion five hundred sixty-seven million eight hundred ninety thousand one hundred twenty-three and 12/100"),
            (12345678901234.12345678901234m, "twelve trillion three hundred forty-five billion six hundred seventy-eight million nine hundred one thousand two hundred thirty-four and 12/100"),
            (123456789012345.123456789012345m, "one hundred twenty-three trillion four hundred fifty-six billion seven hundred eighty-nine million twelve thousand three hundred forty-five and 12/100"),
            (1234567890123456.1234567890123456m, "one quadrillion two hundred thirty-four trillion five hundred sixty-seven billion eight hundred ninety million one hundred twenty-three thousand four hundred fifty-six and 12/100"),
            (12345678901234567.12345678901234567m, "twelve quadrillion three hundred forty-five trillion six hundred seventy-eight billion nine hundred one million two hundred thirty-four thousand five hundred sixty-seven and 12/100"),
            (123456789012345678.123456789012345678m, "one hundred twenty-three quadrillion four hundred fifty-six trillion seven hundred eighty-nine billion twelve million three hundred forty-five thousand six hundred seventy-eight and 12/100"),
            (1234567890123456789.1234567890123456789m, "one quintillion two hundred thirty-four quadrillion five hundred sixty-seven trillion eight hundred ninety billion one hundred twenty-three million four hundred fifty-six thousand seven hundred eighty-nine and 12/100"),
            (12345678901234567890.12345678901234567890m, "twelve quintillion three hundred forty-five quadrillion six hundred seventy-eight trillion nine hundred one billion two hundred thirty-four million five hundred sixty-seven thousand eight hundred ninety and 12/100"),
            (123456789012345678901.123456789012345678901m, "one hundred twenty-three quintillion four hundred fifty-six quadrillion seven hundred eighty-nine trillion twelve billion three hundred forty-five million six hundred seventy-eight thousand nine hundred one and 12/100"),
            (1234567890123456789012.1234567890123456789012m, "one sextillion two hundred thirty-four quintillion five hundred sixty-seven quadrillion eight hundred ninety trillion one hundred twenty-three billion four hundred fifty-six million seven hundred eighty-nine thousand twelve and 12/100"),
            (12345678901234567890123.12345678901234567890123m, "twelve sextillion three hundred forty-five quintillion six hundred seventy-eight quadrillion nine hundred one trillion two hundred thirty-four billion five hundred sixty-seven million eight hundred ninety thousand one hundred twenty-three and 12/100"),
            (123456789012345678901234.123456789012345678901234m, "one hundred twenty-three sextillion four hundred fifty-six quintillion seven hundred eighty-nine quadrillion twelve trillion three hundred forty-five billion six hundred seventy-eight million nine hundred one thousand two hundred thirty-four and 12/100"),
            (1234567890123456789012345.1234567890123456789012345m, "one septillion two hundred thirty-four sextillion five hundred sixty-seven quintillion eight hundred ninety quadrillion one hundred twenty-three trillion four hundred fifty-six billion seven hundred eighty-nine million twelve thousand three hundred forty-five and 12/100"),
            (12345678901234567890123456.12345678901234567890123456m, "twelve septillion three hundred forty-five sextillion six hundred seventy-eight quintillion nine hundred one quadrillion two hundred thirty-four trillion five hundred sixty-seven billion eight hundred ninety million one hundred twenty-three thousand four hundred fifty-six and 12/100"),
            (123456789012345678901234567.123456789012345678901234567m, "one hundred twenty-three septillion four hundred fifty-six sextillion seven hundred eighty-nine quintillion twelve quadrillion three hundred forty-five trillion six hundred seventy-eight billion nine hundred one million two hundred thirty-four thousand five hundred sixty-seven and 12/100"),
            (1234567890123456789012345678.1234567890123456789012345678m, "one octillion two hundred thirty-four septillion five hundred sixty-seven sextillion eight hundred ninety quintillion one hundred twenty-three quadrillion four hundred fifty-six trillion seven hundred eighty-nine billion twelve million three hundred forty-five thousand six hundred seventy-eight and 10/100"),
            (12345678901234567890123456789.12345678901234567890123456789m, "twelve octillion three hundred forty-five septillion six hundred seventy-eight sextillion nine hundred one quintillion two hundred thirty-four quadrillion five hundred sixty-seven trillion eight hundred ninety billion one hundred twenty-three million four hundred fifty-six thousand seven hundred eighty-nine and 00/100"),

            (1.12m, "one and 12/100"),
            (1.123456789m, "one and 12/100"),
            (1.126m, "one and 13/100"),

            (456m, "four hundred fifty-six and 00/100"),
            (-456m, "negative four hundred fifty-six and 00/100"),

            (decimal.MaxValue, "seventy-nine octillion two hundred twenty-eight septillion one hundred sixty-two sextillion five hundred fourteen quintillion two hundred sixty-four quadrillion three hundred thirty-seven trillion five hundred ninety-three billion five hundred forty-three million nine hundred fifty thousand three hundred thirty-five and 00/100"),
            (decimal.MinValue, "negative seventy-nine octillion two hundred twenty-eight septillion one hundred sixty-two sextillion five hundred fourteen quintillion two hundred sixty-four quadrillion three hundred thirty-seven trillion five hundred ninety-three billion five hundred forty-three million nine hundred fifty thousand three hundred thirty-five and 00/100"),
        ];

        [TestMethod]
        public void TestFloatingPoints()
        {
            foreach ((decimal input, string output) in FloatingPointData)
                Assert.AreEqual(output, input.Wordify(FractionOption.Check));
        }

        private class FractionResult(decimal value, string result)
        {
            public decimal Value { get; set; } = value;
            public string Result { get; set; } = result;
        }

        private class FractionTest(FractionOption format, List<TestNumbers.FractionResult> results)
        {
            public FractionOption Format { get; set; } = format;
            public List<FractionResult> Results { get; set; } = results;
        }

        private static readonly FractionTest[] FractionTests =
        [
            new(FractionOption.Round,
            [
                new(0.0m, "zero"),
                new(0.0001m, "zero"),
                new(0.25m, "zero"),
                new(0.5m, "zero"),
                new(0.5001m, "one"),
                new(0.75m, "one"),
                new(0.9999m, "one"),
            ]),
            new(FractionOption.Truncate,
            [
                new(0.0m, "zero"),
                new(0.0001m, "zero"),
                new(0.25m, "zero"),
                new(0.5m, "zero"),
                new(0.75m, "zero"),
                new(0.9999m, "zero"),
            ]),
            new(FractionOption.Fraction,
            [
                new(0.0m, "zero"),
                new(0.001m, "zero and 1/1000"),
                new(0.01m, "zero and 1/100"),
                new(0.1m, "zero and 1/10"),
                new(0.5m, "zero and 1/2"),
                new(0.25m, "zero and 1/4"),
                new(0.75m, "zero and 3/4"),
                new(0.33m, "zero and 33/100"),
                new(0.66m, "zero and 33/50"),
            ]),
            new(FractionOption.Words,
            [
                new(0.0m, "zero"),
                new(0.5m, "zero and one half"),
                new(0.25m, "zero and one fourth"),
                new(0.75m, "zero and three fourths"),
                new(0.33m, "zero and thirty-three one hundredths"),
                new(0.66m, "zero and thirty-three fiftieths"),
            ]),
            new(FractionOption.Check,
            [
                new(0.0m, "zero and 00/100"),
                new(0.001m, "zero and 00/100"),
                new(0.01m, "zero and 01/100"),
                new(0.1m, "zero and 10/100"),
                new(0.5m, "zero and 50/100"),
                new(0.25m, "zero and 25/100"),
                new(0.75m, "zero and 75/100"),
                new(0.33m, "zero and 33/100"),
                new(0.66m, "zero and 66/100"),
                new(0.0001m, "zero and 00/100"),
                new(0.9999m, "one and 00/100"),
            ]),
            new(FractionOption.UsCurrency,
            [
                new(0.0m, "zero dollars and no cents"),
                new(0.001m, "zero dollars and no cents"),
                new(0.01m, "zero dollars and one cent"),
                new(0.5m, "zero dollars and fifty cents"),
                new(0.25m, "zero dollars and twenty-five cents"),
                new(0.75m, "zero dollars and seventy-five cents"),
                new(0.33m, "zero dollars and thirty-three cents"),
                new(0.66m, "zero dollars and sixty-six cents"),
                new(0.0001m, "zero dollars and no cents"),
                new(0.9999m, "one dollar and no cents"),
            ])
        ];

        [TestMethod]
        public void TestFractions()
        {
            // Default parameter

            Assert.AreEqual("zero", 0.0m.Wordify());
            Assert.AreEqual("zero and 1/2", 0.5m.Wordify());
            Assert.AreEqual("zero and 1/4", 0.25m.Wordify());
            Assert.AreEqual("zero and 3/4", 0.75m.Wordify());
            Assert.AreEqual("zero and 33/100", 0.33m.Wordify());
            Assert.AreEqual("zero and 33/50", 0.66m.Wordify());

            // Readme.md examples
            Assert.AreEqual("three hundred forty-five and 7/10", 345.7.Wordify());
            Assert.AreEqual("three hundred forty-five and 7/10", 345.7.Wordify(FractionOption.Fraction));
            Assert.AreEqual("three hundred forty-five and 70/100", 345.7.Wordify(FractionOption.Check));
            Assert.AreEqual("three hundred forty-five and seven tenths", 345.7.Wordify(FractionOption.Words));
            Assert.AreEqual("three hundred forty-six", 345.7.Wordify(FractionOption.Round));
            Assert.AreEqual("three hundred forty-five", 345.7.Wordify(FractionOption.Truncate));
            Assert.AreEqual("three hundred forty-five and .7", 345.7.Wordify(FractionOption.Decimal));
            Assert.AreEqual("three hundred forty-five dollars and seventy cents", 345.7.Wordify(FractionOption.UsCurrency));

            Assert.AreEqual("three hundred forty-five and 67/100", 345.67.Wordify());
            Assert.AreEqual("twelve and 1/10", 12.1.Wordify());
            Assert.AreEqual("zero and 1/2", .5.Wordify());

            foreach (FractionTest test in FractionTests)
            {
                foreach (FractionResult result in test.Results)
                    Assert.AreEqual(result.Result, result.Value.Wordify(test.Format));
            }
        }
    }
}
