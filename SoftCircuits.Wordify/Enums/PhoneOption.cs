// Copyright (c) 2023-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

#pragma warning disable IDE0130 // Namespace does not match folder structure
namespace SoftCircuits.Wordify
#pragma warning restore IDE0130 // Namespace does not match folder structure
{
    /// <summary>
    /// Specifies options for formatting phone numbers.
    /// </summary>
    [Flags]
    public enum PhoneOption
    {
        /// <summary>
        /// No phone number options.
        /// </summary>
        None = 0x0000,

        /// <summary>
        /// Show plus symbol (+) next to international code.
        /// </summary>
        InternationalPlusSign = 0x0001,

        /// <summary>
        /// Display any area code with a dash. The default.
        /// </summary>
        AreaCodeDash = None,

        /// <summary>
        /// Display any area code within parentheses.
        /// </summary>
        AreaCodeParentheses = 0x0002,
    }
}
