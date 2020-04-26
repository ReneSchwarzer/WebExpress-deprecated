namespace WebExpress.UI.Controls
{
    public class PropertyColorText : PropertyColor
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="color">Die Farbe</param>
        public PropertyColorText(TypesTextColor color)
        {
            Value = (Color)color;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="color">Die Farbe</param>
        public PropertyColorText(string color)
        {
            Value = Color.User;
            UserValue = color;
        }

        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <returns>Die zur Farbe gehörende CSS-KLasse</returns>
        public override string ToClass()
        {
            return ToClass("text");
        }

        /// <summary>
        /// Umwandlung in einen CSS-Style
        /// </summary>
        /// <returns>Der zur Farbe gehörende CSS-Style</returns>
        public override string ToStyle()
        {
            return ToStyle("color");
        }
    }
}
