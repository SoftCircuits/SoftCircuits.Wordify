// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.Wordify
{
    /// <summary>
    /// Specifies how a string's case should be set.
    /// </summary>
    public enum CaseOption
    {
        /// <summary>
        /// Convert each character in the string to upper case.
        /// </summary>
        Upper,

        /// <summary>
        /// Convert each character in the string to lower case.
        /// </summary>
        Lower,

        /// <summary>
        /// Convert the first letter to upper case. All other characters are left unchanged.
        /// </summary>
        Capitalize,

        /// <summary>
        /// Convert the first letter of each sentence to upper case.
        /// </summary>
        Sentence,

        /// <summary>
        /// Convert the string to title capitalization.
        /// </summary>
        Title,
    }
}
