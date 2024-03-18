// Copyright (c) 2023-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using System.Text;

namespace SoftCircuits.Wordify
{
    public static partial class Wordify
    {
        /// <summary>
        /// Formats a person's name.
        /// </summary>
        /// <param name="first">Optional first name.</param>
        /// <param name="last">Optional last name.</param>
        /// <param name="middle">Optional middle name.</param>
        /// <param name="title">Optional title.</param>
        /// <param name="suffix">Optional suffix.</param>
        /// <returns>The formatted name.</returns>
        public static string FormatName(string? first = null, string? last = null, string? middle = null, string? title = null, string? suffix = null)
        {
            StringBuilder builder = new();

            foreach (string? part in new[] { title, first, middle, last, suffix})
            {
                if (!string.IsNullOrWhiteSpace(part))
                {
                    if (builder.Length > 0)
                        builder.Append(' ');
                    builder.Append(part);
                }
            }
            return builder.ToString();
        }

        /// <summary>
        /// Formats an address.
        /// </summary>
        /// <param name="street">Optional street and number.</param>
        /// <param name="street2">Optional additional street information.</param>
        /// <param name="city">Optional city.</param>
        /// <param name="state">Optional state.</param>
        /// <param name="zip">Optional zip code.</param>
        /// <param name="country">Optional country.</param>
        /// <param name="delimiter">Optional delimiter used to separate main address components. Default is a new line.</param>
        /// <returns>The formatted string.</returns>
        public static string FormatAddress(string? street = null, string? street2 = null, string? city = null, string? state = null, string? zip = null, string? country = null, string delimiter = "\r\n")
        {
            List<string> parts = new();

            if (!string.IsNullOrWhiteSpace(street))
                parts.Add(street);

            if (!string.IsNullOrWhiteSpace(street2))
                parts.Add(street2);

            string s = FormatCityStateZip(city, state, zip);
            if (!string.IsNullOrWhiteSpace(s))
                parts.Add(s);

            if (!string.IsNullOrWhiteSpace(country))
                parts.Add(country);

            return string.Join(delimiter, parts);
        }

        /// <summary>
        /// Formats a city, state and zip code.
        /// </summary>
        /// <param name="city">Optional city.</param>
        /// <param name="state">Optional state.</param>
        /// <param name="zip">Optional zip code.</param>
        /// <returns>The formatted string.</returns>
        public static string FormatCityStateZip(string? city = null, string? state = null, string? zip = null)
        {
            StringBuilder builder = new();

            if (!string.IsNullOrWhiteSpace(city))
                builder.Append(city);

            if (!string.IsNullOrWhiteSpace(state))
            {
                if (builder.Length > 0)
                    builder.Append(", ");
                builder.Append(state);
            }

            if (!string.IsNullOrWhiteSpace(zip))
            {
                if (builder.Length > 0)
                    builder.Append(' ');
                builder.Append(zip);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Formats the digits in a string as a phone number.
        /// </summary>
        /// <param name="digits">A string that contains the digits to be formatted. Any non-digit characters
        /// are simply discarded.</param>
        /// <param name="options">Formatting options.</param>
        /// <returns>Returns the formatted string.</returns>
        public static string FormatPhoneNumber(this string? digits, PhoneOption options = PhoneOption.None)
        {
            if (digits == null)
                return string.Empty;

            StringBuilder builder = new();

            digits = string.Concat(digits.Where(c => char.IsDigit(c)));
            int index = 0;
            int length = digits.Length - index - 10;
            if (length > 0)
            {
                // International code
                if (options.HasFlag(PhoneOption.InternationalPlusSign))
                    builder.Append('+');
                builder.Append(digits, index, length);
                index += length;
            }

            length = digits.Length - index - 7;
            if (length > 0)
            {
                // Area code
                if (options.HasFlag(PhoneOption.AreaCodeParentheses))
                {
                    if (builder.Length > 0)
                        builder.Append(' ');
                    builder.Append('(');
                    builder.Append(digits, index, length);
                    builder.Append(')');
                    builder.Append(' ');
                }
                else
                {
                    if (builder.Length > 0)
                        builder.Append('-');
                    builder.Append(digits, index, length);
                    builder.Append('-');
                }
                index += length;
            }

            length = digits.Length - index - 4;
            if (length > 0)
            {
                builder.Append(digits, index, length);
                builder.Append('-');
                index += length;
            }

            length = digits.Length - index;
            builder.Append(digits, index, length);

            return builder.ToString();
        }
    }
}
