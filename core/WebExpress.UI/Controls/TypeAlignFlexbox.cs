namespace WebExpress.UI.Controls
{
    public enum TypeAlignFlexbox
    {
        None,
        Start,
        End,
        Center,
        Baseline,
        Stretch
    }

    public static class TypeAlignFlexboxExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="layout">Das Layout, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypeAlignFlexbox layout)
        {
            switch (layout)
            {
                case TypeAlignFlexbox.Start:
                    return "align-items-start";
                case TypeAlignFlexbox.End:
                    return "align-items-end";
                case TypeAlignFlexbox.Center:
                    return "align-items-center";
                case TypeAlignFlexbox.Baseline:
                    return "align-items-baseline";
                case TypeAlignFlexbox.Stretch:
                    return "align-items-stretch";
            }

            return string.Empty;
        }
    }
    
}
