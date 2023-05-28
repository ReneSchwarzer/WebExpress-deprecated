namespace WebExpress.UI.WebControl
{
    public class PropertyColorChart : PropertyColor<TypeColorChart>
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PropertyColorChart()
        {
            SystemColor = (TypeColorChart)TypeColor.Default;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="color">Die Farbe</param>
        public PropertyColorChart(TypeColorChart color)
        {
            SystemColor = color;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="color">Die Farbe</param>
        public PropertyColorChart(string color)
        {
            SystemColor = (TypeColorChart)TypeColor.User;
            UserColor = color;
        }

        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <returns>Die zur Farbe gehörende CSS-KLasse</returns>
        public override string ToClass()
        {
            return null;
        }

        /// <summary>
        /// Umwandlung in einen CSS-Style
        /// </summary>
        /// <returns>Der zur Farbe gehörende CSS-Style</returns>
        public override string ToStyle()
        {
            return null;
        }

        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <returns>Die zur Farbe gehörende CSS-KLasse</returns>
        public override string ToString()
        {
            if ((TypeColor)SystemColor != TypeColor.Default && (TypeColor)SystemColor != TypeColor.User)
            {
                return SystemColor.ToChartColor();
            }
            else if ((TypeColor)SystemColor == TypeColor.User)
            {
                return UserColor;
            }

            return null;
        }
    }
}
