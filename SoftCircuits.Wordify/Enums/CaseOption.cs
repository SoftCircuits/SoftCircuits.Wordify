namespace SoftCircuits.Wordify
{
    public enum CaseOption
    {
        /// <summary>
        /// Converts each character in the string to upper case.
        /// </summary>
        Upper,

        /// <summary>
        /// Converts each character in the string to lower case.
        /// </summary>
        Lower,

        /// <summary>
        /// Convert only the first letter to upper case. All other characters left unchanged.
        /// unchanged.
        /// </summary>
        CapitalizeFirstLetter,

        /// <summary>
        /// Converts the first character of each sentence to upper case.
        /// </summary>
        Sentence,

        /// <summary>
        /// Converts the string to title capitalization.
        /// </summary>
        Title,
    }
}
