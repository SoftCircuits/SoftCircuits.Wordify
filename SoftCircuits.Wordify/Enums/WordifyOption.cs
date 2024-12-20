﻿// Copyright (c) 2023-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace SoftCircuits.Wordify
#pragma warning restore IDE0130 // Namespace does not match folder structure
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
