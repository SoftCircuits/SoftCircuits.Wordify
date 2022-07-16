// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using SoftCircuits.Wordify.Helpers;
using System.Diagnostics;

namespace SoftCircuits.Wordify.Extensions
{
    internal static class StringEditorExtensions
    {
        public static bool IsWordCharacter(this StringEditor se, int pos)
        {
            Debug.Assert(pos < se.Length);
            char c = se[pos];
            return char.IsLetterOrDigit(c) ||
                c == '\'' ||
                (c == '.' && pos < se.Length - 1 && char.IsDigit(se[pos + 1]));
        }

        public static bool IsEndOfSentenceCharacter(this StringEditor se, int pos)
        {
            Debug.Assert(pos < se.Length);
            char c = se[pos];
            return c == '!' ||
                c == '?' ||
                c == ':' ||
                (c == '.' && !(pos < (se.Length - 1) && char.IsDigit(se[pos + 1])));
        }

        /// <summary>
        /// Gets the start and end index of the first contiguous sequence of letters.
        /// </summary>
        public static bool FindFirstWord(this StringEditor se, out int startIndex, out int endIndex)
        {
            startIndex = se.IndexOf(char.IsLetter);
            if (startIndex >= 0)
            {
                endIndex = se.IndexOf(c => !char.IsLetter(c), startIndex + 1);
                if (endIndex < 0)
                    endIndex = se.Length;
                return true;
            }
            endIndex = -1;
            return false;
        }

        /// <summary>
        /// Gets the start and end index of the last contiguous sequence of letters.
        /// </summary>
        public static bool FindLastWord(this StringEditor se, out int startIndex, out int endIndex)
        {
            endIndex = se.LastIndexOf(char.IsLetter);
            if (endIndex >= 0)
            {
                startIndex = se.LastIndexOf(c => !char.IsLetter(c), endIndex - 1);
                if (startIndex < 0)
                    startIndex = 0;
                else
                    startIndex++;
                endIndex++;
                return true;
            }
            startIndex = -1;
            return false;
        }

