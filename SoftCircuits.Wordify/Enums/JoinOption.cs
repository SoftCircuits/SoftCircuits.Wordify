// Copyright (c) 2022 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.Wordify
{
    [Flags]
    public enum CollectionOption
    {
        /// <summary>
        /// Uses "and" as the final conjunction. This is the default.
        /// </summary>
        AndConjunction = 0x00,

        /// <summary>
        /// Uses "or" as the final conjunction.
        /// </summary>
        OrConjunction = 0x01,

        /// <summary>
        /// Specifies that a comma should also be included with the final conjunction. For example,
        /// "One, two, and three."
        /// </summary>
        OxfordComma = 0x02,
    }
}
