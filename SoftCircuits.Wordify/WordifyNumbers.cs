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
        public static string Transform(int value) => WordifyNumber(value.ToString());

        /// <summary>
        /// Converts the given value to words.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted string.</returns>
        public static string Transform(long value) => WordifyNumber(value.ToString());

        /// <summary>
        /// Converts the given value to words.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted string.</returns>
        public static string Transform(uint value) => WordifyNumber(value.ToString());

        /// <summary>
        /// Converts the given value to words.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted string.</returns>
        public static string Transform(ulong value) => WordifyNumber(value.ToString());

        /// <summary>
        /// Converts the given value to words.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted string.</returns>
        public static string Transform(double value, FractionFormat format) => Transform((decimal)value, format);

        /// <summary>
        /// Converts the given value to words.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <returns>The converted string.</returns>
        public static string Transform(decimal value, FractionFormat format)
        {
            string? fraction = FormatFraction(ref value, format);
            string s = WordifyNumber(Math.Floor(value).ToString());
            if (fraction != null)
                s = $"{s} and {fraction}";


            //string s = WordifyNumber(value.ToString());
            //if (sFraction != null)
            //{
            //    StringEditor editor = new(s);

            //    // Replace " and " with ", "
            //    // Append " and "
            //    // Append fraction

            //    int i = editor.IndexOf(" and ");
            //    if (i != -1)
            //        editor.Insert(i, ", ", 5);
            //    editor.Append(sFraction);

            //    return editor;
            //}
            //else return s;


            return s;
        }



        //private static string NumberToWords(string number)
        //{
        //    Debug.Assert(number != null);
        //    Debug.Assert(!number.Contains('.'));
        //    Debug.Assert(!number.Contains(' '));

        //    int i = 0;
        //    int length = number.Length;
        //    StringBuilder builder = new();

        //    // Handle negative numbers
        //    if (number[0] == '-')
        //    {
        //        builder.Append("negative ");
        //        i++;
        //    }

        //    int prevDigitValue = 0;

        //    for (; i < length; i++)
        //    {
        //        Debug.Assert(char.IsDigit(number[i]));
        //        int digitValue = number[i] - '0';
        //        int remaining = length - (i + 1);

        //        switch (remaining % 3)
        //        {
        //            case 0: // One's column

        //                bool showThousands = true;
        //                if (i == 0)
        //                {
        //                    // First digit in number (last in loop)
        //                    temp = $"{StringHelper.Ones[digitValue]} ";
        //                }
        //                else if (digits[i - 1] == '1')
        //                {
        //                    // This digit is part of "teen" value
        //                    temp = $"{StringHelper.Teens[digitValue]} ";
        //                    // Skip tens position
        //                    i--;
        //                }
        //                else if (digitValue != 0)
        //                {
        //                    // Any non-zero digit
        //                    temp = $"{StringHelper.Ones[digitValue]} ";
        //                }
        //                else
        //                {
        //                    // This digit is zero. If digit in tens and hundreds
        //                    // column are also zero, don't show "thousands"
        //                    temp = string.Empty;
        //                    // Test for non-zero digit in this grouping
        //                    if (digits[i - 1] != '0' || i > 1 && digits[i - 2] != '0')
        //                        showThousands = true;
        //                    else
        //                        showThousands = false;
        //                }

        //                // Show "thousands" if non-zero in grouping
        //                if (showThousands)
        //                {
        //                    if (column > 0)
        //                        temp = $"{temp}{StringHelper.Thousands[column / 3]}{(allZeros ? " " : ", ")}";
        //                    // Indicate non-zero digit encountered
        //                    allZeros = false;
        //                }
        //                builder.Insert(0, temp);
        //                //break;


        //                if (prevDigitValue != 0)
        //                   builder.Append('-');

        //                builder.Append(StringHelper.Ones[digitValue]);
        //                break;

        //            case 1: // Ten's column
        //                if (digitValue > 0)
        //                {
        //                    builder.Append(StringHelper.Tens[digitValue]);
        //                    //builder.Append(number[i + 1]  $"(digits[i + 1] != '0' ? "-" : " ")}");
        //                }
        //                break;

        //            case 2: // Hundred's column
        //                if (digitValue > 0)
        //                {
        //                    builder.Append(StringHelper.Ones[digitValue]);
        //                    builder.Append(" hundred");
        //                }
        //                break;
        //        }
        //        prevDigitValue = digitValue;
        //    }

        //    return builder.ToString();
        //}

        /// <summary>
        /// Converts a number string to words. Input string must not include decimal or exponential notation.
        /// </summary>
        private static string WordifyNumber(string digits)
        {
            bool isNegative = false;
            int firstIndex = 0;
            bool allZeros = true;
            string temp;

            Debug.Assert(!digits.Contains('.'));

            if (digits.Length > 0 && digits[0] == '-')
            {
                isNegative = true;
                firstIndex = 1;
            }

            // Use StringBuilder to build result
            StringBuilder builder = new();

            // Traverse characters in reverse order
            for (int i = digits.Length - 1; i >= firstIndex; i--)
            {
                int digitValue = digits[i] - '0';
                int column = digits.Length - (i + 1);

                // Determine if ones, tens, or hundreds column
                switch (column % 3)
                {
                    case 0:        // Ones position
                        bool showThousands = true;
                        if (i == 0)
                        {
                            // First digit in number (last in loop)
                            temp = $"{StringHelper.Ones[digitValue]} ";
                        }
                        else if (digits[i - 1] == '1')
                        {
                            // This digit is part of "teen" value
                            temp = $"{StringHelper.Teens[digitValue]} ";
                            // Skip tens position
                            i--;
                        }
                        else if (digitValue != 0)
                        {
                            // Any non-zero digit
                            temp = $"{StringHelper.Ones[digitValue]} ";
                        }
                        else
                        {
                            // This digit is zero. If digit in tens and hundreds
                            // column are also zero, don't show "thousands"
                            temp = string.Empty;
                            // Test for non-zero digit in this grouping
                            if (digits[i - 1] != '0' || i > 1 && digits[i - 2] != '0')
                                showThousands = true;
                            else
                                showThousands = false;
                        }

                        // Show "thousands" if non-zero in grouping
                        if (showThousands)
                        {
                            if (column > 0)
                                temp = $"{temp}{StringHelper.Thousands[column / 3]}{(allZeros ? " " : ", ")}";
                            // Indicate non-zero digit encountered
                            allZeros = false;
                        }
                        builder.Insert(0, temp);
                        break;

                    case 1:        // Tens column
                        if (digitValue > 0)
                            builder.Insert(0, $"{StringHelper.Tens[digitValue]}{(digits[i + 1] != '0' ? "-" : " ")}");
                        break;

                    case 2:        // Hundreds column
                        if (digitValue > 0)
                            builder.Insert(0, $"{StringHelper.Ones[digitValue]} hundred ");
                        break;
                }
            }

            // Remove trailing space
            if (char.IsWhiteSpace(builder[^1]))
                builder.Length--;

            // Indicate if negative
            if (isNegative)
                builder.Insert(0, "negative ");

            return builder.ToString();
        }

        /// <summary>
        /// <para>
        /// Formats the fractional portion of <paramref name="value"/> using the specified format.
        /// The formatted fraction is positive regardless of the sign of <paramref name="value"/>.
        /// </para>
        /// <para>
        /// If <paramref name="format"/> is <see cref="FractionFormat.Round"/> or <see cref="FractionFormat.Truncate"/>,
        /// <paramref name="value"/> is modified by being rounded to the appropriate whole number.
        /// </para>
        /// </summary>
        /// <param name="value">The value that contains the fraction to be formatted.</param>
        /// <param name="format">The type of format to use.</param>
        /// <returns>The formatted fractional part of <paramref name="value"/>, or <c>null</c> if it
        /// has no fractional part after possible modification.</returns>
        private static string? FormatFraction(ref decimal value, FractionFormat format)
        {
            // Note: Math.Floor rounds down, Math.Ceiling rounds up, and Math.Truncate rounds towards zero.
            long integralPart = (long)Math.Truncate(value);
            decimal fractionalPart = Math.Abs(value - integralPart);
            string? fraction = null;

            switch (format)
            {
                case FractionFormat.Round:
                    value = Math.Round(value);
                    break;

                case FractionFormat.Truncate:
                    value = integralPart;
                    break;

                case FractionFormat.Decimal:
                    fraction = fractionalPart.ToString(".0");
                    break;

                case FractionFormat.Fraction:
                    {
                        Fraction f = Fraction.FromReal(fractionalPart);
                        if (!f.IsEmpty)
                            fraction = f.ToString();
                    }
                    break;

                case FractionFormat.Words:
                default:
                    {
                        Fraction f = Fraction.FromReal(fractionalPart);
                        if (!f.IsEmpty)
                        {
                            if (f.Denominator == 1)
                            {
                                fraction = WordifyNumber(f.Numerator.ToString());
                            }
                            else if (f.Numerator == 1 && f.Denominator == 2)
                            {
                                fraction = "one half";
                            }
                            else
                            {
                                fraction = MakeOrdinal(f.Denominator);
                                fraction = $"{WordifyNumber(f.Numerator.ToString())} {fraction.Pluralize(f.Numerator)}";
                            }
                        }
                    }
                    break;

                case FractionFormat.Check:
                    {
                        int cents = (int)Math.Round(fractionalPart * 100.0m);
                        if (cents > 0)
                            fraction = $"{cents:00}/100";
                        else
                            fraction = "XX/100";
                    }
                    break;
            }

            return fraction;
        }
    }
}
