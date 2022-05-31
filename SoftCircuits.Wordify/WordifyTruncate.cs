using SoftCircuits.Wordify.Helpers;

namespace SoftCircuits.Wordify
{
    public static partial class Wordify
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
        public static string Truncate(this string? s, int maxLength, TruncateOptions options = TruncateOptions.None)
        {
            if (maxLength < 0)
                throw new ArgumentOutOfRangeException(nameof(maxLength), "Length cannot be less than zero.");

            if (s == null)
                return string.Empty;

            if (s.Length <= maxLength)
                return s;

            StringEditor editor = new(s);
            if (options.HasFlag(TruncateOptions.AppendEllipsis))
            {
                if (maxLength > Ellipsis.Length)
                    maxLength -= Ellipsis.Length;
                else
                    options &= ~TruncateOptions.AppendEllipsis;
            }

            int length = maxLength;

            if (options.HasFlag(TruncateOptions.TrimPartialWords))
            {
                while (length > 0 && StringHelper.IsWordCharacter(editor, length))
                    length--;
                while (length > 0 && char.IsWhiteSpace(editor[length - 1]))
                    length--;
                if (length == 0)
                    length = maxLength;
            }

            editor.Resize(length);

            if (options.HasFlag(TruncateOptions.AppendEllipsis))
                editor.Append(Ellipsis);

            return editor;
        }
    }
}
