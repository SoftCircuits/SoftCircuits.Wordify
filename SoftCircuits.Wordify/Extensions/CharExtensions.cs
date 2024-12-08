// Copyright (c) 2023-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using SoftCircuits.Wordify.Helpers;

namespace SoftCircuits.Wordify.Extensions
{
    internal static class CharExtensions
    {
        private static readonly HashSet<char> Vowels = new("aeiou",
            CharEqualityComparer.CaseInsensitive);
        private static readonly HashSet<char> Consonants = new("bcdfghjklmnpqrstvwxyz",
            CharEqualityComparer.CaseInsensitive);

        public static bool IsVowel(this char c) => Vowels.Contains(c);

        public static bool IsConsonant(this char c) => Consonants.Contains(c);
    }
}
