// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using System.Diagnostics.CodeAnalysis;

namespace SoftCircuits.Wordify.Helpers
{
    /// <summary>
    /// Helper class to return chunks of a string.
    /// </summary>
    internal class StringSlicer
    {
        private string String;
        private int Index;

        public StringSlicer(string s) => Reset(s);

        [MemberNotNull(nameof(String))]
        public void Reset(string s)
        {
            String = s;
            Index = 0;
        }

        public void Reset()
        {
            Index = 0;
        }

        /// <summary>
        /// Returns the number of characters remaining in this string.
        /// </summary>
        public int Remaining => String.Length - Index;

        /// <summary>
        /// Takes a chunk of this string.
        /// </summary>
        /// <param name="count">The number of characters to take.</param>
        public ReadOnlySpan<char> Slice(int count)
        {
            if (Remaining > 0 && count > 0)
            {
                if (count > Remaining)
                    count = Remaining;

                ReadOnlySpan<char> result = String.AsSpan(Index, count);

                Index += count;
                return result;
            }
            return ReadOnlySpan<char>.Empty;
        }
    }
}
