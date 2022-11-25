// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using System.Text;

namespace SoftCircuits.Wordify
{
    public static partial class WordifyExtensions
    {
        private const int AlphabetLength = 26;
        private const string DefaultColumnName = "A";

        /// <summary>
        /// Converts an integer value to a spreadsheet column name. (For example, 2 would be converted
        /// to "B" and 28 would be converted to "AB".)
        /// </summary>
        /// <param name="column">The column number to convert.</param>
        /// <returns>The converted column name, or "A" if <paramref name="column"/> does not represent
        /// a valid spreadsheet column number.</returns>
        public static string ToSpreadsheetColumn(this int column)
        {
            if (column > 0)
            {
                StringBuilder builder = new(6);

                do
                {
                    column--;
                    char c = (char)('A' + column % AlphabetLength);
                    builder.Insert(0, c);
                    column /= AlphabetLength;
                } while (column > 0);

                return builder.ToString();
            }
            return DefaultColumnName;
        }

        /// <summary>
        /// Parses a spreadsheet column name and returns its corresponding integer value.
        /// </summary>
        /// <param name="s">The column name to parse.</param>
        /// <returns>The corresponding integer column value.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static int ParseSpreadsheetColumn(this string? s)
        {
            if (!TryParseSpreadsheetColumn(s, out int result))
                throw new ArgumentException("Invalid spreadsheet column format", nameof(s));
            return result;
        }

        /// <summary>
        /// Attempts to parse a spreadsheet column name and return its corresponding integer value.
        /// </summary>
        /// <param name="s">The column name to parse.</param>
        /// <param name="result">If successful, is set to the parsed integer value.</param>
        /// <returns>True if successful, false if <paramref name="s"/> could not be parsed.</returns>
        public static bool TryParseSpreadsheetColumn(this string? s, out int result)
        {
            int pos = 0;
            result = 0;

            if (s != null)
            {
                // Skip leading whitespace
                while (pos < s.Length && !char.IsLetter(s[pos]))
                    pos++;

                if (pos < s.Length)
                {
                    do
                    {
                        int i = char.ToUpper(s[pos]) - 'A';
                        result *= AlphabetLength;
                        result += i + 1;
                        pos++;

                    } while (pos < s.Length && char.IsLetter(s[pos]));

                    return true;
                }
            }
            return false;
        }
    }
}
