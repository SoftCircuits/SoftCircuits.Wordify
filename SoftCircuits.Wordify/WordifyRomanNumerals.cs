// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using System.Text;

namespace SoftCircuits.Wordify
{
    public static partial class WordifyExtensions
    {
        private class RomanInfo
        {
            public int Value { get; set; }
            public string Numerals { get; set; }

            public RomanInfo(int value, string numerals)
            {
                Value = value;
                Numerals = numerals;
            }
        }

        private static readonly List<RomanInfo> RomanNumerals = new()
        {
            new RomanInfo(1000, "M"),
            new RomanInfo(900, "CM"),
            new RomanInfo(500, "D"),
            new RomanInfo(400, "CD"),
            new RomanInfo(100, "C"),
            new RomanInfo(90, "XC"),
            new RomanInfo(50, "L"),
            new RomanInfo(40, "XL"),
            new RomanInfo(10, "X"),
            new RomanInfo(9, "IX"),
            new RomanInfo(5, "V"),
            new RomanInfo(4, "IV"),
            new RomanInfo(1, "I"),
        };

        // TODO: Comments
        // TODO: How to handle overflow ???

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToRomanNumerals(this int value)
        {
            // Although Roman numerals don't generally include
            // zero, we use 'N' (for the Latin word nulla)
            if (value == 0)
                return "N";

            // Otherwise, use lookup table to build string
            StringBuilder builder = new();
            foreach (RomanInfo info in RomanNumerals)
            {
                while (value >= info.Value)
                {
                    builder.Append(info.Numerals);
                    value -= info.Value;
                }
            }
            return builder.ToString();
        }

        /// <summary>
        /// Converts a string of Roman numeral characters to the equivalent integer value.
        /// Throws an exception if an unsupported character is encountered.
        /// </summary>
        /// <param name="s">String to convert</param>
        /// <exception cref="ArgumentException"></exception>
        public static int ParseRomanNumerals(this string? s)
        {
            if (!TryParseRomanNumerals(s, out int result))
                throw new ArgumentException("Invalid Roman numeral character", nameof(s));
            return result;
        }

        /// <summary>
        /// Attempts to convert a string of Roman numeral characters to the equivalent integer value.
        /// </summary>
        /// <param name="s">The Roman numerals string to parse.</param>
        /// <param name="result">Outputs the value of the Roman numerals string if successful.</param>
        /// <returns>True if successful, false if the string contained unsupported characters.</returns>
        public static bool TryParseRomanNumerals(this string? s, out int result)
        {
            result = 0;
            int pos = 0;

            if (s == null)
                return false;

            s = s.ToUpper();

            // Skip leading whitespace
            while (pos < s.Length && char.IsWhiteSpace(s[pos]))
                pos++;

            // Special handling for zero
            if (s[pos] == 'N')
                return true;

            // Use lookup table to parse string
            while (pos < s.Length && !char.IsWhiteSpace(s[pos]))
            {
                RomanInfo? info;

                // Test for double-character match
                if ((s.Length - pos) >= 2)
                {
                    info = RomanNumerals.Find(i => i.Numerals == s[pos..(pos + 2)]);
                    if (info != null)
                    {
                        result += info.Value;
                        pos += 2;
                        continue;
                    }
                }

                // Otherwise, text for single-character match
                info = RomanNumerals.Find(i => i.Numerals == s[pos..(pos + 1)]);
                if (info != null)
                {
                    result += info.Value;
                    pos++;
                }
                else
                {
                    // Invalid character encountered
                    return false;
                }
            }
            // Return true if any characters processed
            return (pos > 0);
        }
    }
}
