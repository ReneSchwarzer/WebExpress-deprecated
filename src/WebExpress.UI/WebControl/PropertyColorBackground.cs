namespace WebExpress.UI.WebControl
{
    public class PropertyColorBackground : PropertyColor<TypeColorBackground>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PropertyColorBackground()
        {
            SystemColor = (TypeColorBackground)TypeColor.Default;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="color">Die Farbe</param>
        public PropertyColorBackground(TypeColorBackground color)
        {
            SystemColor = color;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="color">Die Farbe</param>
        public PropertyColorBackground(string color)
        {
            SystemColor = (TypeColorBackground)TypeColor.User;
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
                return "background:" + UserColor + ";";
            }

            return null;
        }

    }
}
