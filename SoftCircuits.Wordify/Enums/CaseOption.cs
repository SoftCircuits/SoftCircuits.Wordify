// Copyright (c) 2023-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace SoftCircuits.Wordify
#pragma warning restore IDE0130 // Namespace does not match folder structure
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
        /// Convert the first letter of each word to upper case. All other characters are left unchanged.
        /// </summary>
        CapitalizeAll,

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
