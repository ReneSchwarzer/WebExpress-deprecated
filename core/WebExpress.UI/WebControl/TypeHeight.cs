namespace WebExpress.UI.WebControl
{
    /// <summary>
    /// Die Höhenoptionen
    /// </summary>
    public enum TypeHeight
    {
        Default,
        TwentyFive,
        Fifty,
        SeventyFive,
        OneHundred
    }

    public static class TypesHeightExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="width">Die Weite, welche umgewandelt werden soll</param>
        /// <returns>Die zur Anordnung gehörende CSS-KLasse</returns>
        public static string ToClass(this TypeHeight width)
        {
            return width switch
            {
                TypeHeight.TwentyFive => "h-25",
                TypeHeight.Fifty => "h-50",
                TypeHeight.SeventyFive => "h-75",
                TypeHeight.OneHundred => "h-100",
                _ => string.Empty,
            };
        }
    }
}
