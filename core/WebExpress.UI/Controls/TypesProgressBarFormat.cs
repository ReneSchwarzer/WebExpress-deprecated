namespace WebExpress.UI.Controls
{
    /// <summary>
    /// Die ProgressBar Formate
    /// </summary>
    public enum TypesProgressBarFormat
    {
        Default,
        Colored,
        Striped,
        Animated
    }

    public static class TypesProgressBarFormatExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="layout">Das Layout, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypesProgressBarFormat layout)
        {
            switch (layout)
            {
                case TypesProgressBarFormat.Colored:
                    return "progress-bar";
                case TypesProgressBarFormat.Striped:
                    return "progress-bar progress-bar-striped";
                case TypesProgressBarFormat.Animated:
                    return "progress-bar progress-bar-striped progress-bar-animated";
            }

            return string.Empty;
        }
    }
}
