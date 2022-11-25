// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.Wordify.Extensions
{
    internal static class CharExtensions
    {
        private static readonly HashSet<char> Vowels = new("aeiou");
        private static readonly HashSet<char> Consonants = new("bcdfghjklmnpqrstvwxyz");

        public static bool IsVowel(this char c) => Vowels.Contains(char.ToLower(c));

        public static bool IsConsonant(this char c) => Consonants.Contains(char.ToLower(c));
    }
}
