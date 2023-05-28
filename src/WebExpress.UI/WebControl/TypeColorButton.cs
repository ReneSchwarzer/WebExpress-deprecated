namespace WebExpress.UI.WebControl
{
    public enum TypeColorButton
    {
        Default = 0,
        Primary = 1,
        Secondary = 2,
        Success = 3,
        Info = 4,
        Warning = 5,
        Danger = 6,
        Dark = 7,
        Light = 8
    }

    public static class TypeColorButtonExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="color">Die Farbe, welches umgewandelt werden soll</param>
        /// <param name="outline">Die Outline-Eigenschaft</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypeColorButton color, bool outline = false)
        {
            if (outline)
            {
                switch (color)
                {
                    case TypeColorButton.Primary:
                        return "btn-outline-primary";
                    case TypeColorButton.Secondary:
                        return "btn-outline-secondary";
                    case TypeColorButton.Success:
                        return "btn-outline-success";
                    case TypeColorButton.Info:
                        return "btn-outline-info";
                    case TypeColorButton.Warning:
                        return "btn-outline-warning";
                    case TypeColorButton.Danger:
                        return "btn-outline-danger";
                    case TypeColorButton.Dark:
                        return "btn-outline-dark";
                }
            }
            else
            {
                switch (color)
                {
                    case TypeColorButton.Primary:
                        return "btn-primary";
                    case TypeColorButton.Secondary:
                        return "btn-secondary";
                    case TypeColorButton.Success:
                        return "btn-success";
                    case TypeColorButton.Info:
                        return "btn-info";
                    case TypeColorButton.Warning:
                        return "btn-warning";
                    case TypeColorButton.Danger:
                        return "btn-danger";
                    case TypeColorButton.Light:
                        return "btn-light";
                    case TypeColorButton.Dark:
                        return "btn-dark";
                }
            }

            return string.Empty;
        }
    }
}
