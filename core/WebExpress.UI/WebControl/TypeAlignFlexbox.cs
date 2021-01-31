namespace WebExpress.UI.WebControl
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
            return layout switch
            {
                TypeAlignFlexbox.Start => "align-items-start",
                TypeAlignFlexbox.End => "align-items-end",
                TypeAlignFlexbox.Center => "align-items-center",
                TypeAlignFlexbox.Baseline => "align-items-baseline",
                TypeAlignFlexbox.Stretch => "align-items-stretch",
                _ => string.Empty,
            };
        }
    }

}
