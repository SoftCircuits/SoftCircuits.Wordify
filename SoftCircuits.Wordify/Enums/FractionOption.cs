// Copyright (c) 2023-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace SoftCircuits.Wordify
#pragma warning restore IDE0130 // Namespace does not match folder structure
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
        /// Display any fractional portion using a decimal (e.g. &quot;4.75&quot;).
        /// </summary>
        Decimal,

        /// <summary>
        /// Display any fractional portion using a fraction (e.g. &quot;3/4&quot;).
        /// </summary>
        Fraction,

        /// <summary>
        /// Display any fractional portion using words (e.g. &quot;three fourths&quot;).
        /// </summary>
        Words,

        /// <summary>
        /// Display the fractional portion as is common when writing checks (e.g. &quot;75/100&quot;).
        /// </summary>
        Check,

        /// <summary>
        /// Display the fraction portion as US currency (e.g. &quot;seventy-five cents&quot;).
        /// </summary>
        UsCurrency,
    }
}
