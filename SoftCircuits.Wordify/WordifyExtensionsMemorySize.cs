// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using SoftCircuits.Wordify.Enums;

namespace SoftCircuits.Wordify
{
    public static partial class WordifyExtensions
    {
        private const ulong Kilobyte = 1000;
        private const ulong Kibibyte = 1024;

        private static readonly (string Decimal, string Binary)[] Suffixes =
        {
            ("B", "B"),
            ("KB", "KiB"),
            ("MB", "MiB"),
            ("GB", "GiB"),
            ("TB", "TiB"),
            ("PB", "PiB"),
            ("EB", "EiB"),
            ("ZB", "ZiB"),
            ("YB", "YiB")
        };

        /// <summary>
        /// Converts an integer value to a memory size in the form "850 bytes", "1.67 MB", etc.
        /// </summary>
        /// <param name="sizeInBytes">The integer value to convert.</param>
        /// <param name="options">Specifies the format method.</param>
        /// <param name="numberFormat">Optional format specifier used to format the numeric portion
        /// of the resulting string.</param>
        /// <returns>The resulting memory size string.</returns>
        public static string ToMemorySize(this int sizeInBytes, MemorySizeOption options = MemorySizeOption.Decimal, string numberFormat = "#,0.##") =>
            ToMemorySize((ulong)sizeInBytes, options, numberFormat);

        /// <summary>
        /// Converts an integer value to a memory size in the form "850 bytes", "1.67 MB", etc.
        /// </summary>
        /// <param name="sizeInBytes">The integer value to convert.</param>
        /// <param name="options">Specifies the format method.</param>
        /// <param name="numberFormat">Optional format specifier used to format the numeric portion
        /// of the resulting string.</param>
        /// <returns>The resulting memory size string.</returns>
        public static string ToMemorySize(this long sizeInBytes, MemorySizeOption options = MemorySizeOption.Decimal, string numberFormat = "#,0.##") =>
            ToMemorySize((ulong)sizeInBytes, options, numberFormat);

        /// <summary>
        /// Converts an integer value to a memory size in the form "850 bytes", "1.67 MB", etc.
        /// </summary>
        /// <param name="sizeInBytes">The integer value to convert.</param>
        /// <param name="options">Specifies the format method.</param>
        /// <param name="numberFormat">Optional format specifier used to format the numeric portion
        /// of the resulting string.</param>
        /// <returns>The resulting memory size string.</returns>
        public static string ToMemorySize(this ulong sizeInBytes, MemorySizeOption options = MemorySizeOption.Decimal, string numberFormat = "#,0.##")
        {
            double size = sizeInBytes;
            int index = 0;
            ulong kb = options.HasFlag(MemorySizeOption.Binary) ? Kibibyte : Kilobyte;

            while (size >= kb)
            {
                index++;
                size /= kb;
            }

            string suffix = options.HasFlag(MemorySizeOption.Binary) ? Suffixes[index].Binary : Suffixes[index].Decimal;
            return $"{size.ToString(numberFormat)} {suffix}";
        }

        /// <summary>
        /// Converts a memory size string (e.g., "1.5 MB" or "5 GiB") to the corresponding integer value.
        /// </summary>
        /// <param name="s">The memory size string to convert.</param>
        /// <returns>The integer value of the memory size string.</returns>
        [Obsolete("This method has been deprecated and will be removed in a future version. Please use ParseMemorySize() instead.")]
        public static ulong FromMemorySize(this string? s) => ParseMemorySize(s);

        /// <summary>
        /// Converts a memory size string (e.g., "1.5 MB" or "5 GiB") to the corresponding integer value.
        /// </summary>
        /// <param name="s">The memory size string to convert.</param>
        /// <returns>The integer value of the memory size string.</returns>
        public static ulong ParseMemorySize(this string? s)
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
                        string suffix = s[startIndex..pos];

                        for (int i = 0; i < Suffixes.Length; i++)
                        {
                            if (suffix.Equals(Suffixes[i].Decimal, StringComparison.OrdinalIgnoreCase))
                            {
                                size *= Math.Pow(Kilobyte, i);
                                break;
                            }
                            if (suffix.Equals(Suffixes[i].Binary, StringComparison.OrdinalIgnoreCase))
                            {
                                size *= Math.Pow(Kibibyte, i);
                                break;
                            }
                        }
                    }
                    return (ulong)Math.Round(size);
                }
            }
            return 0UL;
        }
    }
}
