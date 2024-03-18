// Copyright (c) 2023-2024 Jonathan Wood (www.softcircuits.com)
// Licensed under the MIT license.
//

using System.ComponentModel;
using System.Reflection;

namespace SoftCircuits.Wordify
{
    public static partial class WordifyExtensions
    {
        /// <summary>
        /// Converts an enum value to a string.
        /// </summary>
        /// <typeparam name="T">The enum type.</typeparam>
        /// <param name="value">The value to transform.</param>
        /// <param name="ignoreDescription">By default, the description from the <see cref="DescriptionAttribute"/>
        /// associated with the value will be returned if one is available. Set this parameter to true to prevent that.</param>
        /// <returns>The transformed string.</returns>
        public static string Wordify<T>(this T value, bool ignoreDescription = false) where T : Enum
        {
            string valueString = value.ToString();

            if (!ignoreDescription)
            {
                MemberInfo? memberInfo = typeof(T).GetMember(valueString).FirstOrDefault();
                if (memberInfo != null)
                {
                    DescriptionAttribute? attribute = memberInfo.GetCustomAttribute<DescriptionAttribute>();
                    if (attribute != null)
                        return attribute.Description;
                }
            }
            return Wordify(valueString, WordifyOption.AutoDetect);
        }
    }
}
