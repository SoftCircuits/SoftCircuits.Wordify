// Copyright (c) 2023-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace SoftCircuits.Wordify.Helpers
{
    /// <summary>
    /// Adds additional functionality to the <see cref="MutableString"/> class.
    /// </summary>
    internal partial class MutableString
    {
        public bool IsWordCharacter(int pos)
        {
            Debug.Assert(pos < Length);
            char c = CharArray[pos];
            return char.IsLetterOrDigit(c) || c == '\'' || (c == '.' && pos < Length - 1 && char.IsDigit(CharArray[pos + 1]));
        }

        public bool IsEndOfSentenceCharacter(int pos)
        {
            Debug.Assert(pos < Length);
            char c = CharArray[pos];
            return c == '!' || c == '?' || c == ':' || (c == '.' && !(pos < (Length - 1) && char.IsDigit(CharArray[pos + 1])));
        }

        /// <summary>
        /// Gets the start and end index of the first contiguous sequence of letters.
        /// </summary>
        public bool FindFirstWord(out int startIndex, out int endIndex)
        {
            startIndex = IndexOf(char.IsLetter);
            if (startIndex >= 0)
            {
                endIndex = IndexOf(c => !char.IsLetter(c), startIndex + 1);
                if (endIndex < 0)
                    endIndex = Length;
                return true;
            }
            endIndex = -1;
            return false;
        }

        /// <summary>
        /// Gets the start and end index of the last contiguous sequence of letters.
        /// </summary>
        public bool FindLastWord(out int startIndex, out int endIndex)
        {
            endIndex = LastIndexOf(char.IsLetter);
            if (endIndex >= 0)
            {
                startIndex = LastIndexOf(c => !char.IsLetter(c), endIndex - 1);
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
        public int IndexOf(char c, int startIndex = -1, bool ignoreCase = false)
        {
            IComparer<char> comparer = CharComparer.GetComparer(ignoreCase);

            for (int i = Math.Max(startIndex, 0); i < Length; i++)
            {
                if (comparer.Compare(c, CharArray[i]) == 0)
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
        public int IndexOf(string s, int startIndex = -1, bool ignoreCase = false)
        {
            IComparer<char> comparer = CharComparer.GetComparer(ignoreCase);

            for (int i = Math.Max(startIndex, 0); i < Length - (s.Length - 1); i++)
            {
                if (MatchesAt(s, i, comparer))
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
        public int IndexOf(Func<char, bool> predicate, int startIndex = -1)
        {
            for (int i = Math.Max(startIndex, 0); i < Length; i++)
            {
                if (predicate(CharArray[i]))
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
        public int LastIndexOf(char c, int startIndex = -1, bool ignoreCase = false)
        {
            IComparer<char> comparer = CharComparer.GetComparer(ignoreCase);

            if (startIndex < 0)
                startIndex = Length - 1;

            for (int i = startIndex; i >= 0; i--)
            {
                if (comparer.Compare(c, CharArray[i]) == 0)
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
        public int LastIndexOf(string s, int startIndex = -1, bool ignoreCase = false)
        {
            IComparer<char> comparer = CharComparer.GetComparer(ignoreCase);

            if (startIndex < 0)
                startIndex = Length - 1;

            for (int i = startIndex - (s.Length - 1); i >= 0; i--)
            {
                if (MatchesAt(s, i, comparer))
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Returns the index of the last character that causes <paramref name="predicate"/> to return true,
        /// or -1 if no match not found.
        /// </summary>
        public int LastIndexOf(Func<char, bool> predicate, int startIndex = -1)
        {
            if (startIndex < 0)
                startIndex = Length - 1;

            for (int i = startIndex; i >= 0; i--)
            {
                if (predicate(CharArray[i]))
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Determines if the characters at the specified index match the specified string.
        /// </summary>
        /// <param name="index">The index to test at.</param>
        /// <param name="s">The string compare to.</param>
        /// <returns>True if the characters match, false otherwise.</returns>
        public bool MatchesAt(string s, int index)
        {
            // Abort if no room for string
            if (Length - index < s.Length)
                return false;

            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] != CharArray[index + i])
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Determines if the characters at the specified index match the specified string.
        /// </summary>
        /// <param name="index">The index to test at.</param>
        /// <param name="s">The string compare to.</param>
        /// <param name="comparer">The <see cref="IComparer{T}"/> to compare characters.</param>
        /// <returns>True if the characters match, false otherwise.</returns>
        public bool MatchesAt(string s, int index, IComparer<char> comparer)
        {
            // Abort if no room for string
            if (Length - index < s.Length)
                return false;

            for (int i = 0; i < s.Length; i++)
            {
                if (comparer.Compare(s[i], CharArray[index + i]) != 0)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Determines if the characters at the specified index match the specified string.
        /// </summary>
        /// <param name="index">The index to test at.</param>
        /// <param name="s">The string compare to.</param>
        /// <param name="ignoreCase">True if characters should be matched using case-insensitive comparison.</param>
        /// <returns>True if the characters match, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MatchesAt(string s, int index, bool ignoreCase = false) => MatchesAt(s, index, CharComparer.GetComparer(ignoreCase));

        /// <summary>
        /// Determines if the specified string matches the characters at the end of this string.
        /// </summary>
        /// <param name="s">The string to compare to.</param>
        /// <param name="index">The ending index of the characters to compare.</param>
        /// <param name="comparer">The <see cref="IComparer{T}"/> to compare characters.</param>
        /// <returns>True if the characters match, false otherwise.</returns>
        public bool MatchesEndingAt(string s, int index, IComparer<char> comparer)
        {
            Debug.Assert(s != null && s.Length > 0);

            index -= s.Length - 1;
            if (index < 0)
                return false;

            return MatchesAt(s, index, comparer);
        }

        /// <summary>
        /// Determines if the specified string matches the characters at the end of this string.
        /// </summary>
        /// <param name="s">The string to compare to.</param>
        /// <param name="index">The ending index of the characters to compare.</param>
        /// <param name="ignoreCase">True if characters should be matched using case-insensitive comparison.</param>
        /// <returns>True if the characters match, false otherwise.</returns>
        public bool MatchesEndingAt(string s, int index, bool ignoreCase = false)
        {
            Debug.Assert(s != null && s.Length > 0);

            index -= s.Length - 1;
            if (index < 0)
                return false;

            return MatchesAt(s, index, ignoreCase);
        }

        /// <summary>
        /// Returns true if this string contains the specified character.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(char c) => IndexOf(c) >= 0;

        /// <summary>
        /// Returns true if this string contains the specified string.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(string s) => IndexOf(s) >= 0;

        /// <summary>
        /// Returns true if any character causes <paramref name="predicate"/> to return true.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(Func<char, bool> predicate) => IndexOf(predicate) >= 0;
    }
}
