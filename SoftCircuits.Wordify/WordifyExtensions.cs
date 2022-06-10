﻿// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using System.Text;

namespace SoftCircuits.Wordify
{
    public partial class WordifyExtensions
    {
        /// <summary>
        /// Converts a string to text by attempting to insert spaces between words.
        /// </summary>
        /// <param name="s">The string to transform.</param>
        /// <param name="options">The transformation method.</param>
        /// <returns>The transformed string.</returns>
        public static string Wordify(this string? s, WordifyOption options = WordifyOption.AutoDetect)
        {
            if (s == null || s.Length == 0)
                return string.Empty;

            // Auto detect method if needed
            if (options == WordifyOption.AutoDetect)
            {
                if (s.HasEmbeddedWhiteSpace())
                    return s;
                if (s.Contains('_'))
                    options = WordifyOption.ReplaceUnderscores;
                else if (s.Contains('-'))
                    options = WordifyOption.ReplaceHyphens;
                else
                    options = WordifyOption.CamelCase;
            }

            return options switch
            {
                WordifyOption.ReplaceUnderscores => s.Replace('_', ' '),
                WordifyOption.ReplaceHyphens => s.Replace('-', ' '),
                WordifyOption.CamelCase => s.InsertCamelCaseSpaces(),
                _ => s,
            };
        }

        /// <summary>
        /// Inserts spaces between words as indicated by camel case. For example,
        /// "CamelCase" is converted to "Camel Case".
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <returns>The converted string.</returns>
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

        /// <summary>
        /// Counts the number of words in this string. Words are separated by one or more whitespace
        /// character.
        /// </summary>
        /// <returns>The modified string.</returns>
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

        /// <summary>
        /// Returns a copy of this string with all whitespace sequences replaced with a single space
        /// character, and all leading and trailing whitespace removed.
        /// </summary>
        /// <param name="s">The string to normalize.</param>
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

        #region Null and empty strings

        /// <summary>
        /// Returns the specified string, or an empty string if the string is null.
        /// </summary>
        public static string EmptyIfNull(this string? s) => s ?? string.Empty;

        /// <summary>
        /// Returns the specified string, or null if the string is empty.
        /// </summary>
        public static string? NullIfEmpty(this string? s) => string.IsNullOrEmpty(s) ? null : s;

        /// <summary>
        /// Returns the specified string, or an empty string if the string is null or contains only whitespace.
        /// </summary>
        public static string EmptyIfNullOrWhiteSpace(this string? s) => string.IsNullOrWhiteSpace(s) ? string.Empty : s;

        /// <summary>
        /// Returns the specified string, or null if the string is null or contains only whitespace.
        /// </summary>
        public static string? NullIfEmptyOrWhiteSpace(this string? s) => string.IsNullOrWhiteSpace(s) ? null : s;

        #endregion

    }
}