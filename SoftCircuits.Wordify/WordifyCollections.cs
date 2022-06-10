// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using System.Text;

namespace SoftCircuits.Wordify
{
    public static partial class WordifyExtensions
    {
        private const string AndConjunction = " and ";
        private const string OrConjunction = " or ";
        private const string Comma = ", ";
        private const char CommaNoSpace = ',';

        /// <summary>
        /// Combines a collection of items into a common string format. Empty and null values are discarded.
        /// </summary>
        /// <typeparam name="T">The type of each item in the collection.</typeparam>
        /// <param name="values">The collection to join.</param>
        /// <param name="options">Options for how the string is formed.</param>
        /// <returns>A string that represents the collection.</returns>
        public static string Wordify<T>(this IEnumerable<T>? values, CollectionOption options = CollectionOption.AndConjunction)
        {
            if (values == null)
                return string.Empty;

            // Eliminate null or empty values
            IEnumerable<string> stringValues = values
                .Select(v => v?.ToString())
                .Where(s => !string.IsNullOrEmpty(s))!;

            int i = 0, lastIndex = stringValues.Count() - 1;

            using (IEnumerator<string> enumerator = stringValues.GetEnumerator())
            {
                if (!enumerator.MoveNext())
                    return string.Empty;

                StringBuilder builder = new();
                builder.Append(enumerator.Current);

                while (enumerator.MoveNext())
                {
                    bool isLast = ++i == lastIndex;

                    if (isLast)
                    {
                        if (options.HasFlag(CollectionOption.OxfordComma))
                            builder.Append(CommaNoSpace);
                        builder.Append(options.HasFlag(CollectionOption.OrConjunction) ? OrConjunction : AndConjunction);
                    }
                    else
                    {
                        builder.Append(Comma);
                    }
                    builder.Append(enumerator.Current);
                }
                return builder.ToString();
            }
        }
    }
}
