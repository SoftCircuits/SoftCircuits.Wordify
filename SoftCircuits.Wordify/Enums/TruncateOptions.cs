﻿namespace SoftCircuits.Wordify
{
    [Flags]
    public enum TruncateOptions
    {
        /// <summary>
        /// Disable all options.
        /// </summary>
        None,

        /// <summary>
        /// Add an ellipsis (...) to the end of the truncated string, if there's room.
        /// </summary>
        AppendEllipsis,

        /// <summary>
        /// Trailing partial words and whitespace are removed, unless there is not enough room
        /// for at least one whole word.
        /// </summary>
        TrimPartialWords,
    }
}
