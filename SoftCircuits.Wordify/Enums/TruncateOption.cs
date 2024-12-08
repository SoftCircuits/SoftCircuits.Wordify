// Copyright (c) 2023-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace SoftCircuits.Wordify
#pragma warning restore IDE0130 // Namespace does not match folder structure
{
    /// <summary>
    /// Specifies how a string shold be truncated.
    /// </summary>
    [Flags]
    public enum TruncateOption
    {
        /// <summary>
        /// Specify no options.
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
