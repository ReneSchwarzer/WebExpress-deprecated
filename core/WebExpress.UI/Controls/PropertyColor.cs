namespace WebExpress.UI.Controls
{
    public abstract class PropertyColor : IProperty
    {
        /// <summary>
        /// Die möglichen Farben
        /// </summary>
        public enum Color
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
            Transparent= 10,
            Mute = 11,
            User = 12
        };

        /// <summary>
        /// Die Farbe
        /// </summary>
        public Color Value { get; protected set; }

        /// <summary>
        /// Die benutzerdefinierte Farbe
        /// </summary>
        public string UserValue { get; protected set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PropertyColor()
        {

        }

        /// <summary>
        /// Konvertiert eine Abstandsangabe in einen String
        /// </summary>
        /// <param name="size"></param>
        /// <returns>Die Stringrepräsentation</returns>
        protected string ConvertColor(Color color)
        {
            switch (color)
            {
                case Color.Primary:
                    return "primary";
                case Color.Secondary:
                    return "secondary";
                case Color.Success:
                    return "success";
                case Color.Info:
                    return "info";
                case Color.Warning:
                    return "warning";
                case Color.Danger:
                    return "danger";
                case Color.Light:
                    return "light";
                case Color.Dark:
                    return "dark";
                case Color.White:
                    return "white";
                case Color.Transparent:
                    return "transparent";
                case Color.Mute:
                    return "mute";
            }

            return string.Empty;
        }

        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="prefix">Der Präfix</param>
        /// <returns>Die zur Farbe gehörende CSS-KLasse</returns>
        protected string ToClass(string prefix)
        {
            if (Value != Color.Default && Value != Color.User)
            {
                return prefix + "-" + ConvertColor(Value);
            }

            return null;
        }

        /// <summary>
        /// Umwandlung in einen CSS-Style
        /// </summary>
        /// <param name="prefix">Der Präfix</param>
        /// <returns>Das zur Farbe gehörende CSS-Style</returns>
        protected string ToStyle(string prefix)
        {
            if (Value == Color.User)
            {
                return prefix + ":" + UserValue;
            }

            return null;
        }

        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <returns>Die zur Farbe gehörende CSS-KLasse</returns>
        public abstract string ToClass();

        /// <summary>
        /// Umwandlung in einen CSS-Style
        /// </summary>
        /// <returns>Der zur Farbe gehörende CSS-Style</returns>
        public abstract string ToStyle();
    }
}
