namespace SoftCircuits.Wordify
{
    public enum FractionFormat
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
        /// Display any fractional portion using a fraction (e.g. "4 3/4").
        /// </summary>
        Fraction,

        /// <summary>
        /// Display any fractional portion using words.
        /// </summary>
        Words,

        /// <summary>
        /// Display the fractional portion as is common when writing checks (e.g. "4 75/100").
        /// </summary>
        Check
    }
}
