// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using SoftCircuits.Wordify.Helpers;
using System.Diagnostics;
using System.Text;

namespace SoftCircuits.Wordify
{
    public static partial class Wordify
    {
        /// <summary>
        /// Converts the given value to words.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted string.</returns>
        public static string Transform(int value) => FormatNumber(value.ToString());

        /// <summary>
        /// Converts the given value to words.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted string.</returns>
        public static string Transform(long value) => FormatNumber(value.ToString());

        /// <summary>
        /// Converts the given value to words.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted string.</returns>
        public static string Transform(uint value) => FormatNumber(value.ToString());

        /// <summary>
        /// Converts the given value to words.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted string.</returns>
        public static string Transform(ulong value) => FormatNumber(value.ToString());

        /// <summary>
        /// Converts the given value to words.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted string.</returns>
        public static string Transform(double value, FractionOption format) => Transform((decimal)value, format);

        /// <summary>
        /// Converts the given value to words.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted string.</returns>
        public static string Transform(decimal value, FractionOption format)
        {
            string? fraction = FormatFraction(ref value, format);

            decimal dollars = Math.Floor(value);
            StringBuilder builder = new(FormatNumber(dollars.ToString()));
            if (format == FractionOption.UsCurrency)
            {
                builder.Append(' ');
                builder.Append(Pluralize("dollar", dollars));
            }
            if (fraction != null)
            {
                builder.Append(" and ");
                builder.Append(fraction);
            }
            return builder.ToString();
        }

        [Flags]
        private enum ColumnState
        {
            None = 0x00,
            SetOnes = 0x01,
            SetTens = 0x02,
            SetHundreds = 0x04,
        };

        /// <summary>
        /// Converts a number string to words. Input string must not include a decimal point, spaces or
        /// exponential notation.
        /// </summary>
        private static string FormatNumber(string number)
        {
            Debug.Assert(number != null);
            Debug.Assert(!number.Contains('.'));
            Debug.Assert(!number.Contains(' '));

            ColumnState columnState = ColumnState.SetHundreds | ColumnState.SetTens;
            StringBuilder builder = new();
            int[] values;
            int length;

            // Create array with value for each digit
            if (number[0] == '-')
            {
                // Negative number
                builder.Append("negative");
                length = number.Length - 1;
                values = new int[length];
                for (int i = 0; i < length; i++)
                    values[i] = number[i + 1] - '0';
            }
            else
            {
                length = number.Length;
                values = new int[length];
                for (int i = 0; i < length; i++)
                    values[i] = number[i] - '0';
            }

            // Iterate through each digit
            for (int i = 0; i < length; i++)
            {
                // Remaining digits after this one
                int remaining = length - (i + 1);

                switch (remaining % 3)
                {
                    case 0: // Ones column

                        // Write ones digit
                        if (values[i] != 0 && !columnState.HasFlag(ColumnState.SetOnes))
                        {
                            if (i > 0 && values[i - 1] != 0)
                                builder.Append('-');
                            else if (builder.Length > 0)
                                builder.Append(' ');
                            builder.Append(StringHelper.Ones[values[i]]);
                        }
                        else if (remaining == 0 && i == 0)
                            builder.Append(StringHelper.Ones[0]);

                        // Write thousand, million, etc.
                        if (remaining >= 3 && columnState != ColumnState.None)
                        {
                            builder.Append(' ');
                            builder.Append(StringHelper.Thousands[remaining / 3]);
                        }

                        // Reset column state
                        columnState = ColumnState.None;
                        break;

                    case 1: // Tens column

                        if (values[i] > 0)
                        {
                            // Write tens column
                            if (builder.Length > 0)
                                builder.Append(' ');

                            if (values[i] == 1)
                            {
                                Debug.Assert(i + 1 < length);
                                builder.Append(StringHelper.Teens[values[i + 1]]);
                                // Indicate ones column set
                                columnState |= ColumnState.SetOnes;
                            }
                            else builder.Append(StringHelper.Tens[values[i]]);

                            // Indicate tens column set
                            columnState |= ColumnState.SetTens;
                        }
                        break;

                    case 2: // Hundreds column
                        if (values[i] > 0)
                        {
                            // Write hundreds column
                            if (builder.Length > 0)
                                builder.Append(' ');

                            builder.Append(StringHelper.Ones[values[i]]);
                            builder.Append(" hundred");

                            // Indicate hundreds column set
                            columnState |= ColumnState.SetHundreds;
                        }
                        break;
                }
            }
            return builder.ToString();
        }

        /// <summary>
        /// <para>
        /// Formats the fractional portion of <paramref name="value"/> using the specified format.
        /// The formatted fraction is positive regardless of the sign of <paramref name="value"/>.
        /// </para>
        /// <para>
        /// If <paramref name="format"/> is <see cref="FractionOption.Round"/> or <see cref="FractionOption.Truncate"/>,
        /// <paramref name="value"/> is modified by being rounded to the appropriate whole number.
        /// </para>
        /// </summary>
        /// <param name="value">The value that contains the fraction to be formatted.</param>
        /// <param name="format">The type of format to use.</param>
        /// <returns>The formatted fractional part of <paramref name="value"/>, or <c>null</c> if it
        /// has no fractional part after possible modification.</returns>
        private static string? FormatFraction(ref decimal value, FractionOption format)
        {
            // Note: Math.Floor rounds down, Math.Ceiling rounds up, and Math.Truncate rounds towards zero.
            decimal integralPart = Math.Truncate(value);
            decimal fractionalPart = Math.Abs(value - integralPart);
            string? fraction = null;

            switch (format)
            {
                case FractionOption.Round:
                    value = Math.Round(value);
                    break;

                case FractionOption.Truncate:
                    value = integralPart;
                    break;

                case FractionOption.Decimal:
                    fraction = fractionalPart.ToString(".0");
                    break;

                case FractionOption.Fraction:
                    {
                        Fraction f = Fraction.FromReal(fractionalPart);
                        if (!f.IsEmpty)
                            fraction = f.ToString();
                    }
                    break;

                case FractionOption.Words:
                default:
                    {
                        Fraction f = Fraction.FromReal(fractionalPart);
                        if (!f.IsEmpty)
                        {
                            if (f.Denominator == 1)
                            {
                                fraction = FormatNumber(f.Numerator.ToString());
                            }
                            else if (f.Numerator == 1 && f.Denominator == 2)
                            {
                                fraction = "one half";
                            }
                            else
                            {
                                fraction = MakeOrdinal(f.Denominator);
                                fraction = $"{FormatNumber(f.Numerator.ToString())} {fraction.Pluralize(f.Numerator)}";
                            }
                        }
                    }
                    break;

                case FractionOption.Check:
                    {
                        int cents = (int)Math.Round(fractionalPart * 100.0m);
                        if (cents >= 100)
                        {
                            value += 1;
                            cents = 0;
                        }
                        fraction = $"{cents:00}/100";
                    }
                    break;

                case FractionOption.UsCurrency:
                    {
                        int cents = (int)Math.Round(fractionalPart * 100.0m);
                        if (cents >= 100)
                        {
                            value += 1;
                            cents = 0;
                        }
                        if (cents == 0)
                            fraction = "no cents";
                        else
                            fraction = $"{FormatNumber(cents.ToString())} {Pluralize("cent", cents)}";
                    }
                    break;
            }

            return fraction;
        }
    }
}
