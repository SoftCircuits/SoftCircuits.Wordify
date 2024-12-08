// Copyright (c) 2023-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace SoftCircuits.Wordify
#pragma warning restore IDE0130 // Namespace does not match folder structure
{
    /// <summary>
    /// Specifies options for the memory size methods.
    /// </summary>
    [Flags]
    public enum MemorySizeOption
    {
        /// <summary>
        /// Specifies that numbers should be formatted using power of ten. For example, 1 KB = 1,000,
        /// 1 MB = 1,000,000, 1 GB = 1,000,000,000, etc.
        /// </summary>
        Decimal = 0x0000,

        /// <summary>
        /// Specifies numbers should be formatted using power of two. For example, 1 KiB = 1024,
        /// 1 MiB = 1,048,576, 1 GiB = 1,073,741,824, etc.
        /// </summary>
        Binary = 0x0001
    }
}
