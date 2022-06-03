namespace SoftCircuits.Wordify
{
    internal static class CharExtensions
    {
        private static readonly string Vowels = "aeiou";
        private static readonly string Consonants = "bcdfghjklmnpqrstvwxyz";

        public static bool IsVowel(this char c) => Vowels.Contains(char.ToLower(c));

        public static bool IsConsonant(this char c) => Consonants.Contains(char.ToLower(c));
    }
}
