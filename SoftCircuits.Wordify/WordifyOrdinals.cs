using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftCircuits.Wordify
{
    public static partial class Wordify
    {
        // Ordinal Rules:
        //
        // Translations:
        // One = first
        // Two = second
        // Three = third
        // Five = fifth
        // Eight = eighth
        // Nine = “ninth”
        // Twelve = Twelfth
        //
        // When numbers end in "y" this letter is replaced with "ieth"
        // Twenty = twentieth
        // Thirty = thirtieth
        // Forty = fortieth
        // Fifty = fiftieth
        // Eighty = eightieth
        //
        // Remember that, in compound numbers, "th" is added only to the last digit
        // 422 = four hundred and twenty-second
        // 5111 = five thousand, one hundred and eleventh
        // 650 = six hundred and fortieth
        // 129 = one hundred and twenty-ninth

        private static Dictionary<string, string> OrdinalReplacementLookup = new(StringComparer.OrdinalIgnoreCase)
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
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string MakeOrdinal(int value) => MakeOrdinal(Transform(value));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string MakeOrdinal(long value) => MakeOrdinal(Transform(value));


        // TODO: Do we need a version that uses digits? (e.g. 2nd, 21st ?)
        public static string MakeOrdinal(string? s)
        {
            if (s == null)
                return string.Empty;

            s = s.Trim();
            if (s.Length == 0)
                return s;

            StringEditor editor = new(s);

            // Find last word
            int pos = s.Length - 1;
            while (pos >= 0 && !char.IsLetter(editor[pos]))
                pos--;
            int lastWordEnd = pos + 1;

            while (pos >= 0 && char.IsLetter(editor[pos]))
                pos--;
            int lastWordStart = pos + 1;

            if (lastWordStart == lastWordEnd)
                return s;

            string lastWord = editor[lastWordStart..lastWordEnd];

            if (OrdinalReplacementLookup.TryGetValue(lastWord, out string? replacement))
            {
                editor.Insert(lastWordStart, replacement, lastWord.Length);
            }
            else if (lastWord[^1] == 'y')
            {
                editor.Insert(lastWordEnd - 1, "ieth", 1);
            }
            else
            {
                editor.Insert(lastWordEnd, "th");
            }

            return editor;
        }



        /// <summary>
        /// Converts a number info an ordinal number string.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="format">Format string used to create the digits.</param>
        /// <returns>The ordinal string.</returns>
        public static string MakeOrdinalDigits(int value, string format = "#,0") => MakeOrdinalDigits(value.ToString(format));

        /// <summary>
        /// Converts a number info an ordinal number string.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="format">Format string used to create the digits.</param>
        /// <returns>The ordinal string.</returns>
        public static string MakeOrdinalDigits(long value, string format = "#,0") => MakeOrdinalDigits(value.ToString(format));

        /// <summary>
        /// Converts a number info an ordinal number string.
        /// </summary>
        /// <param name="s">A string with numeric digits.</param>
        /// <returns>The ordinal string.</returns>
        public static string MakeOrdinalDigits(string? s)
        {
            if (string.IsNullOrEmpty(s))
                return string.Empty;

            char lastDigit = s[^1];
            char prevDigit = (s.Length > 1) ? s[^2] : '\0';
            string suffix;

            if (!char.IsDigit(lastDigit))
                return s;

            if (lastDigit == '1' && prevDigit != '1')
                suffix = "st";
            else if (lastDigit == '2' && prevDigit != '1')
                suffix = "nd";
            else if (lastDigit == '3' && prevDigit != '1')
                suffix = "rd";
            else
                suffix = "th";

            return string.Concat(s, suffix);
        }
    }
}
