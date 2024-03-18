// Copyright (c) 2023-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

namespace SoftCircuits.Wordify
{
    /// <summary>
    /// Specifies how spaces are inserted into a string.
    /// </summary>
    public enum WordifyOption
    {
        /// <summary>
        /// Replace hyphens with spaces.
        /// </summary>
        ReplaceHyphens,

        /// <summary>
        /// Replace underscores with spaces.
        /// </summary>
        ReplaceUnderscores,

        /// <summary>
        /// Insert spaces between camel-case words.
        /// </summary>
        CamelCase,

        /// <summary>
        /// Automatically detect best option.
        /// </summary>
        AutoDetect,
    }
}
