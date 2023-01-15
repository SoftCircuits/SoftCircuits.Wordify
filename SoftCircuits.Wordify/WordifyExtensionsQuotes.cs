// Copyright (c) 2023 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.Wordify
{
    public static partial class WordifyExtensions
    {
        /// <summary>
        /// Encloses the given string in double quotes.
        /// </summary>
        /// <param name="s">The string to be enclosed in quotes.</param>
        /// <returns>The transformed string.</returns>
        public static string WrapInQuotes(this string? s) => $"\"{s}\"";

        /// <summary>
        /// Encloses the given string in single quotes.
        /// </summary>
        /// <param name="s">The string to be enclosed in quotes.</param>
        /// <returns>The transformed string.</returns>
        public static string WrapInSingleQuotes(this string? s) => $"'{s}'";

        /// <summary>
        /// Encloses the given character in double quotes.
        /// </summary>
        /// <param name="c">The character to be enclosed in quotes.</param>
        /// <returns>The transformed string.</returns>
        public static string WrapInQuotes(this char c) => $"\"{c}\"";

        /// <summary>
        /// Encloses the given character in single quotes.
        /// </summary>
        /// <param name="c">The character to be enclosed in quotes.</param>
        /// <returns>The transformed string.</returns>
        public static string WrapInSingleQuotes(this char c) => $"'{c}'";
    }
}
