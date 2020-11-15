namespace WebExpress.UI.Controls
{
    public class PropertyColorText : PropertyColor<TypeColorText>
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public PropertyColorText()
        {
            SystemColor = (TypeColorText)TypeColor.Default;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="color">Die Farbe</param>
        public PropertyColorText(TypeColorText color)
        {
            SystemColor = color;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="color">Die Farbe</param>
        public PropertyColorText(string color)
        {
            SystemColor = (TypeColorText)TypeColor.User;
            UserColor = color;
        }

        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <returns>Die zur Farbe gehörende CSS-KLasse</returns>
        public override string ToClass()
        {
            if ((TypeColor)SystemColor != TypeColor.Default && (TypeColor)SystemColor != TypeColor.User)
            {
                return SystemColor.ToClass();
            }

            return null;
        }

        /// <summary>
        /// Umwandlung in einen CSS-Style
        /// </summary>
        /// <returns>Der zur Farbe gehörende CSS-Style</returns>
        public override string ToStyle()
        {
            if ((TypeColor)SystemColor == TypeColor.User)
            {
                return "color:" + UserColor + ";";
            }

            return null;
        }
    }
}
