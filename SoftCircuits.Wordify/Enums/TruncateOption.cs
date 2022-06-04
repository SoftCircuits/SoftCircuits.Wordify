﻿// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.Wordify
{
    [Flags]
    public enum TruncateOption
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
