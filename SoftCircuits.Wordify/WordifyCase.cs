using SoftCircuits.Wordify.Helpers;
using System.Diagnostics;

namespace SoftCircuits.Wordify
{
    public static partial class Wordify
    {
        /// <summary>
        /// Converts each character in the string to upper case.
        /// </summary>
        /// <param name="s">The string to convert</param>
        /// <param name="caseType"></param>
        /// <returns></returns>
        public static string SetCase(this string? s, CaseType caseType)
        {
            s ??= string.Empty;

            return caseType switch
            {
                CaseType.Lower => s.ToLower(),
                CaseType.Upper => s.ToUpper(),
                CaseType.CapitalizeFirstCharacter => CapitalizeFirstCharacter(s),
                CaseType.Sentence => SetSentenceCase(s),
                CaseType.Title => SetTitleCase(s),
                _ => s,
            };
        }

        /// <summary>
        /// Capitalizes the first character in the given string. Does not modify any other characters.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string CapitalizeFirstCharacter(this string? s)
        {
            if (s == null || s.Length == 0)
                return string.Empty;

            if (!char.IsLower(s[0]))
                return s;

            StringEditor editor = new(s);
            editor[0] = char.ToUpper(editor[0]);
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
