﻿// Copyright (c) 2023-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.Wordify.Helpers
{
    /// <summary>
    /// Class to hold a fraction. Includes code to convert a floating point value to a fraction.
    /// </summary>
    /// <remarks>
    /// This code was derived from
    /// <see href="https://stackoverflow.com/questions/5124743/algorithm-for-simplifying-decimal-to-fractions"/>.
    /// </remarks>
    internal class Fraction(int numerator, int denominator)
    {
        public int Numerator { get; set; } = numerator;
        public int Denominator { get; set; } = denominator;

        /// <summary>
        /// Returns true if either the <see cref="Numerator"/> is equal to the <see cref="Denominator"/>,
        /// or if either one equals zero.
        /// </summary>
        public bool IsEmpty => Numerator == Denominator || Numerator == 0 || Denominator == 0;

        public override string ToString() => $"{Numerator}/{Denominator}";

        /// <summary>
        /// Creates a <see cref="Fraction"/> instance from a floating point value.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static Fraction FromReal(decimal value, decimal accuracy = 0.000001m)
        {
            value -= Math.Floor(value);

            if (accuracy <= 0.0m || accuracy >= 1.0m)
                throw new ArgumentOutOfRangeException(nameof(accuracy), "Must be between 0 and 1");

            int sign = Math.Sign(value);

            if (sign == -1)
                value = Math.Abs(value);

            // Accuracy is the maximum relative error; convert to absolute maxError
            decimal maxError = sign == 0 ? accuracy : value * accuracy;

            int n = (int)Math.Floor(value);
            value -= n;

            if (value < maxError)
                return new Fraction(sign * n, 1);

            if (1 - maxError < value)
                return new Fraction(sign * (n + 1), 1);

            // The lower fraction is 0/1
            int lower_n = 0;
            int lower_d = 1;

            // The upper fraction is 1/1
            int upper_n = 1;
            int upper_d = 1;

            while (true)
            {
                // The middle fraction is (lower_n + upper_n) / (lower_d + upper_d)
                int middle_n = lower_n + upper_n;
                int middle_d = lower_d + upper_d;

                if (middle_d * (value + maxError) < middle_n)
                {
                    // real + error < middle : middle is our new upper
                    Seek(ref upper_n, ref upper_d, lower_n, lower_d, (un, ud) => (lower_d + ud) * (value + maxError) < (lower_n + un));
                }
                else if (middle_n < (value - maxError) * middle_d)
                {
                    // middle < real - error : middle is our new lower
                    Seek(ref lower_n, ref lower_d, upper_n, upper_d, (ln, ld) => (ln + upper_n) < (value - maxError) * (ld + upper_d));
                }
                else
                {
                    // Middle is our best fraction
                    return new Fraction((n * middle_d + middle_n) * sign, middle_d);
                }
            }
        }

        /// <summary>
        /// Binary seek for the value where f() becomes false.
        /// </summary>
        private static void Seek(ref int a, ref int b, int ainc, int binc, Func<int, int, bool> f)
        {
            a += ainc;
            b += binc;

            if (f(a, b))
            {
                int weight = 1;

                do
                {
                    weight *= 2;
                    a += ainc * weight;
                    b += binc * weight;
                }
                while (f(a, b));

                do
                {
                    weight /= 2;

                    int adec = ainc * weight;
                    int bdec = binc * weight;

                    if (!f(a - adec, b - bdec))
                    {
                        a -= adec;
                        b -= bdec;
                    }
                }
                while (weight > 1);
            }
        }
    }
}
