// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.Wordify
{
    public static partial class WordifyExtensions
    {
        /// <summary>
        /// Creates a description of the time span between now and <paramref name="dateTime"/>.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/> to describe.</param>
        /// <param name="useUtc">If true, <paramref name="dateTime"/> is assumed to be in UTC.</param>
        /// <param name="options">Optional formatting options.</param>
        /// <returns>The generated description.</returns>
        public static string Wordify(this DateTime dateTime, bool useUtc = false, DateTimeOption options = DateTimeOption.None) =>
            Wordify(dateTime, useUtc ? DateTime.UtcNow : DateTime.Now, options);

        /// <summary>
        /// Creates a description of the time span between <paramref name="relativeTo"/> and <paramref name="dateTime"/>.
        /// </summary>
        /// <param name="dateTime">The <see cref="DateTime"/> to describe.</param>
        /// <param name="relativeTo">The <see cref="DateTime"/> that <paramref name="dateTime"/> is relative to.</param>
        /// <param name="options">Optional formatting options.</param>
        /// <returns>The generated description.</returns>
        public static string Wordify(this DateTime dateTime, DateTime relativeTo, DateTimeOption options = DateTimeOption.None)
        {
            bool inPast = dateTime < relativeTo;

            IEnumerator<Difference> enumerator = GetDifferenceParts(dateTime, relativeTo).GetEnumerator();

            if (!enumerator.MoveNext() || enumerator.Current.Type == DifferenceKind.Millisecond)
                return "now";

            Difference difference = enumerator.Current;
            if (difference.Type == DifferenceKind.Day && difference.Value == 1)
                return inPast ? "yesterday" : "tomorrow";

            string value;
            string type = difference.Type.ToString();
            if (difference.Value == 1)
            {
                // Determine 'indefinite article'
                value = (difference.Type == DifferenceKind.Hour) ? "an" : "a";
            }
            else if (options.HasFlag(DateTimeOption.UseWords))
                value = difference.Value.Wordify();
            else
                value = difference.Value.ToString();

            string description = type
                .ToLower()
                .Pluralize(difference.Value);

            return $"{value} {description} {(inPast ? "ago" : "from now")}";
        }

        // TODO: Support DateTimeOffset

#if NET6_0_OR_GREATER

        // TODO: Support DateOnly
        // TODO: Support TimeOnly

#endif

        /// <summary>
        /// Creates a description of the specified <see cref="TimeSpan"/>
        /// </summary>
        /// <param name="timeSpan">The <see cref="TimeSpan"/> to describe.</param>
        /// <param name="precision">By default the precision is 1 and only one part of the difference
        /// is included in the description. Use a larger number to show more differences.</param>
        /// <returns>The generated description.</returns>
        public static string Wordify(this TimeSpan timeSpan, int precision = 1, DateTimeOption options = DateTimeOption.None)
        {
            IEnumerator<Difference> enumerator = GetDifferenceParts(timeSpan).GetEnumerator();

            if (!enumerator.MoveNext())
                return "0 milliseconds";    // ???

            List<string> parts = new();

            do
            {
                Difference difference = enumerator.Current;

                string value;
                if (options.HasFlag(DateTimeOption.UseWords))
                    value = difference.Value.Wordify();
                else
                    value = difference.Value.ToString();

                string description = difference.Type
                    .ToString()
                    .ToLower()
                    .Pluralize(difference.Value);

                parts.Add($"{value} {description}");

            } while (--precision > 0 && enumerator.MoveNext());

            return parts.Wordify();
        }

        #region Private Methods

        private enum DifferenceKind
        {
            Millisecond,
            Second,
            Minute,
            Hour,
            Day,
            Week,
            Month,
            Year,
        };

        private class Difference
        {
            public DifferenceKind Type { get; set; }
            public int Value { get; set; }

            public Difference(DifferenceKind partType, int value)
            {
                Type = partType;
                Value = value;
            }
        }

        private const int DaysPerWeek = 7;

        /// <summary>
        /// Gets the difference parts for a <see cref="TimeSpan"/>. Cannot do months or years because we don't
        /// know what dates are involved and cannot detect leap years, etc.
        /// </summary>
        private static IEnumerable<Difference> GetDifferenceParts(TimeSpan timeSpan)
        {
            if (timeSpan == TimeSpan.Zero)
                yield break;

            // Convert negative timespan to positive
            timeSpan = timeSpan.Duration();

            // Weeks
            if (timeSpan.Days > 7)
            {
                int weeks = (timeSpan.Days / DaysPerWeek);
                yield return new(DifferenceKind.Week, weeks);
                timeSpan = new TimeSpan(timeSpan.Days - (weeks * DaysPerWeek), timeSpan.Hours, timeSpan.Minutes,
                    timeSpan.Seconds, timeSpan.Milliseconds);
            }

            if (timeSpan.Days > 0)
                yield return new(DifferenceKind.Day, timeSpan.Days);
            if (timeSpan.Hours > 0)
                yield return new(DifferenceKind.Hour, timeSpan.Hours);
            if (timeSpan.Minutes > 0)
                yield return new(DifferenceKind.Minute, timeSpan.Minutes);
            if (timeSpan.Seconds > 0)
                yield return new(DifferenceKind.Second, timeSpan.Seconds);
            if (timeSpan.Milliseconds > 0)
                yield return new(DifferenceKind.Millisecond, timeSpan.Milliseconds);
        }

        /// <summary>
        /// Gets the difference parts between two dates.
        /// </summary>
        private static IEnumerable<Difference> GetDifferenceParts(DateTime startDateTime, DateTime endDateTime)
        {
            // Calculate absoluate difference
            DateTime lowDateTime, highDateTime;
            if (startDateTime < endDateTime)
            {
                lowDateTime = startDateTime;
                highDateTime = endDateTime;
            }
            else if (startDateTime > endDateTime)
            {
                lowDateTime = endDateTime;
                highDateTime = startDateTime;
            }
            else yield break;   // No difference

            DateTime tempDateTime;
            int value;

            // Years
            if ((tempDateTime = lowDateTime.AddYears(1)) <= highDateTime)
            {
                value = 0;
                do
                {
                    lowDateTime = tempDateTime;
                    value++;
                } while ((tempDateTime = lowDateTime.AddYears(1)) <= highDateTime);
                yield return new(DifferenceKind.Year, value);
            }

            // Months
            if ((tempDateTime = lowDateTime.AddMonths(1)) <= highDateTime)
            {
                value = 0;
                do
                {
                    lowDateTime = tempDateTime;
                    value++;
                } while ((tempDateTime = lowDateTime.AddMonths(1)) <= highDateTime);
                yield return new(DifferenceKind.Month, value);
            }

            TimeSpan timeSpan = highDateTime - lowDateTime;
            if (timeSpan.Days > 0)
                yield return new(DifferenceKind.Day, timeSpan.Days);
            if (timeSpan.Hours > 0)
                yield return new(DifferenceKind.Hour, timeSpan.Hours);
            if (timeSpan.Minutes > 0)
                yield return new(DifferenceKind.Minute, timeSpan.Minutes);
            if (timeSpan.Seconds > 0)
                yield return new(DifferenceKind.Second, timeSpan.Seconds);
            if (timeSpan.Milliseconds > 0)
                yield return new(DifferenceKind.Millisecond, timeSpan.Milliseconds);
        }

        #endregion

    }
}
