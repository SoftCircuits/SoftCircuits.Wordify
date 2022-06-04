// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.Wordify
{
    public static partial class Wordify
    {
        private const long Kilobyte = 1024;
        private static readonly string[] Suffixes = { "B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

        /// <summary>
        /// Converts an integer value to a memory size in the form "850 bytes", "1.67 MB", etc.
        /// </summary>
        /// <param name="sizeInBytes">The integer value to convert.</param>
        /// <param name="numberFormat">Optional format specifier used to format the numeric portion
        /// of the resulting string.</param>
        /// <returns>The resulting memory size string.</returns>
        public static string ToMemorySize(ulong sizeInBytes, string numberFormat = "#,0.##")
        {
            double size = sizeInBytes;
            int suffixIndex = 0;

            while (size >= Kilobyte)
            {
                suffixIndex++;
                size /= Kilobyte;
            }
            return $"{size.ToString(numberFormat)} {Suffixes[suffixIndex]}";
        }

        /// <summary>
        /// Converts a memory size string (e.g., "1.5 MB") to the integer value.
        /// </summary>
        /// <param name="s">The memory size string to convert.</param>
        /// <returns>The integer value of the memory size string.</returns>
        public static ulong FromMemorySize(this string? s)
        {
            if (!string.IsNullOrEmpty(s))
            {
                // Skip leading whitespace
                int startIndex = 0;
                while (startIndex < s.Length && char.IsWhiteSpace(s[startIndex]))
                    startIndex++;

                // Get numeric portion
                int pos = startIndex;
                while (pos < s.Length && (char.IsDigit(s[pos]) || s[pos] == '.'))
                    pos++;
                if (pos > startIndex && double.TryParse(s[startIndex..pos], out double size))
                {
                    // Skip whitespace
                    while (pos < s.Length && char.IsWhiteSpace(s[pos]))
                        pos++;

                    // Get multiplier portion
                    startIndex = pos;
                    while (pos < s.Length && char.IsLetter(s[pos]))
                        pos++;

                    if (pos > startIndex)
                    {
                        string suffix = s[startIndex..pos].ToUpper();
                        int suffixIndex = -1;
                        for (int i = 0; i < Suffixes.Length; i++)
                        {
                            if (suffix == Suffixes[i])
                            {
                                suffixIndex = i;
                                break;
                            }
                        }

                        if (suffixIndex > 0)
                            size *= Math.Pow(Kilobyte, suffixIndex);
                    }
                    return (ulong)Math.Round(size);
                }
            }
            return 0;
        }
    }
}
