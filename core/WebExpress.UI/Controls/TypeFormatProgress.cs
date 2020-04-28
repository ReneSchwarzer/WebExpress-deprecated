namespace WebExpress.UI.Controls
{
    /// <summary>
    /// Die ProgressBar Formate
    /// </summary>
    public enum TypeFormatProgress
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
        public static string ToClass(this TypeFormatProgress layout)
        {
            switch (layout)
            {
                case TypeFormatProgress.Colored:
                    return "progress-bar";
                case TypeFormatProgress.Striped:
                    return "progress-bar progress-bar-striped";
                case TypeFormatProgress.Animated:
                    return "progress-bar progress-bar-striped progress-bar-animated";
            }

            return string.Empty;
        }
    }
}
