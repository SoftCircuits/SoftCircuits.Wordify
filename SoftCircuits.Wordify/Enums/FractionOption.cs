// Copyright (c) 2023 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.Wordify
{
    /// <summary>
    /// Specifies how a fraction should be formatted.
    /// </summary>
    public enum FractionOption
    {
        /// <summary>
        /// Round to the nearest whole number value and do not display any fractional portion.
        /// </summary>
        Round,

        /// <summary>
        /// Truncate down to the nearest whole number value and do not display and fraction portion.
        /// </summary>
        Truncate,

        /// <summary>
        /// Display any fractional portion using a decimal (e.g. "4.75").
        /// </summary>
        Decimal,

        /// <summary>
        /// Display any fractional portion using a fraction (e.g. "3/4").
        /// </summary>
        Fraction,

        /// <summary>
        /// Display any fractional portion using words (e.g. "three fourths").
        /// </summary>
        Words,

        /// <summary>
        /// Display the fractional portion as is common when writing checks (e.g. "75/100").
        /// </summary>
        Check,

        /// <summary>
        /// Display the fraction portion as US currency (e.g. "seventy-five cents").
        /// </summary>
        UsCurrency,
    }
}
