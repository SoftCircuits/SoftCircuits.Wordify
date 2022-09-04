// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("WordifyTests")]

namespace SoftCircuits.Wordify.Helpers
{
    /// <summary>
    /// Class that makes it easy to edit a string. Exposes the string as a writable array and includes
    /// methods for inserting, deleting, copying, etc. characters.
    /// </summary>
    internal partial class StringEditor
    {
        /// <summary>
        /// Constructs a new <see cref="StringEditor"/> instance.
        /// </summary>
        /// <param name="s">Initial string value.</param>
        public StringEditor(string? s)
        {
            Initialize(s);
        }

        #region Primatives

        /// <summary>
        /// Internal string. This is the initial value for this string. Once the string is modified, then the value for this string
        /// will be copied to <see cref="InternalArray"/>.
        /// </summary>
        private string InternalString;

        /// <summary>
        /// Gets the current length of this object's string.
        /// </summary>
        public int Length { get; private set; }

        /// <summary>
        /// Internal character array. Initially set to null. Holds the string value after this object has been modified.
        /// </summary>
        private char[]? InternalArray = null;

        /// <summary>
        /// Current size of internal array.
        /// </summary>
        private int InternalCapacity;

        // These functions change depending on whether or not the string has been modified.
        protected Func<int, char> GetAt;
        protected Action<int, char> SetAt;
        protected Func<string> GetString;
        protected Func<int, int, string> Substring;
        protected Func<char[]> GetArray;

        /// <summary>
        /// Indicates if the current string is in a modified state.
        /// </summary>
        [MemberNotNullWhen(true, nameof(InternalArray))]
        private bool IsModified => InternalArray != null;

        /// <summary>
        /// Initializes this object with a new string. Any changes to the current string are discarded.
        /// </summary>
        /// <param name="s">The new string value.</param>
        [MemberNotNull(nameof(InternalString))]
        [MemberNotNull(nameof(GetAt))]
        [MemberNotNull(nameof(SetAt))]
        [MemberNotNull(nameof(GetString))]
        [MemberNotNull(nameof(Substring))]
        [MemberNotNull(nameof(GetArray))]
        public void Initialize(string? s)
        {
            InternalString = s ?? string.Empty;
            Length = InternalString.Length;
            InternalArray = null;
            InternalCapacity = 0;
            SetUnmodifiedMode();
        }

        /// <summary>
        /// Resizes Resizes this <see cref="StringEditor"/> object. Initial resize copies characters
        /// from original string.
        /// </summary>
        /// <param name="length">Specifies the new length.</param>
        public void Resize(int length)
        {
            bool copyInternalString = false;

            if (InternalCapacity < length || InternalArray == null)
            {
                // To reduce the number of reallocations, we double the requested size
                InternalCapacity = Math.Max(InternalCapacity * 2, length);

                if (InternalArray == null)
                {
                    InternalArray = new char[InternalCapacity];
                    SetModifiedMode();
                    copyInternalString = true;
                }
                else Array.Resize(ref InternalArray, InternalCapacity);
            }

            Length = length;

            // Must do this after updating Length
            if (copyInternalString)
                Copy(InternalString, 0);
        }

        #region Unmodified String Functions

        [MemberNotNull(nameof(GetAt))]
        [MemberNotNull(nameof(SetAt))]
        [MemberNotNull(nameof(GetString))]
        [MemberNotNull(nameof(Substring))]
        [MemberNotNull(nameof(GetArray))]
        private void SetUnmodifiedMode()
        {
            Debug.Assert(!IsModified);
            GetAt = String_GetAt;
            SetAt = String_SetAt;
            GetString = String_GetString;
            Substring = String_Substring;
            GetArray = String_GetArray;
        }

        private char String_GetAt(int index)
        {
            Debug.Assert(!IsModified);
            return InternalString[index];
        }
        private void String_SetAt(int index, char value)
        {
            Debug.Assert(!IsModified);
            char[] array = GetArray();
            array[index] = value;
        }

        private string String_GetString()
        {
            Debug.Assert(!IsModified);
            return InternalString;
        }

        private string String_Substring(int offset, int length)
        {
            Debug.Assert(!IsModified);
            return InternalString.Substring(offset, length);
        }

        private char[] String_GetArray()
        {
            Debug.Assert(!IsModified);
            Resize(Length);
            Debug.Assert(IsModified);
            return InternalArray;
        }

        #endregion

        #region Modified String Functions

        [MemberNotNull(nameof(GetAt))]
        [MemberNotNull(nameof(SetAt))]
        [MemberNotNull(nameof(GetString))]
        [MemberNotNull(nameof(Substring))]
        [MemberNotNull(nameof(GetArray))]
        private void SetModifiedMode()
        {
            Debug.Assert(IsModified);
            GetAt = Array_GetAt;
            SetAt = Array_SetAt;
            GetString = Array_GetString;
            Substring = Array_Substring;
            GetArray = Array_GetArray;
        }

        private char Array_GetAt(int index)
        {
            Debug.Assert(IsModified);
            return InternalArray[index];
        }

        private void Array_SetAt(int index, char value)
        {
            Debug.Assert(IsModified);
            InternalArray[index] = value;
        }

        private string Array_GetString()
        {
            Debug.Assert(IsModified);
            return new(InternalArray, 0, Length);
        }

        private string Array_Substring(int offset, int length)
        {
            Debug.Assert(IsModified);
            return new(InternalArray, offset, length);
        }

        private char[] Array_GetArray()
        {
            Debug.Assert(IsModified);
            return InternalArray;
        }

        #endregion

        #endregion

        #region Content Access

        /// <summary>
        /// Gets or sets the character at the specified index.
        /// </summary>
        public char this[int index]
        {
            get => GetAt(index);
            set => SetAt(index, value);
        }

        /// <summary>
        /// Gets or sets the character at the specified index.
        /// </summary>
        public char this[Index index]
        {
            get => GetAt(index.GetOffset(Length));
            set => SetAt(index.GetOffset(Length), value);
        }

        /// <summary>
        /// Returns the specified range.
        /// </summary>
        public string this[Range range]
        {
            get
            {
                (int offset, int length) = range.GetOffsetAndLength(Length);
                return Substring(offset, length);
            }
        }

        /// <summary>
        /// Returns the current value as a regular <see cref="String"/>.
        /// </summary>
        public override string ToString() => GetString();

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

            // Shift characters
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
            char[] array = GetArray();
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
            if (targetIndex >= Length)
                return;

            int count = s.Length;
            if (count > Length - targetIndex)
                count = Length - targetIndex;

            if (count <= 0)
                return;

            // Copy characters
            char[] array = GetArray();
            for (int i = 0; i < count; i++)
                array[targetIndex + i] = s[i];
        }

        #endregion

        #region Operators

        public static implicit operator string(StringEditor se) => se.ToString();
        public static explicit operator StringEditor(string s) => new(s);

        #endregion

    }
}
