// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("WordifyTests")]

namespace SoftCircuits.Wordify
{
    /// <summary>
    /// Class that makes it easy to edit a string. Exposes the string as a writable array and includes
    /// methods for inserting, deleting, copying, etc. characters.
    /// </summary>
    internal class StringEditor
    {
        private readonly string Original;

        /// <summary>
        /// Constructs a new <see cref="StringEditor"/> instance.
        /// </summary>
        /// <param name="s">Initial string value.</param>
        public StringEditor(string? s)
        {
            Original = s ?? string.Empty;
            InternalArray = null;
            Length = Original.Length;
        }

        /// <summary>
        /// Appends the specified string to this object.
        /// </summary>
        /// <param name="s"></param>
        public void Append(string s)
        {
            if (string.IsNullOrEmpty(s))
                return;

            // Resize array
            int length = Length;
            Resize(length + s.Length);

            // Copy string
            Copy(s, length);
        }

        /// <summary>
        /// Inserts the specified string at the specified index.
        /// </summary>
        /// <param name="index">The index where the string should be inserted.</param>
        /// <param name="s">The string to insert.</param>
        public void Insert(int index, string s)
        {
            if (string.IsNullOrEmpty(s))
                return;

            // Ensure valid index
            int length = Length;
            if (index > length)
                index = length;

            // Resize array
            Resize(length + s.Length);

            // Shift characters
            if (index < length)
                Move(index, index + s.Length, length - index);

            // Copy string
            Copy(s, index);
        }

        /// <summary>
        /// Inserts the specified string at the specified index, replacing the specified number of characters.
        /// </summary>
        /// <param name="index">The index where the string should be inserted.</param>
        /// <param name="s">The string to insert.</param>
        /// <param name="replaceCount">The number of characters to replace.</param>
        public void Insert(int index, string s, int replaceCount)
        {
            if (string.IsNullOrEmpty(s))
                return;

            // Ensure valid index
            int length = Length;
            if (index > length)
                index = length;

            // Ensure valid replacement character count
            if (replaceCount < 0)
                replaceCount = 0;
            else if (replaceCount > length - index)
                replaceCount = length - index;

            // Determine if string grows or shrinks
            int delta = s.Length - replaceCount;
            if (delta > 0)
            {
                // Grow array
                Resize(length + delta);

                // Shift characters
                if (index + s.Length < Length)
                    Move(index, index + delta, length - index);
            }
            else if (delta < 0)
            {
                // Shift characters
                if (index + s.Length < Length)
                {
                    int offset = index + replaceCount;
                    Move(offset, offset + delta, length - index - replaceCount);
                }

                // Shrink array
                Resize(length + delta);
            }

            // Copy string
            Copy(s, index);
        }

        /// <summary>
        /// Deletes the specified number of characters at the specified index.
        /// </summary>
        /// <param name="index">The starting index where characters should be deleted.</param>
        /// <param name="count">The number of characters to delete.</param>
        public void Delete(int index, int count)
        {
            int length = Length;
            if (index >= length || count <= 0)
                return;

            int maxCount = length - index;
            if (count > maxCount)
                count = maxCount;

            // Shift characters
            Move(index + count, index, length - index - count);

            // Resize array
            Resize(length - count);
        }

        /// <summary>
        /// Copies characters from one part of the array to another.
        /// </summary>
        /// <param name="sourceIndex">Starting index of where characters are copied from.</param>
        /// <param name="targetIndex">Starting index of where characters are copied to.</param>
        /// <param name="count">The number of characters to copy.</param>
        public void Move(int sourceIndex, int targetIndex, int count)
        {
            int maxCount = Length - Math.Max(sourceIndex, targetIndex);
            if (count > maxCount)
                count = maxCount;

            if (count <= 0)
                return;

            // Copy characters
            char[] array = Array;
            if (sourceIndex > targetIndex)
            {
                for (int i = 0; i < count; i++)
                    array[targetIndex + i] = array[sourceIndex + i];
            }
            else if (sourceIndex < targetIndex)
            {
                for (int i = count - 1; i >= 0; i--)
                    array[targetIndex + i] = array[sourceIndex + i];
            }
        }

