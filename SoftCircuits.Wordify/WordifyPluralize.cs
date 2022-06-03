﻿namespace SoftCircuits.Wordify
{
    public static partial class Wordify
    {
        /// <summary>
        /// Words that are the same for both singular and plural.
        /// </summary>
        private static readonly HashSet<string> DefectiveNounsLookup = new(StringComparer.OrdinalIgnoreCase)
        {
            "aircraft",
            "binoculars",
            "bison",
            "bourgeois",
            "buffalo",
            "carps",
            "chassis",
            "clothes",
            "cod",
            "corps",
            "deer",
            "dice",
            "elk",
            "fish",
            "fruit",
            "gallows",
            "glasses",
            "goggles",
            "goldfish",
            "grouse",
            "halibut",
            "means",
            "moose",
            "offspring",
            "pajamas",
            "pants",
            "pas", // faux pas
            "reindeer",
            "rendezvous",
            "salmon",
            "scissors",
            "series",
            "scissors",
            "shrimp",
            "sheep",
            "shellfish",
            "shorts",
            "species",
            "squid",
            "swine",
            "trout",
            "tuna",
            "tweezers",
        };

        private static readonly Dictionary<string, string> IrregularNounsLookup = new(StringComparer.OrdinalIgnoreCase)
        {
            ["analysis"] = "analyses",
            ["antithesis"] = "antitheses",
            ["axis"] = "axes",
            ["bacillus"] = "bacilli",
            ["bacterium"] = "bacteria",
            ["child"] = "children",
            ["codex"] = "codices",
            ["crisis"] = "crises",
            ["datum"] = "data",
            ["diagnosis"] = "diagnoses",
            ["die"] = "dice",
            ["ellipsis"] = "ellipses",
            ["erratum"] = "errata",
            ["foot"] = "feet",
            ["goose"] = "geese",
            ["hypothesis"] = "hypotheses",
            ["locus"] = "loci",
            ["louse"] = "lice",
            ["man"] = "men",
            ["minutia"] = "minutiae",
            ["mouse"] = "mice",
            ["oasis"] = "oases",
            ["ovum"] = "ova",
            ["ox"] = "oxen",
            ["parenthesis"] = "parentheses",
            ["person"] = "people",
            ["phylum"] = "phyla",
            ["quiz"] = "quizzes",
            ["stimulus"] = "stimuli",
            ["stratum"] = "strata",
            ["synopsis"] = "synopses",
            ["thesis"] = "theses",
            ["tooth"] = "teeth",
            ["vita"] = "vitae",
            ["woman"] = "women",

            // Ends if "f" or "fe"
            ["calf"] = "calves",
            ["elf"] = "elves",
            ["half"] = "halves",
            ["knife"] = "knives",
            ["leaf"] = "leaves",
            ["life"] = "lives",
            ["loaf"] = "loaves",
            ["self"] = "selves",
            ["thief"] = "thieves",
            ["wife"] = "wives",
            ["wolf"] = "wolves",
        };

        /// <summary>
        /// Converts a string to its plural form.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Pluralize(this string? s)
        {
            if (s == null)
                return string.Empty;

            StringEditor editor = new(s);
            if (!editor.FindLastWord(out int startIndex, out int endIndex))
                return s;
            string lastWord = editor.Substring(startIndex, endIndex - startIndex);

            if (DefectiveNounsLookup.Contains(lastWord))
                return s;

            if (IrregularNounsLookup.TryGetValue(lastWord, out string? replacement))
            {
                editor.Insert(startIndex, replacement, endIndex - startIndex);
            }
            else if (char.ToLower(editor[endIndex - 1]) == 'y' && endIndex >= 2 && editor[endIndex - 2].IsConsonant())
            {
                editor.Insert(endIndex - 1, "ies", 1);
            }
            else if (editor.MatchesEndingAt(endIndex - 1, "s", true) ||
                editor.MatchesEndingAt(endIndex - 1, "x", true) ||
                editor.MatchesEndingAt(endIndex - 1, "z", true) ||
                editor.MatchesEndingAt(endIndex - 1, "sh", true) ||
                editor.MatchesEndingAt(endIndex - 1, "ch", true))
            {
                editor.Insert(endIndex, "es");
            }
            else
            {
                editor.Insert(endIndex, "s");
            }
            return editor;
        }

        /// <summary>
        /// Converts a string to its singular form.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Singularize(this string? s)
        {
            if (s == null)
                return string.Empty;

            StringEditor editor = new(s);
            if (!editor.FindLastWord(out int startIndex, out int endIndex))
                return s;
            string lastWord = editor.Substring(startIndex, endIndex - startIndex);

            if (DefectiveNounsLookup.Contains(lastWord))
                return s;

            string? replacement = null;
            foreach (KeyValuePair<string, string> noun in IrregularNounsLookup)
            {
                if (lastWord.Equals(noun.Value, StringComparison.OrdinalIgnoreCase))
                {
                    replacement = noun.Key;
                    break;
                }
            }

            if (replacement != null)
            {
                editor.Insert(startIndex, replacement, endIndex - startIndex);
            }
            else if (editor.MatchesEndingAt(endIndex - 1, "ies", true))
            {
                editor.Insert(endIndex - 3, "y", 3);
            }
            else if (editor.MatchesEndingAt(endIndex - 1, "ses", true) ||
                editor.MatchesEndingAt(endIndex - 1, "xes", true) ||
                editor.MatchesEndingAt(endIndex - 1, "zes", true))
            {
                editor.Delete(endIndex - 2, 2);
            }
            else if (editor.MatchesEndingAt(endIndex - 1, "shes", true) ||
                editor.MatchesEndingAt(endIndex - 1, "ches", true))
            {
                editor.Delete(endIndex - 2, 2);
            }
            else if (editor.MatchesEndingAt(endIndex - 1, "s", true))
            {
                editor.Delete(endIndex - 1, 1);
            }
            return editor;
        }

        /// <summary>
        /// Converts the given string to the plural form only if <paramref name="value"/> is not equal to 1.
        /// </summary>
        public static string Pluralize(this string? s, int value) => (value == 1) ? s ?? string.Empty : Pluralize(s);

        /// <summary>
        /// Converts the given string to the plural form only if <paramref name="value"/> is not equal to 1.
        /// </summary>
        public static string Pluralize(this string? s, long value) => (value == 1) ? s ?? string.Empty : Pluralize(s);

        /// <summary>
        /// Converts the given string to the plural form only if <paramref name="value"/> is not equal to 1.0.
        /// </summary>
        public static string Pluralize(this string? s, double value) => (value == 1.0) ? s ?? string.Empty : Pluralize(s);

        /// <summary>
        /// Converts the given string to the plural form only if <paramref name="value"/> is not equal to 1.0.
        /// </summary>
        public static string Pluralize(this string? s, decimal value) => (value == 1.0M) ? s ?? string.Empty : Pluralize(s);
    }
}
