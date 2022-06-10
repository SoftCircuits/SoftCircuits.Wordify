// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.Wordify
{
    public static partial class WordifyExtensions
    {
        private const string Ellipsis = "...";

        /// <summary>
        /// Returns a copy of this string truncated to the specified length.
        /// </summary>
        /// <param name="s"></param>
        /// <param name="maxLength">Maximum string length.</param>
        /// <param name="options">Specifies truncate options.</param>
        /// <returns>The modified string.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static string Truncate(this string? s, int maxLength, TruncateOption options = TruncateOption.None)
        {
            if (maxLength < 0)
                throw new ArgumentOutOfRangeException(nameof(maxLength), "Length cannot be less than zero.");

            if (s == null)
                return string.Empty;

            if (s.Length <= maxLength)
                return s;

            StringEditor editor = new(s);
            if (options.HasFlag(TruncateOption.AppendEllipsis))
            {
                if (maxLength > Ellipsis.Length)
                    maxLength -= Ellipsis.Length;
                else
                    options &= ~TruncateOption.AppendEllipsis;
            }

            int length = maxLength;

            if (options.HasFlag(TruncateOption.TrimPartialWords))
            {
                while (length > 0 && editor.IsWordCharacter(length))
                    length--;
                while (length > 0 && char.IsWhiteSpace(editor[length - 1]))
                    length--;
                if (length == 0)
                    length = maxLength;
            }

            editor.Resize(length);

            if (options.HasFlag(TruncateOption.AppendEllipsis))
                editor.Append(Ellipsis);

            return editor;
        }
    }
}
