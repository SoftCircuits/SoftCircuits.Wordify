// Copyright (c) 2023-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.Wordify.Helpers
{
    internal class CaseSensitiveComparer : IComparer<char>
    {
        public static readonly CaseSensitiveComparer Instance = new();

        public int Compare(char c1, char c2)
        {
            return c1.CompareTo(c2);
        }
    }

    internal class CaseInsensitiveComparer : IComparer<char>
    {
        public static readonly CaseInsensitiveComparer Instance = new();

        public int Compare(char c1, char c2)
        {
            return char.ToUpper(c1).CompareTo(char.ToUpper(c2));
        }
    }

    internal static class CharComparer
    {
        public static IComparer<char> CaseSensitive => CaseSensitiveComparer.Instance;
        public static IComparer<char> CaseInsensitive => CaseInsensitiveComparer.Instance;

        public static IComparer<char> GetComparer(bool ignoreCase) => ignoreCase ?
            CaseInsensitive :
            CaseSensitive;
    }
}
