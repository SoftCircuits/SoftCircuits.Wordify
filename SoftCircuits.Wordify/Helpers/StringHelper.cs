using System.Diagnostics;

namespace SoftCircuits.Wordify.Helpers
{
    /// <summary>
    /// String helper class.
    /// </summary>
    internal static class StringHelper
    {
        /// <summary>
        /// Words for digits 0 through 9.
        /// </summary>
        public static readonly string[] Ones =
        {
            "zero",
            "one",
            "two",
            "three",
            "four",
            "five",
            "six",
            "seven",
            "eight",
            "nine"
        };

        /// <summary>
        /// Words for numbers 10 through 19.
        /// </summary>
        public static readonly string[] Teens =
        {
            "ten",
            "eleven",
            "twelve",
            "thirteen",
            "fourteen",
            "fifteen",
            "sixteen",
            "seventeen",
            "eighteen",
            "nineteen"
        };

        /// <summary>
        /// Words for "tens" 10 through 90.
        /// </summary>
        public static readonly string[] Tens =
        {
            "",
            "ten",
            "twenty",
            "thirty",
            "forty",
            "fifty",
            "sixty",
            "seventy",
            "eighty",
            "ninety"
        };

        /// <summary>
        /// US Numbering
        /// </summary>
        public static readonly string[] Thousands =
        {
            "",
            "thousand",
            "million",
            "billion",
            "trillion",
            "quadrillion",
            "quintillion",
            "sextillion",
            "septillion",
            "octillion",
        };

        public static bool IsWordCharacter(string s, int pos)
        {
            Debug.Assert(pos < s.Length);
            char c = s[pos];
            return char.IsLetterOrDigit(c) ||
                c == '\'' ||
                (c == '.' && pos < s.Length - 1 && char.IsDigit(s[pos + 1]));
        }

        public static bool IsWordCharacter(StringEditor editor, int pos)
        {
            Debug.Assert(pos < editor.Length);
            char c = editor[pos];
            return char.IsLetterOrDigit(c) ||
                c == '\'' ||
                (c == '.' && pos < editor.Length - 1 && char.IsDigit(editor[pos + 1]));
        }

        public static bool IsEndOfSentenceCharacter(StringEditor editor, int pos)
        {
            Debug.Assert(pos < editor.Length);
            char c = editor[pos];
            return c == '!' ||
                c == '?' ||
                c == ':' ||
                (c == '.' && !(pos < (editor.Length - 1) && char.IsDigit(editor[pos + 1])));
        }

        public static bool RangeIncludesLowerCase(StringEditor editor, int start, int end)
        {
            Debug.Assert(start <= end);
            Debug.Assert(end <= editor.Length);

            for (int i = start; i < end; i++)
            {
                if (char.IsLower(editor[i]))
                    return true;
            }

            return false;
        }
    }
}
