namespace WebExpress.UI.WebControl
{
    public class PropertyColorButton : PropertyColor<TypeColorButton>
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="color">Die Farbe</param>
        public PropertyColorButton(TypeColorButton color)
        {
            SystemColor = color;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="color">Die Farbe</param>
        public PropertyColorButton(string color)
        {
            SystemColor = (TypeColorButton)TypeColor.User;
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
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <param name="outline">Bestimmt ob die Hintergrundfarbe entfernt wird</param>
        /// <returns>Die zur Farbe gehörende CSS-KLasse</returns>
        public virtual string ToClass(bool outline)
        {
            if ((TypeColor)SystemColor != TypeColor.Default && (TypeColor)SystemColor != TypeColor.User)
            {
                return SystemColor.ToClass(outline);
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

        /// <summary>
        /// Umwandlung in einen CSS-Style
        /// </summary>
        /// <param name="outline">Bestimmt ob die Hintergrundfarbe entfernt wird</param>
        /// <returns>Der zur Farbe gehörende CSS-Style</returns>
        public virtual string ToStyle(bool outline)
        {
            if ((TypeColor)SystemColor == TypeColor.User)
            {
                if (outline)
                {
                    return "border-color:" + UserColor + ";";
                }

                return "background:" + UserColor + ";border-color:" + UserColor + ";";
            }

            return null;
        }

    }
}