        /// <summary>
        /// Copies the given string to this <see cref="StringEditor"/> object, starting at the specified index.
        /// </summary>
        /// <param name="s">The string to copy.</param>
        /// <param name="targetIndex">The starting index where the string should be copied.</param>
        public void Copy(string s, int targetIndex)
        {
            int count = s.Length;
            if (count > Length - targetIndex)
                count = Length - targetIndex;

            if (count <= 0)
                return;

            // Copy characters
            char[] array = Array;
            for (int i = 0; i < count; i++)
                array[targetIndex + i] = s[i];
        }

        /// <summary>
        /// Converts this object to a string.
        /// </summary>
        public override string ToString() => (InternalArray != null) ? new(InternalArray, 0, Length) : Original;

        public string Substring(int startIndex, int length) => (InternalArray != null) ?
            new string(InternalArray, startIndex, length) :
            Original.Substring(startIndex, length);

        #region Character Access

        /// <summary>
        /// Gets or sets the character at the specified index.
        /// </summary>
        public char this[int index]
        {
            get => GetAt(index);
            set => SetAt(index, value);
        }

        public char this[Index index]
        {
            get => GetAt(index.GetOffset(Length));
            set => SetAt(index.GetOffset(Length), value);
        }

        public string this[Range range]
        {
            get
            {
                int startIndex = range.Start.GetOffset(Length);
                int length = range.End.GetOffset(Length) - startIndex;
                return Substring(startIndex, length);
            }
        }

        private char GetAt(int index) => (InternalArray != null) ? InternalArray[index] : Original[index];

        private void SetAt(int index, char c) => Array[index] = c;

        #endregion

        #region IndexOf / Contains

