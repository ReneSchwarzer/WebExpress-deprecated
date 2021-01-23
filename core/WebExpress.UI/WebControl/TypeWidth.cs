namespace WebExpress.UI.WebControl
{
    /// <summary>
    /// Die Weitenoptionen
    /// </summary>
    public enum TypeWidth
    {
        Default,
        TwentyFive,
        Fifty,
        SeventyFive,
        OneHundred
    }

    public static class TypesWidthExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="width">Die Weite, welche umgewandelt werden soll</param>
        /// <returns>Die zur Anordnung gehörende CSS-KLasse</returns>
        public static string ToClass(this TypeWidth width)
        {
            switch (width)
            {
                case TypeWidth.TwentyFive:
                    return "w-25";
                case TypeWidth.Fifty:
                    return "w-50";
                case TypeWidth.SeventyFive:
                    return "w-75";
                case TypeWidth.OneHundred:
                    return "w-100";
            }

            return string.Empty;
        }
    }
}
