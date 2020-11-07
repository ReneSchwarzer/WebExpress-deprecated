namespace WebExpress.UI.Controls
{
    public enum TypeColorBackgroundList
    {
        Default = 0,
        Primary = 1,
        Secondary = 2,
        Success = 3,
        Info = 4,
        Warning = 5,
        Danger = 6,
        Dark = 7,
        Light = 8,
        White = 9,
        Transparent = 10
    }

    public static class TypeColorBackgroundListExtensions
    {
        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="layout">Das Layout, welches umgewandelt werden soll</param>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public static string ToClass(this TypeColorBackgroundList layout)
        {
            switch (layout)
            {
                case TypeColorBackgroundList.Primary:
                    return "list-group-item-primary";
                case TypeColorBackgroundList.Secondary:
                    return "list-group-item-secondary";
                case TypeColorBackgroundList.Success:
                    return "list-group-item-success";
                case TypeColorBackgroundList.Info:
                    return "list-group-item-info";
                case TypeColorBackgroundList.Warning:
                    return "list-group-item-warning";
                case TypeColorBackgroundList.Danger:
                    return "list-group-item-danger";
                case TypeColorBackgroundList.Light:
                    return "list-group-item-light";
                case TypeColorBackgroundList.Dark:
                    return "list-group-item-dark";
                case TypeColorBackgroundList.White:
                    return "bg-white";
                case TypeColorBackgroundList.Transparent:
                    return "bg-transparent";
            }

            return string.Empty;
        }
    }
}
