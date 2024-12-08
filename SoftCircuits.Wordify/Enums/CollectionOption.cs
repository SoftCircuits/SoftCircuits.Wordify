// Copyright (c) 2023-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace SoftCircuits.Wordify
#pragma warning restore IDE0130 // Namespace does not match folder structure
{
    /// <summary>
    /// Specifies how a collection should be formatted.
    /// </summary>
    [Flags]
    public enum CollectionOption
    {
        /// <summary>
        /// Use &quot;and&quot; as the final conjunction. This is the default behavior.
        /// </summary>
        AndConjunction = 0x00,

        /// <summary>
        /// Use &quot;or&quot; as the final conjunction.
        /// </summary>
        OrConjunction = 0x01,

        /// <summary>
        /// Specifies that a comma should also be included with the final conjunction. For example,
        /// &quot;One, two, and three.&quot;
        /// </summary>
        OxfordComma = 0x02,
    }
}
