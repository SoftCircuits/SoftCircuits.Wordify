namespace SoftCircuits.Wordify
{
    public static partial class Wordify
    {
        /// <summary>
        /// Converts a string to its plural form.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Pluralize(this string? s)
        {
            bool isUpper = false;
            int lastCharIndex = -1;

            if (s != null)
            {
                for (int i = s.Length - 1; i >= 0; i--)
                {
                    if (char.IsLetter(s[i]))
                    {
                        isUpper = char.IsUpper(s[i]);
                        lastCharIndex = i;
                        break;
                    }
                }
            }

            if (lastCharIndex < 0)
                return s ?? string.Empty;

            StringEditor editor = new(s);

            if (char.ToLower(editor[lastCharIndex]) == 'y')
            {
                editor.Insert(lastCharIndex, isUpper ? "IES" : "ies");
            }
            else if (char.ToLower(editor[lastCharIndex]) == 's')
            {
                editor.Insert(lastCharIndex, isUpper ? "ES" : "es");
            }
            else editor.Insert(lastCharIndex, isUpper ? "S" : "s");

            return editor.ToString();
        }

        /// <summary>
        /// Converts a string to its singular form.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Singularize(this string? s)
        {
            bool isUpper = false;
            int lastCharIndex = -1;

            if (s != null)
            {
                for (int i = s.Length - 1; i >= 0; i--)
                {
                    if (char.IsLetter(s[i]))
                    {
                        isUpper = char.IsUpper(s[i]);
                        lastCharIndex = i;
                        break;
                    }
                }
            }

            if (lastCharIndex < 0)
                return s ?? string.Empty;

            // TODO: EndsWith() is probably better for case-insensitive comparisons

            StringEditor editor = new(s);

            if (s.EndsWith("ies", StringComparison.OrdinalIgnoreCase))
                return s[0..^3] + "y";
            if (s.EndsWith("es", StringComparison.OrdinalIgnoreCase))
                return s[0..^2];
            if (s.EndsWith("s", StringComparison.OrdinalIgnoreCase))
                return s[0..^1];
            return s;




            //if (char.ToLower(modifier[lastCharIndex]) == 'y')
            //{
            //    modifier.Replace(lastCharIndex, isUpper ? "IES" : "ies");
            //}
            //else if (char.ToLower(modifier[lastCharIndex]) == 's')
            //{
            //    modifier.Replace(lastCharIndex, isUpper ? "ES" : "es");
            //}
            //else modifier.Replace(lastCharIndex, isUpper ? "S" : "s");

            //return modifier.ToString();
        }

        /// <summary>
        /// Converts the given string to the plural form only if <paramref name="value"/> is not equal to 1.
        /// </summary>
        public static string Pluralize(this string? s, int value) => (value == 1) ? s ?? string.Empty : Pluralize(s);

        /// <summary>
        /// Converts the given string to the plural form only if <paramref name="value"/> is not equal to 1.
        /// </summary>
        public static string Pluralize(this string? s, long value) => (value == 1) ? s ?? string.Empty : Pluralize(s);

        /// <summary>
        /// Converts the given string to the plural form only if <paramref name="value"/> is not equal to 1.0.
        /// </summary>
        public static string Pluralize(this string? s, double value) => (value == 1.0) ? s ?? string.Empty : Pluralize(s);

        /// <summary>
        /// Converts the given string to the plural form only if <paramref name="value"/> is not equal to 1.0.
        /// </summary>
        public static string Pluralize(this string? s, decimal value) => (value == 1.0M) ? s ?? string.Empty : Pluralize(s);
    }
}
