// Copyright (c) 2023 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using SoftCircuits.Wordify.Extensions;
using SoftCircuits.Wordify.Helpers;

namespace SoftCircuits.Wordify
{
    public static partial class WordifyExtensions
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
                CaseOption.Capitalize => Capitalize(s),
                CaseOption.CapitalizeAll => CapitalizeAll(s),
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
        /// Capitalizes the first letter in the given string. No other characters are modified.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Capitalize(this string? s)
        {
            if (s == null)
                return string.Empty;

            int firstLetterIndex = s.IndexOf(char.IsLetter);
            if (firstLetterIndex != -1 && char.IsLower(s[firstLetterIndex]))
            {
                MutableString editor = new(s);
                editor[firstLetterIndex] = char.ToUpper(editor[firstLetterIndex]);
                return editor;
            }

            return s;
        }

        /// <summary>
        /// Capitalizes the first letter of each word in the given string. No other characters are modified.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string CapitalizeAll(this string? s)
        {
            if (s == null)
                return string.Empty;

            int firstLetterIndex = s.IndexOf(char.IsLetter);
            if (firstLetterIndex != -1)
            {
                MutableString editor = new(s);
                bool inWord = false;

                for (int i = firstLetterIndex; i < s.Length; i++)
                {
                    if (inWord)
                    {
                        //if (char.IsWhiteSpace(s[i]))
                        if (!char.IsLetter(s[i]) && s[i] != '\'')
                            inWord = false;
                    }
                    else
                    {
                        if (char.IsLetter(s[i]))
                        {
                            editor[i] = char.ToUpper(s[i]);
                            inWord = true;
                        }
                    }
                }
                return editor;
            }

            return s;
        }

        /// <summary>
        /// Converts a string to sentence case by capitalizing the first letter in each sentence.
        /// </summary>
        public static string SetSentenceCase(this string? s)
        {
            if (string.IsNullOrWhiteSpace(s))
                return s ?? string.Empty;

            bool inSentence = false;

            MutableString editor = new(s);
            for (int i = 0; i < editor.Length; i++)
            {
                if (editor.IsWordCharacter(i))
                {
                    if (!inSentence)
                    {
                        editor[i] = char.ToUpper(editor[i]);
                        inSentence = true;
                    }
                }
                else if (editor.IsEndOfSentenceCharacter(i))
                {
                    inSentence = false;
                }
            }
            return editor;
        }

        /// <summary>
        /// Words that are not capitalized in a title.
        /// </summary>
        private static readonly HashSet<string> UncapitalizedTitleWords = new(StringComparer.OrdinalIgnoreCase)
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
        public static string SetTitleCase(this string? s)
        {
            // NOTE: Technically, the last word of the title should always be capitalized

            if (string.IsNullOrWhiteSpace(s))
                return s ?? string.Empty;

            int wordStartIndex = -1;
            bool inSentence = false;

            MutableString editor = new(s);

            int i = 0;
            for (; i < editor.Length; i++)
            {
                if (editor.IsWordCharacter(i))
                {
                    if (wordStartIndex == -1)
                        wordStartIndex = i;
                }
                else
                {
                    if (wordStartIndex != -1)
                    {
                        if (!inSentence || !UncapitalizedTitleWords.Contains(editor[wordStartIndex..i]))
                        {
                            editor[wordStartIndex] = char.ToUpper(editor[wordStartIndex]);
                            inSentence = true;
                        }
                        wordStartIndex = -1;
                    }
                    if (inSentence && editor.IsEndOfSentenceCharacter(i))
                        inSentence = false;
                }
            }

            if (wordStartIndex != -1)
            {
                if (!inSentence || !UncapitalizedTitleWords.Contains(editor[wordStartIndex..i]))
                    editor[wordStartIndex] = char.ToUpper(editor[wordStartIndex]);
            }

            return editor;
        }
    }
}
