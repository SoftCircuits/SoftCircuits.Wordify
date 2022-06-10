﻿// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
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
        /// Use words instead of digits for numbers.
        /// </summary>
        UseWords,
    }
}
