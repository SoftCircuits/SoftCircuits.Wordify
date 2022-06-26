// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.Wordify.Helpers
{
    internal static class CharExtensions
    {
        private static readonly string Vowels = "aeiou";
        private static readonly string Consonants = "bcdfghjklmnpqrstvwxyz";

        public static bool IsVowel(this char c) => Vowels.Contains(char.ToLower(c));

        public static bool IsConsonant(this char c) => Consonants.Contains(char.ToLower(c));
    }
}
