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
            Assert.AreEqual("First enum", Wordify.Transform(MyEnums.One));
            Assert.AreEqual("One", Wordify.Transform(MyEnums.One, true));

            Assert.AreEqual("Second enum", Wordify.Transform(MyEnums.Two));
            Assert.AreEqual("Two", Wordify.Transform(MyEnums.Two, true));

            Assert.AreEqual("Third enum", Wordify.Transform(MyEnums.Three));
            Assert.AreEqual("Three", Wordify.Transform(MyEnums.Three, true));

            Assert.AreEqual("On The Go", Wordify.Transform(MyEnums.OnTheGo));
            Assert.AreEqual("On The Go", Wordify.Transform(MyEnums.OnTheGo, true));

            Assert.AreEqual("Read HTML Page", Wordify.Transform(MyEnums.ReadHTMLPage));
            Assert.AreEqual("Read HTML Page", Wordify.Transform(MyEnums.ReadHTMLPage, true));
        }
    }
}
