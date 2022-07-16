// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using System.Diagnostics.CodeAnalysis;

namespace SoftCircuits.Wordify.Helpers
{
    internal class CaseSensitiveEqualityComparer : IEqualityComparer<char>
    {
        public bool Equals(char c1, char c2)
        {
            return c1.Equals(c2);
        }

        public int GetHashCode([DisallowNull] char obj)
        {
            return obj.GetHashCode();
        }
    }

    internal class CaseInsensitiveEqualityComparer : IEqualityComparer<char>
    {
        public bool Equals(char c1, char c2)
        {
            return char.ToUpper(c1).Equals(char.ToUpper(c2));
        }

        public int GetHashCode([DisallowNull] char obj)
        {
            return char.ToUpper(obj).GetHashCode();
        }
    }

    internal class CaseSensitiveComparer : IComparer<char>
    {
        public int Compare(char c1, char c2)
        {
            return c1.CompareTo(c2);
        }
    }

    internal class CaseInsensitiveComparer : IComparer<char>
    {
        public int Compare(char c1, char c2)
        {
            return char.ToUpper(c1).CompareTo(char.ToUpper(c2));
        }
    }

    internal static class CharComparer
    {
        public static IEqualityComparer<char> GetEqualityComparer(bool ignoreCase) => ignoreCase ?
            new CaseInsensitiveEqualityComparer() :
            new CaseSensitiveEqualityComparer();

        public static IComparer<char> GetComparer(bool ignoreCase) => ignoreCase ?
            new CaseInsensitiveComparer() :
            new CaseSensitiveComparer();
    }
}
