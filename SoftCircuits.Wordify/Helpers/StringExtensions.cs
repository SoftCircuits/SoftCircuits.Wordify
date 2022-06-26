// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.Wordify.Helpers
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
    }
}