        /// <summary>
        /// Returns the index of the first character that matches <paramref name="c"/>, or -1 if no match was found.
        /// </summary>
        public int IndexOf(char c, int startIndex = -1)
        {
            if (startIndex < 0)
                startIndex = 0;

            for (int i = startIndex; i < Length; i++)
            {
                if (GetAt(i) == c)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Returns the index of the first character sequence that matches <paramref name="s"/>, or -1 if no match was found.
        /// </summary>
        public int IndexOf(string s, int startIndex = -1)
        {
            if (startIndex < 0)
                startIndex = 0;

            for (int i = startIndex; i < Length - (s.Length - 1); i++)
            {
                int j = 0;
                for (; j < s.Length; j++)
                {
                    if (GetAt(i + j) != s[j])
                        break;
                }
                if (j == s.Length)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Returns the index of the first character that causes <paramref name="predicate"/> to return true,
        /// or -1 if no match not found.
        /// </summary>
        public int IndexOf(Func<char, bool> predicate, int startIndex = -1)
        {
            if (startIndex < 0)
                startIndex = 0;

            for (int i = startIndex; i < Length; i++)
            {
                if (predicate(GetAt(i)))
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Returns the index of the last character that matches <paramref name="c"/>, or -1 if no match was found.
        /// </summary>
        public int LastIndexOf(char c, int startIndex = -1)
        {
            if (startIndex < 0)
                startIndex = Length - 1;

            for (int i = startIndex; i >= 0; i--)
            {
                if (GetAt(i) == c)
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Returns the index of the last character sequence that matches <paramref name="s"/>, or -1 if no match was found.
        /// </summary>
        public int LastIndexOf(string s, int startIndex = -1)
        {
            if (startIndex < 0)
                startIndex = Length - 1;

            for (int i = startIndex - (s.Length - 1); i >= 0; i--)
            {
                int j = 0;
                for (; j < s.Length; j++)
                {
                    if (GetAt(i + j) != s[j])
                        break;
                }
                if (j == s.Length)
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
                if (predicate(GetAt(i)))
                    return i;
            }
            return -1;
        }

        /// <summary>
        /// Returns true if this string contains the specified character.
        /// </summary>
        public bool Contains(char c) => IndexOf(c) >= 0;

        /// <summary>
        /// Returns true if this string contains the specified string.
        /// </summary>
        public bool Contains(string s) => IndexOf(s) >= 0;

        /// <summary>
        /// Returns true if any character causes <paramref name="predicate"/> to return true.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public bool Contains(Func<char, bool> predicate) => IndexOf(predicate) >= 0;

        #endregion

        #region Find First/Last Word / Matching

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


        //public bool MatchesAt(int index, string s, bool ignoreCase)
        //{

        //}

        public bool MatchesEndingAt(int index, string s, bool ignoreCase = false)
        {
            Debug.Assert(s != null && s.Length > 0);

            index -= s.Length - 1;
            if (index < 0)
                return false;

            if (ignoreCase)
            {
                for (int i = 0; i < s.Length; i++)
                {
                    if (char.ToLower(GetAt(index + i)) != char.ToLower(s[i]))
                        return false;
                }
            }
            else
            {
                for (int i = 0; i < s.Length; i++)
                {
                    if (GetAt(index + i) != s[i])
                        return false;
                }
            }
            return true;
        }

        #endregion

        #region Character classification

        public bool IsWordCharacter(int pos)
        {
            Debug.Assert(pos < Length);
            char c = GetAt(pos);
            return char.IsLetterOrDigit(c) ||
                c == '\'' ||
                (c == '.' && pos < Length - 1 && char.IsDigit(GetAt(pos + 1)));
        }

        public bool IsEndOfSentenceCharacter(int pos)
        {
            Debug.Assert(pos < Length);
            char c = GetAt(pos);
            return c == '!' ||
                c == '?' ||
                c == ':' ||
                (c == '.' && !(pos < (Length - 1) && char.IsDigit(GetAt(pos + 1))));
        }

        #endregion

        #region Character array primatives

        /// <summary>
        /// Array growth granularity.
        /// </summary>
        private const int GrowBy = 16;

        /// <summary>
        /// Internal character array.
        /// </summary>
        private char[]? InternalArray = null;

        /// <summary>
        /// Internal character array size.
        /// </summary>
        private int InternalSize;

        /// <summary>
        /// Gets the current length of this object's string.
        /// </summary>
        public int Length { get; private set; }

        /// <summary>
        /// Returns a modifiable array with the characters from this string.
        /// </summary>
        public char[] Array
        {
            get
            {
                if (InternalArray == null)
                    Resize(Original.Length);
                Debug.Assert(InternalArray != null);
                return InternalArray;
            }
        }

        /// <summary>
        /// Resizes this <see cref="StringEditor"/> object. Initial resize copies characters
        /// from original string.
        /// </summary>
        /// <param name="size">Specifies the new size.</param>
        [MemberNotNull(nameof(InternalArray))]
        public void Resize(int size)
        {
            if (InternalArray == null)
            {
                InternalSize = RoundUpSize(size);
                InternalArray = new char[InternalSize];
                int minSize = Math.Min(InternalSize, Original.Length);
                for (int i = 0; i < minSize; i++)
                    InternalArray[i] = Original[i];
            }
            else if (size > InternalSize)
            {
                InternalSize = RoundUpSize(size);
                System.Array.Resize(ref InternalArray, InternalSize);
            }
            Length = size;
        }

        /// <summary>
        /// Rounds up the given size to the nearest multiple of <see cref="GrowBy"/>.
        /// </summary>
        private static int RoundUpSize(int size)
        {
            int mod = size % GrowBy;
            if (mod != 0)
                size = size + GrowBy - mod;
            return size;
        }

        #endregion

        #region Operators

        public static implicit operator string(StringEditor se) => se.ToString();
        public static explicit operator StringEditor(string s) => new(s);

        #endregion

    }
}
