namespace WebExpress.UI.WebControl
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
            return layout switch
            {
                TypeFade.FadeIn => "fade in",
                TypeFade.FadeOut => "fade out",
                TypeFade.FadeShow => "fade show",
                _ => string.Empty,
            };
        }
    }
}
