// Copyright (c) 2023-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using SoftCircuits.Wordify;

namespace WordifyTests
{
    [TestClass]
    public class TestFormat
    {
        [TestMethod]
        public void TestNames()
        {
            Assert.AreEqual("", Wordify.FormatName());
            Assert.AreEqual("John", Wordify.FormatName("John"));
            Assert.AreEqual("Van Dyk", Wordify.FormatName(null, "Van Dyk"));
            Assert.AreEqual("John Van Dyk", Wordify.FormatName("John", "Van Dyk"));
            Assert.AreEqual("John W. Van Dyk", Wordify.FormatName("John", "Van Dyk", "W."));
            Assert.AreEqual("Dr. John W. Van Dyk", Wordify.FormatName("John", "Van Dyk", "W.", "Dr."));
            Assert.AreEqual("Dr. John W. Van Dyk III", Wordify.FormatName("John", "Van Dyk", "W.", "Dr.", "III"));
        }

        [TestMethod]
        public void TestAddresses()
        {
            Assert.AreEqual("", Wordify.FormatAddress());
            Assert.AreEqual("123 Elm", Wordify.FormatAddress("123 Elm"));
            Assert.AreEqual("123 Elm|Apt 3", Wordify.FormatAddress("123 Elm", "Apt 3", delimiter: "|"));
            Assert.AreEqual("123 Elm|Apt 3|Small Town, UT 84084|United States", Wordify.FormatAddress("123 Elm", "Apt 3", "Small Town", "UT", "84084", "United States", delimiter: "|"));
        }

        [TestMethod]
        public void TestCityStateZips()
        {
            Assert.AreEqual("", Wordify.FormatCityStateZip());
            Assert.AreEqual("Small Town", Wordify.FormatCityStateZip(city: "Small Town"));
            Assert.AreEqual("UT", Wordify.FormatCityStateZip(state: "UT"));
            Assert.AreEqual("84084", Wordify.FormatCityStateZip(zip: "84084"));
            Assert.AreEqual("Small Town, UT", Wordify.FormatCityStateZip(city: "Small Town", state: "UT"));
            Assert.AreEqual("UT 84084", Wordify.FormatCityStateZip(state: "UT", zip: "84084"));
            Assert.AreEqual("Small Town 84084", Wordify.FormatCityStateZip(city: "Small Town", zip: "84084"));
            Assert.AreEqual("Small Town, UT 84084", Wordify.FormatCityStateZip("Small Town", "UT", "84084"));
        }

        [TestMethod]
        public void TestPhoneNumbers()
        {
            Assert.AreEqual("", ((string)null!).FormatPhoneNumber(PhoneOption.None));
            Assert.AreEqual("", "".FormatPhoneNumber(PhoneOption.None));
            Assert.AreEqual("", "Abc".FormatPhoneNumber(PhoneOption.None));
            Assert.AreEqual("1234", "1234".FormatPhoneNumber(PhoneOption.None));
            Assert.AreEqual("123-4567", "1234567".FormatPhoneNumber(PhoneOption.None));
            Assert.AreEqual("123-456-7890", "1234567890".FormatPhoneNumber(PhoneOption.None));
            Assert.AreEqual("(123) 456-7890", "1234567890".FormatPhoneNumber(PhoneOption.AreaCodeParentheses));
            Assert.AreEqual("1-234-567-8901", "12345678901".FormatPhoneNumber(PhoneOption.None));
            Assert.AreEqual("1 (234) 567-8901", "12345678901".FormatPhoneNumber(PhoneOption.AreaCodeParentheses));
            Assert.AreEqual("+1-234-567-8901", "12345678901".FormatPhoneNumber(PhoneOption.InternationalPlusSign));
            Assert.AreEqual("+1 (234) 567-8901", "12345678901".FormatPhoneNumber(PhoneOption.InternationalPlusSign | PhoneOption.AreaCodeParentheses));
            Assert.AreEqual("123-456-789-0123", "1234567890123".FormatPhoneNumber(PhoneOption.None));
            Assert.AreEqual("123-4567", "@@1@2@3@4@5@6@7@@".FormatPhoneNumber(PhoneOption.None));
            Assert.AreEqual("123-4567", "  1234567  ".FormatPhoneNumber(PhoneOption.None));
            Assert.AreEqual("123-4567", "1  2  3  4  5  6  7".FormatPhoneNumber(PhoneOption.None));
        }
    }
}
