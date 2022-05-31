namespace SoftCircuits.Wordify
{
    public static partial class Wordify
    {
        /// <summary>
        /// Encloses the given string in double quotes.
        /// </summary>
        /// <param name="s">The string to be enclosed in quotes.</param>
        /// <returns>The transformed string.</returns>
        public static string DoubleQuotes(this string? s) => $"\"{s}\"";

        /// <summary>
        /// Encloses the given string in single quotes.
        /// </summary>
        /// <param name="s">The string to be enclosed in quotes.</param>
        /// <returns>The transformed string.</returns>
        public static string SingleQuotes(this string? s) => $"'{s}'";

        /// <summary>
        /// Encloses the given character in double quotes.
        /// </summary>
        /// <param name="c">The character to be enclosed in quotes.</param>
        /// <returns>The transformed string.</returns>
        public static string DoubleQuotes(this char c) => $"\"{c}\"";

        /// <summary>
        /// Encloses the given character in single quotes.
        /// </summary>
        /// <param name="c">The character to be enclosed in quotes.</param>
        /// <returns>The transformed string.</returns>
        public static string SingleQuotes(this char c) => $"'{c}'";
    }
}
