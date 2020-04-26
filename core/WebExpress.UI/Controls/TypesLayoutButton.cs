namespace WebExpress.UI.Controls
{
    public enum TypesLayoutButton
    {
        Default,
        Primary,
        Success,
        Info,
        Warning,
        Danger,
        Light,
        Dark
    }

    public static class TypesLayoutButtonExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="layout">Das Layout, welches umgewandelt werden soll</param>
        /// <param name="outline">Die Outline-Eigenschaft</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypesLayoutButton layout, bool outline = false)
        {
            if (outline)
            {
                switch (layout)
                {
                    case TypesLayoutButton.Primary:
                        return "btn-outline-primary";
                    case TypesLayoutButton.Success:
                        return "btn-outline-success";
                    case TypesLayoutButton.Info:
                        return "btn-outline-info";
                    case TypesLayoutButton.Warning:
                        return "btn-outline-warning";
                    case TypesLayoutButton.Danger:
                        return "btn-outline-danger";
                    case TypesLayoutButton.Dark:
                        return "btn-outline-dark";
                }
            }
            else
            {
                switch (layout)
                {
                    case TypesLayoutButton.Primary:
                        return "btn-primary";
                    case TypesLayoutButton.Success:
                        return "btn-success";
                    case TypesLayoutButton.Info:
                        return "btn-info";
                    case TypesLayoutButton.Warning:
                        return "btn-warning";
                    case TypesLayoutButton.Danger:
                        return "btn-danger";
                    case TypesLayoutButton.Light:
                        return "btn-light";
                    case TypesLayoutButton.Dark:
                        return "btn-dark";
                }
            }

            return string.Empty;
        }
    }
}
