// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using SoftCircuits.Wordify;

namespace WordifyTests
{
    [TestClass]
    public class TestFormat
    {
        [TestMethod]
        public void TestPhoneNumbers()
        {
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
