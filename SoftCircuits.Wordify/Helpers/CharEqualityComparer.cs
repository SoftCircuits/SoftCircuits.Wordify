// Copyright (c) 2023-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using System.Diagnostics.CodeAnalysis;

namespace SoftCircuits.Wordify.Helpers
{
    internal class CaseSensitiveEqualityComparer : IEqualityComparer<char>
    {
        public static readonly CaseSensitiveEqualityComparer Instance = new();

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
        public static readonly CaseInsensitiveEqualityComparer Instance = new();

        public bool Equals(char c1, char c2)
        {
            return char.ToUpper(c1).Equals(char.ToUpper(c2));
        }

        public int GetHashCode([DisallowNull] char obj)
        {
            return char.ToUpper(obj).GetHashCode();
        }
    }

    internal static class CharEqualityComparer
    {
        public static IEqualityComparer<char> CaseSensitive => CaseSensitiveEqualityComparer.Instance;
        public static IEqualityComparer<char> CaseInsensitive => CaseInsensitiveEqualityComparer.Instance;

        public static IEqualityComparer<char> GetComparer(bool ignoreCase) => ignoreCase ?
            CaseInsensitive :
            CaseSensitive;
    }
}