        /// <summary>
        /// Finds the index of the specified character.
        /// </summary>
        /// <param name="c">The character to find.</param>
        /// <param name="startIndex">The starting index. Use -1 to start at the start of the string.</param>
        /// <param name="ignoreCase">Set to true for case-insensitive search.</param>
        /// <returns>The index of the first match, or -1 if no match was found.</returns>
        public static int IndexOf(this StringEditor se, char c, int startIndex = -1, bool ignoreCase = false)
        {
            IComparer<char> comparer = CharComparer.GetComparer(ignoreCase);

            for (int i = Math.Max(startIndex, 0); i < se.Length; i++)
            {
                if (comparer.Compare(c, se[i]) == 0)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Finds the index of the specified string.
        /// </summary>
        /// <param name="s">The string to find.</param>
        /// <param name="startIndex">The starting index. Use -1 to start at the start of the string.</param>
        /// <param name="ignoreCase">Set to true for case-insensitive search.</param>
        /// <returns>The index of the first match, or -1 if no match was found.</returns>
        public static int IndexOf(this StringEditor se, string s, int startIndex = -1, bool ignoreCase = false)
        {
            IComparer<char> comparer = CharComparer.GetComparer(ignoreCase);

            for (int i = Math.Max(startIndex, 0); i < se.Length - (s.Length - 1); i++)
            {
                if (se.MatchesAt(s, i, comparer))
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Finds the index of the first character for which <paramref name="predicate"/> returns true.
        /// </summary>
        /// <param name="predicate">Predicate that returns true for the character being sought.</param>
        /// <param name="startIndex">The starting index. Use -1 to start at the start of the string.</param>
        /// <returns>The index of the first match, or -1 if no match was found.</returns>
        public static int IndexOf(this StringEditor se, Func<char, bool> predicate, int startIndex = -1)
        {
            for (int i = Math.Max(startIndex, 0); i < se.Length; i++)
            {
                if (predicate(se[i]))
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Finds the last index of the specified character.
        /// </summary>
        /// <param name="c">The character to find.</param>
        /// <param name="startIndex">The starting index. Use -1 to start at the end of the string.</param>
        /// <param name="ignoreCase">Set to true for case-insensitive search.</param>
        /// <returns>The index of the first match, or -1 if no match was found.</returns>
        public static int LastIndexOf(this StringEditor se, char c, int startIndex = -1, bool ignoreCase = false)
        {
            IComparer<char> comparer = CharComparer.GetComparer(ignoreCase);

            if (startIndex < 0)
                startIndex = se.Length - 1;

            for (int i = startIndex; i >= 0; i--)
            {
                if (comparer.Compare(c, se[i]) == 0)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Find the last index of the specified string.
        /// </summary>
        /// <param name="s">The string to find.</param>
        /// <param name="startIndex">The starting index. Use -1 to start at the end of the string.</param>
        /// <param name="ignoreCase">Set to true for case-insensitive search.</param>
        /// <returns>The index of the first match, or -1 if no match was found.</returns>
        public static int LastIndexOf(this StringEditor se, string s, int startIndex = -1, bool ignoreCase = false)
        {
            IComparer<char> comparer = CharComparer.GetComparer(ignoreCase);

            if (startIndex < 0)
                startIndex = se.Length - 1;

            for (int i = startIndex - (s.Length - 1); i >= 0; i--)
            {
                if (se.MatchesAt(s, i, comparer))
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Returns the index of the last character that causes <paramref name="predicate"/> to return true,
        /// or -1 if no match not found.
        /// </summary>
        public static int LastIndexOf(this StringEditor se, Func<char, bool> predicate, int startIndex = -1)
        {
            if (startIndex < 0)
                startIndex = se.Length - 1;

            for (int i = startIndex; i >= 0; i--)
            {
                if (predicate(se[i]))
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Determines if the characters at the specified index match the specified string.
        /// </summary>
        /// <param name="index">The index to test at.</param>
        /// <param name="s">The string compare to.</param>
        /// <param name="comparer">The <see cref="IComparer{char}"/> to compare characters.</param>
        /// <returns>True if the characters match, false otherwise.</returns>
        public static bool MatchesAt(this StringEditor se, string s, int index, IComparer<char> comparer)
        {
            // Abort if no room for string
            if (se.Length - index < s.Length)
                return false;

            for (int i = 0; i < s.Length; i++)
            {
                if (comparer.Compare(s[i], se[index + i]) != 0)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Determines if the characters at the specified index match the specified string.
        /// </summary>
        /// <param name="index">The index to test at.</param>
        /// <param name="s">The string compare to.</param>
        /// <param name="ignoreCase">True if characters should be matched using case-insensitive compariso.</param>
        /// <returns>True if the characters match, false otherwise.</returns>
        public static bool MatchesAt(this StringEditor se, string s, int index, bool ignoreCase = false)
        {
            IComparer<char> comparer = CharComparer.GetComparer(ignoreCase);
            return se.MatchesAt(s, index, comparer);
        }

        public static bool MatchesEndingAt(this StringEditor se, string s, int index, IComparer<char> comparer)
        {
            Debug.Assert(s != null && s.Length > 0);

            index -= s.Length - 1;
            if (index < 0)
                return false;

            return se.MatchesAt(s, index, comparer);
        }

        /// <summary>
        /// Compares the given string 
        /// </summary>
        public static bool MatchesEndingAt(this StringEditor se, string s, int index, bool ignoreCase = false)
        {
            Debug.Assert(s != null && s.Length > 0);

            index -= s.Length - 1;
            if (index < 0)
                return false;

            return se.MatchesAt(s, index, ignoreCase);
        }

        /// <summary>
        /// Returns true if this string contains the specified character.
        /// </summary>
        public static bool Contains(this StringEditor se, char c) => se.IndexOf(c) >= 0;

        /// <summary>
        /// Returns true if this string contains the specified string.
        /// </summary>
        public static bool Contains(this StringEditor se, string s) => se.IndexOf(s) >= 0;

        /// <summary>
        /// Returns true if any character causes <paramref name="predicate"/> to return true.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static bool Contains(this StringEditor se, Func<char, bool> predicate) => se.IndexOf(predicate) >= 0;
    }
}
