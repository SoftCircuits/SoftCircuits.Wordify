// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using SoftCircuits.Wordify;
using SoftCircuits.Wordify.Enums;

namespace WordifyTests
{
    [TestClass]
    public class TestMemorySize
    {
        private const ulong Kilobyte = 1000UL;
        private const ulong Megabyte = 1000000UL;
        private const ulong Gigabyte = 1000000000UL;
        private const ulong Terabyte = 1000000000000UL;
        private const ulong Petabyte = 1000000000000000UL;
        private const ulong Exabyte = 1000000000000000000UL;
        //private const ulong Zettabyte = 1000000000000000000000UL;
        //private const ulong Yottabyte = 1000000000000000000000000UL;

        private const ulong Kibibyte = 1024UL;
        private const ulong Mebibyte = 1048576UL;
        private const ulong Gibibyte = 1073741824UL;
        private const ulong Tebibyte = 1099511627776UL;
        private const ulong Pebibyte = 1125899906842624UL;
        private const ulong Exbibyte = 1152921504606846976UL;
        //private const ulong Zebibyte = 1180591620717411303424UL;
        //private const ulong Yobibyte = 1208925819614629174706176UL;

        [TestMethod]
        public void Test()
        {
            Assert.AreEqual("0 B", 0.ToMemorySize());
            Assert.AreEqual("1 B", 1.ToMemorySize());
            Assert.AreEqual("2 B", 2.ToMemorySize());
            Assert.AreEqual("1 KB", Kilobyte.ToMemorySize());
            Assert.AreEqual("1 MB", Megabyte.ToMemorySize());
            Assert.AreEqual("1 GB", Gigabyte.ToMemorySize());
            Assert.AreEqual("1 TB", Terabyte.ToMemorySize());
            Assert.AreEqual("1 PB", Petabyte.ToMemorySize());
            Assert.AreEqual("1 EB", Exabyte.ToMemorySize());
            //Assert.AreEqual("1 ZB", Zettabyte.ToMemorySize());
            //Assert.AreEqual("1 YB", Yottabyte.ToMemorySize());

            Assert.AreEqual("0 B", 0.ToMemorySize(MemorySizeOption.Binary));
            Assert.AreEqual("1 B", 1.ToMemorySize(MemorySizeOption.Binary));
            Assert.AreEqual("2 B", 2.ToMemorySize(MemorySizeOption.Binary));
            Assert.AreEqual("1 KiB", Kibibyte.ToMemorySize(MemorySizeOption.Binary));
            Assert.AreEqual("1 MiB", Mebibyte.ToMemorySize(MemorySizeOption.Binary));
            Assert.AreEqual("1 GiB", Gibibyte.ToMemorySize(MemorySizeOption.Binary));
            Assert.AreEqual("1 TiB", Tebibyte.ToMemorySize(MemorySizeOption.Binary));
            Assert.AreEqual("1 PiB", Pebibyte.ToMemorySize(MemorySizeOption.Binary));
            Assert.AreEqual("1 EiB", Exbibyte.ToMemorySize(MemorySizeOption.Binary));
            //Assert.AreEqual("1 ZiB", Zebibyte.ToMemorySize(MemorySizeOption.Binary));
            //Assert.AreEqual("1 YiB", Yobibyte.ToMemorySize(MemorySizeOption.Binary));

            Assert.AreEqual("1.5 KB", (Kilobyte + Kilobyte / 2).ToMemorySize());
            Assert.AreEqual("1.5 MB", (Megabyte + Megabyte / 2).ToMemorySize());
            Assert.AreEqual("1.5 GB", (Gigabyte + Gigabyte / 2).ToMemorySize());
            Assert.AreEqual("1.5 TB", (Terabyte + Terabyte / 2).ToMemorySize());
            Assert.AreEqual("1.5 PB", (Petabyte + Petabyte / 2).ToMemorySize());
            Assert.AreEqual("1.1 KB", (Kilobyte + Kilobyte / 10).ToMemorySize());
            Assert.AreEqual("1.25 KB", (Kilobyte + Kilobyte / 4).ToMemorySize());
            Assert.AreEqual("1.75 MB", (Megabyte + (Megabyte / 4 * 3)).ToMemorySize());
            Assert.AreEqual("1.33 GB", (Gigabyte + Gigabyte / 3).ToMemorySize());
            Assert.AreEqual("1.25 TB", (Terabyte + Terabyte / 4).ToMemorySize());
            Assert.AreEqual("1.2 PB", (Petabyte + Petabyte / 5).ToMemorySize());
            Assert.AreEqual("1.1 PB", (Petabyte + Petabyte / 10).ToMemorySize());

            Assert.AreEqual("1.5 KiB", (Kibibyte + Kibibyte / 2).ToMemorySize(MemorySizeOption.Binary));
            Assert.AreEqual("1.5 MiB", (Mebibyte + Mebibyte / 2).ToMemorySize(MemorySizeOption.Binary));
            Assert.AreEqual("1.5 GiB", (Gibibyte + Gibibyte / 2).ToMemorySize(MemorySizeOption.Binary));
            Assert.AreEqual("1.5 TiB", (Tebibyte + Tebibyte / 2).ToMemorySize(MemorySizeOption.Binary));
            Assert.AreEqual("1.5 PiB", (Pebibyte + Pebibyte / 2).ToMemorySize(MemorySizeOption.Binary));
            Assert.AreEqual("1.1 KiB", (Kibibyte + Kibibyte / 10).ToMemorySize(MemorySizeOption.Binary));
            Assert.AreEqual("1.25 KiB", (Kibibyte + Kibibyte / 4).ToMemorySize(MemorySizeOption.Binary));
            Assert.AreEqual("1.75 MiB", (Mebibyte + (Mebibyte / 4 * 3)).ToMemorySize(MemorySizeOption.Binary));
            Assert.AreEqual("1.33 GiB", (Gibibyte + Gibibyte / 3).ToMemorySize(MemorySizeOption.Binary));
            Assert.AreEqual("1.25 TiB", (Tebibyte + Tebibyte / 4).ToMemorySize(MemorySizeOption.Binary));
            Assert.AreEqual("1.2 PiB", (Pebibyte + Pebibyte / 5).ToMemorySize(MemorySizeOption.Binary));
            Assert.AreEqual("1.1 PiB", (Pebibyte + Pebibyte / 10).ToMemorySize(MemorySizeOption.Binary));

            Assert.AreEqual(1UL, "1".ParseMemorySize());
            Assert.AreEqual(1UL, "1 b".ParseMemorySize());
            Assert.AreEqual(1UL, "1BYTES".ParseMemorySize());
            Assert.AreEqual(Kilobyte, "1  KB".ParseMemorySize());
            Assert.AreEqual(Megabyte, "1 mb ".ParseMemorySize());
            Assert.AreEqual(Gigabyte, "1gb ".ParseMemorySize());
            Assert.AreEqual(Terabyte, "1 TB".ParseMemorySize());
            Assert.AreEqual(Petabyte, "1PB".ParseMemorySize());
            Assert.AreEqual(Exabyte, "1 eb".ParseMemorySize());
            //Assert.AreEqual(Zettabyte, "1 ZB".ParseMemorySize());
            //Assert.AreEqual(Yottabyte, "1 YB".ParseMemorySize());

            Assert.AreEqual(1UL, "1".ParseMemorySize());
            Assert.AreEqual(1UL, "1 b".ParseMemorySize());
            Assert.AreEqual(1UL, "1BYTES".ParseMemorySize());
            Assert.AreEqual(Kibibyte, "1  KiB".ParseMemorySize());
            Assert.AreEqual(Mebibyte, "1 mib ".ParseMemorySize());
            Assert.AreEqual(Gibibyte, "1gib ".ParseMemorySize());
            Assert.AreEqual(Tebibyte, "1 TIB".ParseMemorySize());
            Assert.AreEqual(Pebibyte, "1PIB".ParseMemorySize());
            Assert.AreEqual(Exbibyte, "1 eib".ParseMemorySize());
            //Assert.AreEqual(Zebibyte, "1 ZiB".ParseMemorySize());
            //Assert.AreEqual(Yobibyte, "1 YiB".ParseMemorySize());

            Assert.AreEqual(1000UL + 100UL, "1.1 kb".ParseMemorySize());
            Assert.AreEqual(Kilobyte + Kilobyte / 2, "1.5 kb".ParseMemorySize());
            Assert.AreEqual(Megabyte + Megabyte / 2, "   1.5   MB   ".ParseMemorySize());
            Assert.AreEqual(Gigabyte + Gigabyte / 2, "1.5gb".ParseMemorySize());
            Assert.AreEqual(Kilobyte + Kilobyte / 4, "1.25 kb".ParseMemorySize());
            Assert.AreEqual(Megabyte + (Megabyte / 4 * 3), "1.75 MB".ParseMemorySize());

            Assert.AreEqual(1026UL + 100UL, "1.1 kib".ParseMemorySize());
            Assert.AreEqual(Kibibyte + Kibibyte / 2, "1.5 kib".ParseMemorySize());
            Assert.AreEqual(Mebibyte + Mebibyte / 2, "   1.5   MIB   ".ParseMemorySize());
            Assert.AreEqual(Gibibyte + Gibibyte / 2, "1.5gib".ParseMemorySize());
            Assert.AreEqual(Kibibyte + Kibibyte / 4, "1.25 kib".ParseMemorySize());
            Assert.AreEqual(Mebibyte + (Mebibyte / 4 * 3), "1.75 MIB".ParseMemorySize());
        }
    }
}
