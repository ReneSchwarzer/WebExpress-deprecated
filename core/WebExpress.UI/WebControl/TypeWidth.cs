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
            return width switch
            {
                TypeWidth.TwentyFive => "w-25",
                TypeWidth.Fifty => "w-50",
                TypeWidth.SeventyFive => "w-75",
                TypeWidth.OneHundred => "w-100",
                _ => string.Empty,
            };
        }
    }
}
