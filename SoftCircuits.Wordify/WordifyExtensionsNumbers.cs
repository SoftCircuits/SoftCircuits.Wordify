﻿// Copyright (c) 2023-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using SoftCircuits.Wordify.Helpers;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace SoftCircuits.Wordify
{
    public static partial class WordifyExtensions
    {
        /// <summary>
        /// Converts the given value to words.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted string.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Wordify(this int value) => BuildNumberString(value);

        /// <summary>
        /// Converts the given value to words.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted string.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Wordify(this uint value) => BuildNumberString(value);

        /// <summary>
        /// Converts the given value to words.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted string.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Wordify(this long value) => BuildNumberString(value);

        /// <summary>
        /// Converts the given value to words.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted string.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Wordify(this ulong value) => BuildNumberString(value);

        /// <summary>
        /// Converts the given value to words.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="format">Optional formatting options.</param>
        /// <returns>The converted string.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string Wordify(this double value, FractionOption format = FractionOption.Fraction) => Wordify((decimal)value, format);

        /// <summary>
        /// Converts the given value to words.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="format">Optional formatting options.</param>
        /// <returns>The converted string.</returns>
        public static string Wordify(this decimal value, FractionOption format = FractionOption.Fraction)
        {
            string? decimalPart = BuildNumberString(ref value, format);
            decimal wholePart = Math.Floor(value);

            StringBuilder builder = new(BuildNumberString(wholePart));
            if (format == FractionOption.UsCurrency)
            {
                builder.Append(' ');
                builder.Append(Pluralize("dollar", wholePart));
            }
            if (decimalPart != null)
            {
                builder.Append(" and ");
                builder.Append(decimalPart);
            }
            return builder.ToString();
        }

        #region Private methods

        /// <summary>
        /// Words for digits 0 through 9.
        /// </summary>
        private static readonly string[] Ones =
        [
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
        ];

        /// <summary>
        /// Words for numbers 10 through 19.
        /// </summary>
        private static readonly string[] Teens =
        [
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
        ];

        /// <summary>
        /// Words for "tens" 10 through 90.
        /// </summary>
        private static readonly string[] Tens =
        [
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
        ];

        /// <summary>
        /// US Numbering
        /// </summary>
        private static readonly string[] Thousands =
        [
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
        ];

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
        private static string BuildNumberString<T>(T value) where T : struct
        {
            StringBuilder builder = new();
            ColumnState columnState = ColumnState.SetHundreds | ColumnState.SetTens;
            int[] values = GetDigitValues(value, out bool isNegative);
            int length = values.Length;

            if (isNegative)
                builder.Append("negative");

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
                            builder.Append(Ones[values[i]]);
                        }
                        else if (remaining == 0 && i == 0)
                            builder.Append(Ones[0]);

                        // Write thousand, million, etc.
                        if (remaining >= 3 && columnState != ColumnState.None)
                        {
                            builder.Append(' ');
                            builder.Append(Thousands[remaining / 3]);
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
                                builder.Append(Teens[values[i + 1]]);
                                // Indicate ones column set
                                columnState |= ColumnState.SetOnes;
                            }
                            else builder.Append(Tens[values[i]]);

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

                            builder.Append(Ones[values[i]]);
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
        private static string? BuildNumberString(ref decimal value, FractionOption format)
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
                    fraction = fractionalPart.ToString(".0##");
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
                                fraction = BuildNumberString(f.Numerator);
                            }
                            else if (f.Numerator == 1 && f.Denominator == 2)
                            {
                                fraction = "one half";
                            }
                            else
                            {
                                fraction = MakeOrdinal(f.Denominator);
                                fraction = $"{BuildNumberString(f.Numerator)} {fraction.Pluralize(f.Numerator)}";
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
                            fraction = $"{BuildNumberString(cents)} {Pluralize("cent", cents)}";
                    }
                    break;
            }

            return fraction;
        }

        /// <summary>
        /// Builds an array of digit values from the given value.
        /// </summary>
        private static int[] GetDigitValues<T>(T value, out bool isNegative) where T : struct
        {
            string? digits = value.ToString();
            Debug.Assert(digits != null);
            Debug.Assert(digits.Length > 0);
            Debug.Assert(digits.All(c => char.IsNumber(c) || c == '-'));

            int start = 0;
            isNegative = false;
            if (digits[0] == '-')
            {
                start++;
                isNegative = true;
            }

            int[] values = new int[digits.Length - start];
            for (int i = 0; start < digits.Length; i++, start++)
                values[i] = digits[start] - '0';

            return values;
        }

        #endregion

    }
}
