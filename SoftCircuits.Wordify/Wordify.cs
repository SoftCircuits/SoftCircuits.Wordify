// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using System.Text;

namespace SoftCircuits.Wordify
{
    public partial class Wordify
    {
        /// <summary>
        /// Returns a copy of this tring with all whitespace sequences replaced with a single space
        /// character and all leading and trailing whitespace removed.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <returns>The modified string.</returns>
        public static string NormalizeWhiteSpace(this string? s)
        {
            bool wasSpace = false;

            if (s == null)
                return string.Empty;

            StringBuilder builder = new(s.Length);
            foreach (char c in s)
            {
                if (char.IsWhiteSpace(c))
                {
                    if (builder.Length > 0)
                        wasSpace = true;
                }
                else
                {
                    if (wasSpace)
                        builder.Append(' ');
                    builder.Append(c);
                    wasSpace = false;
                }
            }
            return builder.ToString();
        }

        /// <summary>
        /// Counts the number of words in this string. Words are separated by one or more whitespace
        /// character.
        /// </summary>
        /// <returns></returns>
        public static int CountWords(this string? s)
        {
            bool wasSpace = true;
            int words = 0;

            if (s != null)
            {
                foreach (char c in s)
                {
                    if (char.IsWhiteSpace(c))
                    {
                        wasSpace = true;
                    }
                    else
                    {
                        if (wasSpace)
                            words++;
                        wasSpace = false;
                    }
                }
            }
            return words;
        }

        #region Null and empty strings

        /// <summary>
        /// Returns this string, or an empty string if this string is null.
        /// </summary>
        public static string EmptyIfNull(this string? s) => s ?? string.Empty;

        /// <summary>
        /// Returns this string, or null if this string is empty.
        /// </summary>
        public static string? NullIfEmpty(this string? s) => string.IsNullOrEmpty(s) ? null : s;

        /// <summary>
        /// Returns this string, or an empty string if this string is null or contains only whitespace.
        /// </summary>
        public static string EmptyIfNullOrWhiteSpace(this string? s) => string.IsNullOrWhiteSpace(s) ? string.Empty : s;

        /// <summary>
        /// Returns this string, or null if this string is null or contains only whitespace.
        /// </summary>
        public static string? NullIfEmptyOrWhiteSpace(this string? s) => string.IsNullOrWhiteSpace(s) ? null : s;

        #endregion

        /// <summary>
        /// Inserts spaces into this string. If the string contains hyphens, they are replaced with spaces.
        /// Otherwise, if the string contains underscores, they are replaced with spaces. Otherwise, spaces
        /// are inserted based on camel case letters. If the string already contains embedded spaces, no
        /// changes are made.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string InsertSpaces(this string? s)
        {
            if (s == null)
                return string.Empty;

            if (s.Length == 0 || s.HasEmbeddedWhiteSpace())
                return s;

            if (s.Contains('-'))
                return s.Replace('-', ' ');

            if (s.Contains('_'))
                return s.Replace('_', ' ');

            return s.InsertCamelCaseSpaces();
        }

        /// <summary>
        /// Inserts spaces between words as indicated by camel case. For example,
        /// "CamelCase" would be converted to "Camel Case".
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string InsertCamelCaseSpaces(this string? s)
        {
            bool lastIsUpper = false;
            bool lastIsWhitespace = false;

            if (s == null)
                return string.Empty;

            StringBuilder sb = new();

            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                bool isUpper = char.IsUpper(c);
                bool nextIsLower = i + 1 < s.Length && char.IsLower(s[i + 1]);

                if (isUpper && sb.Length > 0 && (!lastIsUpper || nextIsLower) && !lastIsWhitespace)
                    sb.Append(' ');

                sb.Append(c);
                lastIsUpper = isUpper;
                lastIsWhitespace = char.IsWhiteSpace(c);
            }
            return sb.ToString();
        }
    }
}
