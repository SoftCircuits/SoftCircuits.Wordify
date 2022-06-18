// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using System.Text;

namespace SoftCircuits.Wordify
{
    public static partial class Wordify
    {
        /// <summary>
        /// Formats the digits in a string as a phone number.
        /// </summary>
        /// <param name="digits">A string that contains the digits to be formatted.</param>
        /// <param name="options">Formatting options.</param>
        /// <returns>Returns the formatted string.</returns>
        public static string FormatPhoneNumber(this string? digits, PhoneOption options = PhoneOption.None)
        {
            if (digits == null || digits.Length == 0)
                return string.Empty;

            if (digits.Any(c => !char.IsDigit(c)))
                digits = string.Concat(digits.Where(c => char.IsDigit(c)));

            StringBuilder builder = new();

            int index = 0;

            if (digits.Length > 10)
            {
                int count = digits.Length - 10;
                // International code
                if (options.HasFlag(PhoneOption.InternationalPlusSign))
                    builder.Append('+');
                builder.Append(digits, index, count);
                index += count;
            }

            if (digits.Length > 7)
            {
                int count = Math.Min(digits.Length - 7, 3);
                // Area code
                if (options.HasFlag(PhoneOption.AreaCodeParentheses))
                {
                    if (builder.Length > 0)
                        builder.Append(' ');
                    builder.Append('(');
                    builder.Append(digits, index, count);
                    builder.Append(')');
                    builder.Append(' ');
                }
                else
                {
                    if (builder.Length > 0)
                        builder.Append('-');
                    builder.Append(digits, index, count);
                    builder.Append('-');
                }
                index += count;
            }

            if (digits.Length > 4)
            {
                int count = Math.Min(digits.Length - 4, 3);
                builder.Append(digits, index, count);
                builder.Append('-');
                index += count;
            }

            builder.Append(digits, index, Math.Min(digits.Length, 4));

            return builder.ToString();
        }
    }
}
