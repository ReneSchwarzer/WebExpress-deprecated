namespace WebExpress.UI.WebControl
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
            return layout switch
            {
                TypeFormatProgress.Colored => "progress-bar",
                TypeFormatProgress.Striped => "progress-bar progress-bar-striped",
                TypeFormatProgress.Animated => "progress-bar progress-bar-striped progress-bar-animated",
                _ => string.Empty,
            };
        }
    }
}
