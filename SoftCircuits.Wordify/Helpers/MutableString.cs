// Copyright (c) 2023-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("WordifyTests")]

namespace SoftCircuits.Wordify.Helpers
{
    /// <summary>
    /// Class for editing strings.
    /// </summary>
    internal partial class MutableString
    {
        /// <summary>
        /// Array to hold the string characters.
        /// </summary>
        private char[] CharArray;

        /// <summary>
        /// Gets the current string length.
        /// </summary>
        public int Length { get; private set; }

        /// <summary>
        /// Constructs a new <see cref="MutableString"/> instance.
        /// </summary>
        public MutableString()
        {
            Resize(0);
        }

        /// <summary>
        /// Constructs a new <see cref="MutableString"/> instance.
        /// </summary>
        /// <param name="s">Initial string value.</param>
        public MutableString(string? s)
        {
            Reset(s);
        }

        #region Primitives

        /// <summary>
        /// Resets this object with a new string. Any previous string is lost.
        /// </summary>
        /// <param name="s">New string value.</param>
        [MemberNotNull(nameof(CharArray))]
        public void Reset(string? s)
        {
            s ??= string.Empty;
            Resize(s.Length);
            Copy(s, 0);
        }

        /// <summary>
        /// Resizes this <see cref="MutableString"/> object.
        /// </summary>
        /// <param name="length">Specifies the new string length.</param>
        [MemberNotNull(nameof(CharArray))]
        public void Resize(int length)
        {
            if (CharArray == null || CharArray.Length < length)
            {
                // To minimize the number of reallocations, double requested size
                Array.Resize(ref CharArray, Math.Max(length * 2, 64));
            }
            Length = length;
        }

        /// <summary>
        /// Gets or sets the character at the specified index.
        /// </summary>
        public char this[int index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => CharArray[index];
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => CharArray[index] = value;
        }

        /// <summary>
        /// Gets or sets the character at the specified index.
        /// </summary>
        public char this[Index index]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => CharArray[index.GetOffset(Length)];
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            set => CharArray[index.GetOffset(Length)] = value;
        }

        /// <summary>
        /// Returns the specified range of this string.
        /// </summary>
        public string this[Range range]
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                (int offset, int length) = range.GetOffsetAndLength(Length);
                return new string(CharArray, offset, length);
            }
        }

        /// <summary>
        /// Returns the current value as a regular <see cref="String"/>.
        /// </summary>
        public override string ToString() => new(CharArray, 0, Length);

        #endregion

        #region String Modification

        /// <summary>
        /// Appends the specified string to this object.
        /// </summary>
        /// <param name="s"></param>
        public void Append(string s)
        {
            if (s == null || s.Length == 0)
                return;

            // Resize array
            int oldLength = Length;
            Resize(oldLength + s.Length);

            // Copy string
            Copy(s, oldLength);
        }

        /// <summary>
        /// Inserts the specified string at the specified index.
        /// </summary>
        /// <param name="index">The index where the string should be inserted.</param>
        /// <param name="s">The string to insert.</param>
        public void Insert(int index, string s)
        {
            if (s == null || s.Length == 0)
                return;

            // Ensure valid index
            int oldLength = Length;
            if (index > oldLength)
                index = oldLength;

            // Resize array
            Resize(oldLength + s.Length);

            // Shift characters to make room
            if (index < oldLength)
                Move(index, index + s.Length, oldLength - index);

            // Copy string
            Copy(s, index);
        }

        /// <summary>
        /// Inserts the specified string at the specified index, replacing the specified number of characters.
        /// </summary>
        /// <param name="index">The index where the string should be inserted.</param>
        /// <param name="s">The string to insert.</param>
        /// <param name="replaceCount">The number of characters to replace.</param>
        public void Replace(int index, string s, int replaceCount)
        {
            if (s == null || s.Length == 0)
            {
                Delete(index, replaceCount);
                return;
            }

            // Ensure valid index
            int oldLength = Length;
            if (index > oldLength)
                index = oldLength;

            // Ensure valid replacement character count
            if (replaceCount < 0)
                replaceCount = 0;
            else if (replaceCount > oldLength - index)
                replaceCount = oldLength - index;

            // Determine if string grows or shrinks
            int delta = s.Length - replaceCount;
            if (delta > 0)
            {
                // Grow array
                Resize(oldLength + delta);

                // Shift characters
                if (index + s.Length < Length)
                    Move(index, index + delta, oldLength - index);
            }
            else if (delta < 0)
            {
                // Shift characters
                if (index + s.Length < Length)
                {
                    int offset = index + replaceCount;
                    Move(offset, offset + delta, oldLength - index - replaceCount);
                }

                // Shrink array
                Resize(oldLength + delta);
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
            int oldLength = Length;
            if (index >= oldLength || count <= 0)
                return;

            int maxCount = oldLength - index;
            if (count > maxCount)
                count = maxCount;

            // Shift characters
            if (index + count < oldLength)
                Move(index + count, index, oldLength - index - count);

            // Resize array
            Resize(oldLength - count);
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
            if (sourceIndex > targetIndex)
            {
                for (int i = 0; i < count; i++)
                    CharArray[targetIndex + i] = CharArray[sourceIndex + i];
            }
            else if (sourceIndex < targetIndex)
            {
                for (int i = count - 1; i >= 0; i--)
                    CharArray[targetIndex + i] = CharArray[sourceIndex + i];
            }
        }

        /// <summary>
        /// Copies the given string to this <see cref="MutableString"/> object at the specified index.
        /// Does not grow the string.
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

            Debug.Assert(targetIndex + count <= Length);

            // Copy characters
            for (int i = 0; i < count; i++)
                CharArray[targetIndex + i] = s[i];
        }

        #endregion

        #region Operators

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static implicit operator string(MutableString se) => se.ToString();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static explicit operator MutableString(string s) => new(s);

        #endregion

    }
}
