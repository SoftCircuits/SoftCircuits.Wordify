// Copyright (c) 2023 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using SoftCircuits.Wordify;

namespace WordifyTests
{
    [TestClass]
    public class TestEnums
    {
        enum MyEnums
        {
            [System.ComponentModel.Description("First enum")]
            One,
            [System.ComponentModel.Description("Second enum")]
            Two,
            [System.ComponentModel.Description("Third enum")]
            Three,
            OnTheGo,
            ReadHTMLPage,
        }

        [TestMethod]
        public void Test()
        {
            Assert.AreEqual("First enum", MyEnums.One.Wordify());
            Assert.AreEqual("One", MyEnums.One.Wordify(true));

            Assert.AreEqual("Second enum", MyEnums.Two.Wordify());
            Assert.AreEqual("Two", MyEnums.Two.Wordify(true));

            Assert.AreEqual("Third enum", MyEnums.Three.Wordify());
            Assert.AreEqual("Three", MyEnums.Three.Wordify(true));

            Assert.AreEqual("On The Go", MyEnums.OnTheGo.Wordify());
            Assert.AreEqual("On The Go", MyEnums.OnTheGo.Wordify(true));

            Assert.AreEqual("Read HTML Page", MyEnums.ReadHTMLPage.Wordify());
            Assert.AreEqual("Read HTML Page", MyEnums.ReadHTMLPage.Wordify(true));
        }
    }
}
