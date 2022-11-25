// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using SoftCircuits.Wordify.Helpers;

namespace SoftCircuits.Wordify
{
    public static partial class WordifyExtensions
    {
        /// <summary>
        /// Converts a number to ordinal words.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The resulting ordinal string.</returns>
        public static string MakeOrdinal(this int value) => MakeOrdinal(Wordify(value));

        /// <summary>
        /// Converts a number to ordinal words.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The resulting ordinal string.</returns>
        public static string MakeOrdinal(this long value) => MakeOrdinal(Wordify(value));

        /// <summary>
        /// Converts a number to ordinal words.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The resulting ordinal string.</returns>
        public static string MakeOrdinal(this uint value) => MakeOrdinal(Wordify(value));

        /// <summary>
        /// Converts a number to ordinal words.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The resulting ordinal string.</returns>
        public static string MakeOrdinal(this ulong value) => MakeOrdinal(Wordify(value));

        /// <summary>
        /// Converts a number into an ordinal number string.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="format">Format string used to create the digits.</param>
        /// <returns>The ordinal string.</returns>
        public static string MakeOrdinalDigits(this int value, string format = "#,0") => MakeOrdinalDigits(value.ToString(format));

        /// <summary>
        /// Converts a number into an ordinal number string.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="format">Format string used to create the digits.</param>
        /// <returns>The ordinal string.</returns>
        public static string MakeOrdinalDigits(this long value, string format = "#,0") => MakeOrdinalDigits(value.ToString(format));

        /// <summary>
        /// Converts a number into an ordinal number string.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="format">Format string used to create the digits.</param>
        /// <returns>The ordinal string.</returns>
        public static string MakeOrdinalDigits(this uint value, string format = "#,0") => MakeOrdinalDigits(value.ToString(format));

        /// <summary>
        /// Converts a number into an ordinal number string.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="format">Format string used to create the digits.</param>
        /// <returns>The ordinal string.</returns>
        public static string MakeOrdinalDigits(this ulong value, string format = "#,0") => MakeOrdinalDigits(value.ToString(format));

        /// <summary>
        /// Converts a number into an ordinal number string.
        /// </summary>
        /// <param name="s">A string with numeric digits.</param>
        /// <returns>The ordinal string.</returns>
        public static string MakeOrdinalDigits(this string? s)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;

            MutableString editor = new(s);
            int lastDigitIndex = editor.LastIndexOf(char.IsDigit);
            if (lastDigitIndex < 0)
                return s;   // No digits

            char lastDigit = editor[lastDigitIndex];
            char prevDigit = (lastDigitIndex > 0) ? editor[lastDigitIndex - 1] : '\0';
            string suffix;

            if (lastDigit == '1' && prevDigit != '1')
                suffix = "st";
            else if (lastDigit == '2' && prevDigit != '1')
                suffix = "nd";
            else if (lastDigit == '3' && prevDigit != '1')
                suffix = "rd";
            else
                suffix = "th";

            editor.Insert(lastDigitIndex + 1, suffix);
            return editor;
        }

        #region Private Methods

        /// <summary>
        /// Numbers with different words for ordinal version.
        /// </summary>
        private static readonly Dictionary<string, string> OrdinalReplacementLookup = new(StringComparer.OrdinalIgnoreCase)
        {
            ["one"] = "first",
            ["two"] = "second",
            ["three"] = "third",
            ["five"] = "fifth",
            ["eight"] = "eighth",
            ["nine"] = "ninth",
            ["twelve"] = "twelfth",
        };

        /// <summary>
        /// Converts a string of numbers, expressed as word, to an ordinal version.
        /// </summary>
        /// <remarks>
        /// We might want to make this public but it will ignore strings with just digits.
        /// Also, it doesnt test for non-number words (e.g. would convert boy to boieth).
        /// </remarks>
        /// <param name="s"></param>
        /// <returns></returns>
        private static string MakeOrdinal(this string? s)
        {
            if (s == null)
                return string.Empty;

            MutableString editor = new(s);

            // Find last word
            if (!editor.FindLastWord(out int startIndex, out int endIndex))
                return s;   // No words

            string lastWord = editor[startIndex..endIndex];

            if (OrdinalReplacementLookup.TryGetValue(lastWord, out string? replacement))
            {
                editor.Replace(startIndex, replacement, lastWord.Length);
            }
            else if (lastWord[^1] == 'y')
            {
                editor.Replace(endIndex - 1, "ieth", 1);
            }
            else
            {
                editor.Insert(endIndex, "th");
            }

            return editor;
        }

        #endregion

    }
}
