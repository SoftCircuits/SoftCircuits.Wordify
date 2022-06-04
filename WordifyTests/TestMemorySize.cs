// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using SoftCircuits.Wordify;

namespace WordifyTests
{
    [TestClass]
    public class TestMemorySize
    {
        private const ulong KBytes = 1024UL;
        private const ulong MBytes = 1048576UL;
        private const ulong GBytes = 1073741824UL;
        private const ulong TBytes = 1099511627776UL;
        private const ulong PBytes = 1125899906842624UL;
        //private const ulong EBytes = 1180591620717411303424UL;
        //private const ulong ZBytes = 1208925819614629174706176UL;

        [TestMethod]
        public void Test()
        {

            Assert.AreEqual("0 B", Wordify.ToMemorySize(0UL));
            Assert.AreEqual("1 B", Wordify.ToMemorySize(1UL));
            Assert.AreEqual("2 B", Wordify.ToMemorySize(2UL));
            Assert.AreEqual("1 KB", Wordify.ToMemorySize(KBytes));
            Assert.AreEqual("1 MB", Wordify.ToMemorySize(MBytes));
            Assert.AreEqual("1 GB", Wordify.ToMemorySize(GBytes));
            Assert.AreEqual("1 TB", Wordify.ToMemorySize(TBytes));
            Assert.AreEqual("1 PB", Wordify.ToMemorySize(PBytes));
            //Assert.AreEqual("1 EB", Wordify.ToMemorySize(EBytes));
            //Assert.AreEqual("1 ZB", Wordify.ToMemorySize(ZBytes));

            Assert.AreEqual("1.5 KB", Wordify.ToMemorySize(KBytes + KBytes / 2));
            Assert.AreEqual("1.5 MB", Wordify.ToMemorySize(MBytes + MBytes / 2));
            Assert.AreEqual("1.5 GB", Wordify.ToMemorySize(GBytes + GBytes / 2));
            Assert.AreEqual("1.5 TB", Wordify.ToMemorySize(TBytes + TBytes / 2));
            Assert.AreEqual("1.5 PB", Wordify.ToMemorySize(PBytes + PBytes / 2));
            Assert.AreEqual("1.1 KB", Wordify.ToMemorySize(KBytes + KBytes / 10));
            Assert.AreEqual("1.25 KB", Wordify.ToMemorySize(KBytes + KBytes / 4));
            Assert.AreEqual("1.75 MB", Wordify.ToMemorySize(MBytes + (MBytes / 4 * 3)));
            Assert.AreEqual("1.33 GB", Wordify.ToMemorySize(GBytes + GBytes / 3));
            Assert.AreEqual("1.25 TB", Wordify.ToMemorySize(TBytes + TBytes / 4));
            Assert.AreEqual("1.2 PB", Wordify.ToMemorySize(PBytes + PBytes / 5));
            Assert.AreEqual("1.1 PB", Wordify.ToMemorySize(PBytes + PBytes / 10));

            Assert.AreEqual(1UL, Wordify.FromMemorySize("1"));
            Assert.AreEqual(1UL, Wordify.FromMemorySize("1 b"));
            Assert.AreEqual(1UL, Wordify.FromMemorySize("1BYTES"));
            Assert.AreEqual(KBytes, Wordify.FromMemorySize("1  KB"));
            Assert.AreEqual(MBytes, Wordify.FromMemorySize("1 mb "));
            Assert.AreEqual(GBytes, Wordify.FromMemorySize("1gb "));
            Assert.AreEqual(TBytes, Wordify.FromMemorySize("1 TB"));
            Assert.AreEqual(PBytes, Wordify.FromMemorySize("1PB"));
            //Assert.AreEqual(EBytes, Wordify.FromMemorySize("1 eb"));
            //Assert.AreEqual(ZBytes, Wordify.FromMemorySize("1 ZB"));

            Assert.AreEqual(1026UL + 100UL, Wordify.FromMemorySize("1.1 kb"));
            Assert.AreEqual(KBytes + KBytes / 2, Wordify.FromMemorySize("1.5 kb"));
            Assert.AreEqual(MBytes + MBytes / 2, Wordify.FromMemorySize("   1.5   MB   "));
            Assert.AreEqual(GBytes + GBytes / 2, Wordify.FromMemorySize("1.5gb"));
            Assert.AreEqual(KBytes + KBytes / 4, Wordify.FromMemorySize("1.25 kb"));
            Assert.AreEqual(MBytes + (MBytes / 4 * 3), Wordify.FromMemorySize("1.75 MB"));
        }
    }
}
