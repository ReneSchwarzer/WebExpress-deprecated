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
            switch (width)
            {
                case TypeHeight.TwentyFive:
                    return "h-25";
                case TypeHeight.Fifty:
                    return "h-50";
                case TypeHeight.SeventyFive:
                    return "h-75";
                case TypeHeight.OneHundred:
                    return "h-100";
            }

            return string.Empty;
        }
    }
}
