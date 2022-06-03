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
            Debug.Assert(s != null);

            if (s.Length == 0)
                return;

            // Resize array
            int length = Length;
            Resize(length + s.Length);

            // Copy string
            Copy(s, length);
        }

        /// <summary>
        /// Deletes the specified number of characters at the specified index.
        /// </summary>
        /// <param name="index">The starting index where characters should be deleted.</param>
        /// <param name="count">The number of characters to delete.</param>
        public void Delete(int index, int count)
        {
            // Ensure valid index
            int length = Length;
            if (index > length)
            {
                Debug.Assert(false);
                return;
            }

            if (count > length - index)
                count = length - index;

            // Shift characters
            Copy(index + count, index, length - index - count);

            // Resize array
            Resize(length - count);
        }

        /// <summary>
        /// Inserts the specified string at the specified index.
        /// </summary>
        /// <param name="index">The index where the string should be inserted.</param>
        /// <param name="s">The string to insert.</param>
        public void Insert(int index, string s)
        {
            Debug.Assert(s != null);

            if (s.Length == 0)
                return;

            // Ensure valid index
            int length = Length;
            if (index > length)
                index = length;

            // Resize array
            Resize(length + s.Length);

            // Shift characters
            if (index + s.Length < Length)
                Copy(index, index + s.Length, length - index);

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
            Debug.Assert(s != null);

            // Ensure valid index
            int length = Length;
            if (index > length)
                index = length;

            // Ensure valid replaceChars
            if (replaceCount < 0)
                replaceCount = 0;
            else if (replaceCount > length - index)
                replaceCount = length - index;

            // Determine if string grows or shrinks
            int delta = s.Length - replaceCount;
            if (delta > 0)
            {
                // Resize array
                Resize(length + delta);

                // Shift characters
                if (index + s.Length < Length)
                    Copy(index, index + delta, length - index);
            }
            else if (delta < 0)
            {
                // Shift characters
                if (index + s.Length < Length)
                {
                    int offset = index + replaceCount;
                    Copy(offset, offset + delta, length - index - replaceCount);
                }

                // Resize array
                Resize(length + delta);
            }

            // Copy string
            Copy(s, index);
        }

        /// <summary>
        /// Copies characters from one part of the array to another.
        /// </summary>
        /// <param name="sourceIndex">Starting index of where characters are copied from.</param>
        /// <param name="targetIndex">Starting index of where characters are copied to.</param>
        /// <param name="count">The number of characters to copy.</param>
        public void Copy(int sourceIndex, int targetIndex, int count)
        {
            int maxCount = Length - Math.Min(sourceIndex, targetIndex);
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
                int start = range.Start.GetOffset(Length);
                return (InternalArray != null) ?
                    new(InternalArray, start, range.End.GetOffset(Length) - start) :
                    Original[range];
            }
        }

        private char GetAt(int index) => (InternalArray != null) ? InternalArray[index] : Original[index];

        private void SetAt(int index, char c) => Array[index] = c;

        #endregion

        #region IndexOf

        // TODO: Can these work without building the array?

        //public int IndexOf(char c, int startIndex = -1, int endIndex = -1)
        //{
        //    if (startIndex < 0)
        //        startIndex = 0;
        //    if (endIndex < 0)
        //        endIndex = Length;

        //    for (int i = startIndex; i < endIndex; i++)
        //    {
        //        if (this[i] == c)
        //            return i;
        //    }
        //    return -1;
        //}

        //public int IndexOf(string s, int startIndex = -1, int endIndex = -1)
        //{
        //    if (startIndex < 0)
        //        startIndex = 0;
        //    if (endIndex < 0)
        //        endIndex = Length - s.Length;

        //    for (int i = startIndex; i < endIndex; i++)
        //    {
        //        for (int j = 0; j < s.Length; j++)
        //        {
        //            if (s[j] != this[i])
        //                return i;
        //        }
        //    }
        //    return -1;
        //}

        public int IndexOf(Func<char, bool> predicate, int startIndex = -1, int endIndex = -1)
        {
            if (startIndex < 0)
                startIndex = 0;
            if (endIndex < 0)
                endIndex = Length;

            for (int i = startIndex; i < endIndex; i++)
            {
                if (predicate(this[i]))
                    return i;
            }
            return -1;
        }

        public int LastIndexOf(char c, int startIndex = -1, int endIndex = -1)
        {
            if (startIndex < 0)
                startIndex = 0;
            if (endIndex < 0)
                endIndex = Length - 1;

            for (int i = endIndex; i >= startIndex; i--)
            {
                if (this[i] == c)
                    return i;
            }
            return -1;
        }

        //public int LastIndexOf(string s, int startIndex = -1, int endIndex = -1)
        //{
        //    if (startIndex < 0)
        //        startIndex = 0;
        //    if (endIndex < 0)
        //        endIndex = Length - 1;

        //    for (int i = endIndex; i >= startIndex; i--)
        //    {
        //        for (int j = 0; j < s.Length; j++)
        //        {
        //            if (s[j] != this[i])
        //                return i;
        //        }
        //    }
        //    return -1;
        //}

        public int LastIndexOf(Func<char, bool> predicate, int startIndex = -1, int endIndex = -1)
        {
            if (startIndex < 0)
                startIndex = 0;
            if (endIndex < 0)
                endIndex = Length - 1;

            for (int i = endIndex; i >= startIndex; i--)
            {
                if (predicate(this[i]))
                    return i;
            }
            return -1;
        }

        //public bool FindFirstWord(out int startIndex, out int endIndex)
        //{

        //}

        public bool FindLastWord(out int startIndex, out int endIndex)
        {
            endIndex = LastIndexOf(char.IsLetter);
            if (endIndex >= 0)
            {
                startIndex = LastIndexOf(c => !char.IsLetter(c), 0, endIndex);
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


        public string Substring(int startIndex, int length)
        {
            if (InternalArray != null)
                return new string(InternalArray, startIndex, length);
            else
                return Original.Substring(startIndex, length);
        }

        //public bool MatchesAt(int index, string s, bool ignoreCase)
        //{

        //}

        public bool MatchesEndingAt(int index, string s, bool ignoreCase)
        {
            Debug.Assert(s != null || s.Length == 0);

            index -= s.Length - 1;
            if (index < 0)
                return false;

            char[] array = Array;
            if (ignoreCase)
            {
                for (int i = 0; i < s.Length; i++)
                {
                    if (char.ToLower(array[index + i]) != char.ToLower(s[i]))
                        return false;
                }
            }
            else
            {
                for (int i = 0; i < s.Length; i++)
                {
                    if (array[index + i] != s[i])
                        return false;
                }
            }
            return true;
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
                {
                    Resize(Original.Length);
                    for (int i = 0; i < Original.Length; i++)
                        InternalArray[i] = Original[i];
                }
                Debug.Assert(InternalArray != null);
                return InternalArray;
            }
        }

        /// <summary>
        /// Resizes this <see cref="StringEditor"/> object.
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
