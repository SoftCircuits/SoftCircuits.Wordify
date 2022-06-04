﻿// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using SoftCircuits.Wordify.Helpers;

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
            if (s != null && s.Length > 0)
            {
                StringEditor editor = new(s);
                int firstLetterIndex = editor.IndexOf(char.IsLetter);
                if (firstLetterIndex != -1)
                {
                    if (char.IsLower(editor[firstLetterIndex]))
                    {
                        editor[firstLetterIndex] = char.ToUpper(editor[firstLetterIndex]);
                        return editor;
                    }
                }
            }
            return s ?? string.Empty;
        }

        /// <summary>
        /// Converts a string to sentence case by capitalizing the first letter in each sentence.
        /// </summary>
        public static string SetSentenceCase(string? s)
        {
            bool inSentence = false;

            StringEditor editor = new(s);
            for (int i = 0; i < editor.Length; i++)
            {
                if (StringHelper.IsWordCharacter(editor, i))
                {
                    if (!inSentence)
                    {
                        editor[i] = char.ToUpper(editor[i]);
                        inSentence = true;
                    }
                }
                else if (StringHelper.IsEndOfSentenceCharacter(editor, i))
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
        public static string SetTitleCase(string s)
        {
            int wordStartIndex = -1;
            bool inSentence = false;

            StringEditor editor = new(s);

            int i = 0;
            for (; i < editor.Length; i++)
            {
                if (StringHelper.IsWordCharacter(editor, i))
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
                    if (inSentence && StringHelper.IsEndOfSentenceCharacter(editor, i))
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
