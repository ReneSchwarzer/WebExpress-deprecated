namespace WebExpress.UI.Controls
{
    public enum TypeFade
    {
        None,
        FadeIn,
        FadeOut,
        FadeShow,
    }

    public static class TypeFadeExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="layout">Das Layout, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypeFade layout)
        {
            switch (layout)
            {
                case TypeFade.FadeIn:
                    return "fade in";
                case TypeFade.FadeOut:
                    return "fade out";
                case TypeFade.FadeShow:
                    return "fade show";
            }

            return string.Empty;
        }
    }
}
