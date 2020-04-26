namespace WebExpress.UI.Controls
{
    public enum TypesFade
    {
        None,
        FadeIn,
        FadeOut,
        FadeShow,
    }

    public static class TypesFadeExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="layout">Das Layout, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypesFade layout)
        {
            switch (layout)
            {
                case TypesFade.FadeIn:
                    return "fade in";
                case TypesFade.FadeOut:
                    return "fade out";
                case TypesFade.FadeShow:
                    return "fade show";
            }

            return string.Empty;
        }
    }
}
