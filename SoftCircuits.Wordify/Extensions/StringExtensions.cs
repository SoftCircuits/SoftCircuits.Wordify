// Copyright (c) 2023-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.Wordify.Extensions
{
    internal static class StringExtensions
    {
        /// <summary>
        /// Returns true if this string has any whitespace characters.
        /// </summary>
        public static bool HasWhiteSpace(this string? s)
        {
            if (s != null)
            {
                for (int i = 0; i < s.Length; i++)
                {
                    if (char.IsWhiteSpace(s[i]))
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns true if this string has any whitespace characters other than leading or trailing whitespace.
        /// </summary>
        public static bool HasEmbeddedWhiteSpace(this string? s)
        {
            if (s != null)
            {
                // Skip leading whitespace
                int i = 0;
                while (i < s.Length && char.IsWhiteSpace(s[i]))
                    i++;

                // Look for embedded whitespace
                for (; i < s.Length; i++)
                {
                    if (char.IsWhiteSpace(s[i]))
                    {
                        // Found whitespace, is it followed by non-whitespace?
                        for (i++; i < s.Length; i++)
                        {
                            if (!char.IsWhiteSpace(s[i]))
                                return true;
                        }
                        break;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Finds the index of the first character for which <paramref name="predicate"/> returns true.
        /// </summary>
        /// <param name="s">This string.</param>
        /// <param name="predicate">Predicate that returns true for the character being sought.</param>
        /// <param name="startIndex">The starting index. Use -1 to start at the start of the string.</param>
        /// <returns>The index of the first match, or -1 if no match was found.</returns>
        public static int IndexOf(this string s, Func<char, bool> predicate, int startIndex = -1)
        {
            for (int i = Math.Max(startIndex, 0); i < s.Length; i++)
            {
                if (predicate(s[i]))
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Returns the index of the last character that causes <paramref name="predicate"/> to return true,
        /// or -1 if no match not found.
        /// </summary>
        public static int LastIndexOf(this string s, Func<char, bool> predicate, int startIndex = -1)
        {
            if (startIndex < 0)
                startIndex = s.Length - 1;

            for (int i = startIndex; i >= 0; i--)
            {
                if (predicate(s[i]))
                    return i;
            }
            return -1;
        }
    }
}
