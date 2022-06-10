// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using SoftCircuits.Wordify;

namespace WordifyTests
{
    [TestClass]
    public class TestMemorySize
    {
        private const ulong Kilobytes = 1024UL;
        private const ulong Megabytes = 1048576UL;
        private const ulong Gigabytes = 1073741824UL;
        private const ulong Terabytes = 1099511627776UL;
        private const ulong Petabytes = 1125899906842624UL;
        private const ulong Exabytes = 1152921504606846976UL;
        //private const ulong Zettabytes = 1180591620717411303424UL;
        //private const ulong Yottabytes = 1208925819614629174706176UL;

        [TestMethod]
        public void Test()
        {
            Assert.AreEqual("0 B", 0UL.ToMemorySize());
            Assert.AreEqual("1 B", 1UL.ToMemorySize());
            Assert.AreEqual("2 B", 2UL.ToMemorySize());
            Assert.AreEqual("1 KB", Kilobytes.ToMemorySize());
            Assert.AreEqual("1 MB", Megabytes.ToMemorySize());
            Assert.AreEqual("1 GB", Gigabytes.ToMemorySize());
            Assert.AreEqual("1 TB", Terabytes.ToMemorySize());
            Assert.AreEqual("1 PB", Petabytes.ToMemorySize());
            Assert.AreEqual("1 EB", Exabytes.ToMemorySize());
            //Assert.AreEqual("1 ZB", Zettabytes.ToMemorySize());
            //Assert.AreEqual("1 YB", Yottabytes.ToMemorySize());

            Assert.AreEqual("1.5 KB", (Kilobytes + Kilobytes / 2).ToMemorySize());
            Assert.AreEqual("1.5 MB", (Megabytes + Megabytes / 2).ToMemorySize());
            Assert.AreEqual("1.5 GB", (Gigabytes + Gigabytes / 2).ToMemorySize());
            Assert.AreEqual("1.5 TB", (Terabytes + Terabytes / 2).ToMemorySize());
            Assert.AreEqual("1.5 PB", (Petabytes + Petabytes / 2).ToMemorySize());
            Assert.AreEqual("1.1 KB", (Kilobytes + Kilobytes / 10).ToMemorySize());
           Assert.AreEqual("1.25 KB", (Kilobytes + Kilobytes / 4).ToMemorySize());
           Assert.AreEqual("1.75 MB", (Megabytes + (Megabytes / 4 * 3)).ToMemorySize());
           Assert.AreEqual("1.33 GB", (Gigabytes + Gigabytes / 3).ToMemorySize());
           Assert.AreEqual("1.25 TB", (Terabytes + Terabytes / 4).ToMemorySize());
            Assert.AreEqual("1.2 PB", (Petabytes + Petabytes / 5).ToMemorySize());
            Assert.AreEqual("1.1 PB", (Petabytes + Petabytes / 10).ToMemorySize());

            Assert.AreEqual(1UL, "1".FromMemorySize());
            Assert.AreEqual(1UL, "1 b".FromMemorySize());
            Assert.AreEqual(1UL, "1BYTES".FromMemorySize());
            Assert.AreEqual(Kilobytes, "1  KB".FromMemorySize());
            Assert.AreEqual(Megabytes, "1 mb ".FromMemorySize());
            Assert.AreEqual(Gigabytes, "1gb ".FromMemorySize());
            Assert.AreEqual(Terabytes, "1 TB".FromMemorySize());
            Assert.AreEqual(Petabytes, "1PB".FromMemorySize());
            Assert.AreEqual(Exabytes, "1 eb".FromMemorySize());
            //Assert.AreEqual(Zettabytes, "1 ZB".FromMemorySize());
            //Assert.AreEqual(Yottabytes, "1 YB".FromMemorySize());

            Assert.AreEqual(1026UL + 100UL, "1.1 kb".FromMemorySize());
            Assert.AreEqual(Kilobytes + Kilobytes / 2, "1.5 kb".FromMemorySize());
            Assert.AreEqual(Megabytes + Megabytes / 2, "   1.5   MB   ".FromMemorySize());
            Assert.AreEqual(Gigabytes + Gigabytes / 2, "1.5gb".FromMemorySize());
            Assert.AreEqual(Kilobytes + Kilobytes / 4, "1.25 kb".FromMemorySize());
            Assert.AreEqual(Megabytes + (Megabytes / 4 * 3), "1.75 MB".FromMemorySize());
        }
    }
}
