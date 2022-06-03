using SoftCircuits.Wordify.Helpers;
using System.Diagnostics;

namespace SoftCircuits.Wordify
{
    public static partial class Wordify
    {
        /// <summary>
        /// Sets the case of this string's characters using the specified option.
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <param name="caseOption"></param>
        /// <returns>The converted string.</returns>
        public static string SetCase(this string? s, CaseOption caseOption)
        {
            if (s == null)
                return string.Empty;

            return caseOption switch
            {
                CaseOption.Lower => s.ToLower(),
                CaseOption.Upper => s.ToUpper(),
                CaseOption.CapitalizeFirstLetter => SetFirstLetterCapital(s),
                CaseOption.Sentence => SetSentenceCase(s),
                CaseOption.Title => SetTitleCase(s),
                _ => s,
            };
        }

        /// <summary>
        /// Converts this string to upper case.
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <returns>The converted string.</returns>
        public static string SetUpperCase(this string? s) => (s != null) ? s.ToUpper() : string.Empty;

        /// <summary>
        /// Convert this string to lower case.
        /// </summary>
        /// <param name="s">The string to convert.</param>
        /// <returns>The converted string.</returns>
        public static string SetLowerCase(this string? s) => (s != null) ? s.ToLower() : string.Empty;

        /// <summary>
        /// Capitalizes the first letter in the given string. Does not modify any other characters.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string SetFirstLetterCapital(this string? s)
        {
            if (s == null || s.Length == 0)
                return string.Empty;

            StringEditor editor = new(s);
            int firstLetterIndex = editor.IndexOf(char.IsLetter);
            if (!char.IsLower(editor[firstLetterIndex]))
                return s;
            editor[firstLetterIndex] = char.ToUpper(editor[firstLetterIndex]);
            return editor;
        }

        #region Sentence Case

        /// <summary>
        /// Converts a string to sentence case.
        /// </summary>
        public static string SetSentenceCase(string? s)
        {
            bool inSentence = false;
            int wordStart = -1;
            int i;

            StringEditor editor = new(s);
            for (i = 0; i < editor.Length; i++)
            {
                if (StringHelper.IsWordCharacter(editor, i))
                {
                    if (wordStart == -1)
                        wordStart = i;
                }
                else if (wordStart != -1)
                {
                    SetWordSentenceCase(editor, wordStart, i, ref inSentence);
                    wordStart = -1;
                }
            }

            if (wordStart != -1)
                SetWordSentenceCase(editor, wordStart, i, ref inSentence);

            return editor;
        }

        private static void SetWordSentenceCase(StringEditor editor, int wordStart, int wordEnd, ref bool inSentence)
        {
            Debug.Assert(wordStart != -1);
            Debug.Assert(wordStart <= wordEnd);
            Debug.Assert(wordEnd <= editor.Length);

            // Set word to lower case if not acronym
            if (StringHelper.RangeIncludesLowerCase(editor, wordStart, wordEnd))
            {
                for (int i = wordStart; i < wordEnd; i++)
                    editor[i] = char.ToLower(editor[i]);
            }

            if (!inSentence)
            {
                editor[wordStart] = char.ToUpper(editor[wordStart]);
                inSentence = true;
            }

            if (inSentence && wordEnd < editor.Length && StringHelper.IsEndOfSentenceCharacter(editor, wordEnd))
                inSentence = false;
        }

        #endregion

        #region Title Case

        private static HashSet<string> UncapitalizedTitleWords { get; set; } = new(StringComparer.OrdinalIgnoreCase)
        {
            "a",
            "about",
            "after",
            "an",
            "and",
            "are",
            "around",
            "as",
            "at",
            "be",
            "before",
            "but",
            "by",
            "else",
            "for",
            "from",
            "how",
            "if",
            "in",
            "is",
            "into",
            "nor",
            "of",
            "on",
            "or",
            "over",
            "than",
            "that",
            "the",
            "then",
            "this",
            "through",
            "to",
            "under",
            "when",
            "where",
            "why",
            "with"
        };

        /// <summary>
        /// Converts a string to title case.
        /// </summary>
        public static string SetTitleCase(string s)
        {
            bool inSentence = false;
            int wordStart = -1;
            int i;

            StringEditor editor = new(s);

            for (i = 0; i < editor.Length; i++)
            {
                if (StringHelper.IsWordCharacter(editor, i))
                {
                    if (wordStart == -1)
                        wordStart = i;
                }
                else if (wordStart != -1)
                {
                    SetWordTitleCase(editor, wordStart, i, ref inSentence);
                    wordStart = -1;
                }
            }

            if (wordStart != -1)
                SetWordTitleCase(editor, wordStart, i, ref inSentence);

            return editor.ToString();
        }

        private static void SetWordTitleCase(StringEditor editor, int wordStart, int wordEnd, ref bool inSentence)
        {
            Debug.Assert(wordStart != -1);
            Debug.Assert(wordStart <= wordEnd);
            Debug.Assert(wordEnd <= editor.Length);

            // Set word to lower case if not acronym
            if (StringHelper.RangeIncludesLowerCase(editor, wordStart, wordEnd))
            {
                for (int i = wordStart; i < wordEnd; i++)
                    editor[i] = char.ToLower(editor[i]);
            }

            if (!inSentence || !UncapitalizedTitleWords.Contains(editor[wordStart..wordEnd]))
            {
                editor[wordStart] = char.ToUpper(editor[wordStart]);
                inSentence = true;
            }

            if (inSentence && wordEnd < editor.Length && StringHelper.IsEndOfSentenceCharacter(editor, wordEnd))
                inSentence = false;
        }

        #endregion

    }
}
