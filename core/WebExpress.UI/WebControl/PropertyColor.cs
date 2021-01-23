namespace WebExpress.UI.WebControl
{
    public abstract class PropertyColor<T> : IProperty where T : System.Enum
    {
        /// <summary>
        /// Die Farbe
        /// </summary>
        public T SystemColor { get; protected set; }

        /// <summary>
        /// Die benutzerdefinierte Farbe
        /// </summary>
        public string UserColor { get; protected set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PropertyColor()
        {

        }

        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <returns>Die zur Farbe gehörende CSS-KLasse</returns>
        public abstract string ToClass();

        /// <summary>
        /// Umwandlung in einen CSS-Style
        /// </summary>
        /// <returns>Das zur Farbe gehörende CSS-Style</returns>
        public abstract string ToStyle();

    }
}
