// Copyright (c) 2023 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.Wordify
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
