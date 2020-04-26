namespace WebExpress.UI.Controls
{
    public class PropertyColorBackground : PropertyColor
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="color">Die Farbe</param>
        public PropertyColorBackground(TypesBackgroundColor color)
        {
            Value = (Color)color;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="color">Die Farbe</param>
        public PropertyColorBackground(string color)
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
            return ToClass("bg");
        }

        /// <summary>
        /// Umwandlung in einen CSS-Style
        /// </summary>
        /// <returns>Der zur Farbe gehörende CSS-Style</returns>
        public override string ToStyle()
        {
            return ToStyle("background");
        }
    }
}
