// Copyright (c) 2023-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.Wordify
{
    /// <summary>
    /// Specifies how a collection should be formatted.
    /// </summary>
    [Flags]
    public enum CollectionOption
    {
        /// <summary>
        /// Use "and" as the final conjunction. This is the default.
        /// </summary>
        AndConjunction = 0x00,

        /// <summary>
        /// Use "or" as the final conjunction.
        /// </summary>
        OrConjunction = 0x01,

        /// <summary>
        /// Specifies that a comma should also be included with the final conjunction. For example,
        /// "One, two, and three."
        /// </summary>
        OxfordComma = 0x02,
    }
}
