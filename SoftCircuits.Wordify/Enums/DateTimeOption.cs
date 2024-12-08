// Copyright (c) 2023-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace SoftCircuits.Wordify
#pragma warning restore IDE0130 // Namespace does not match folder structure
{
    /// <summary>
    /// Specifies options for <see cref="DateTime"/> and <see cref="TimeSpan"/> methods.
    /// </summary>
    [Flags]
    public enum DateTimeOption
    {
        /// <summary>
        /// No options specified.
        /// </summary>
        None = 0,

        /// <summary>
        /// Represent numbers with words instead of digits.
        /// </summary>
        UseWords,
    }
}
