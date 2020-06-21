using WebExpress.Html;

namespace WebExpress.UI.Controls
{
    public class PropertyIcon : IProperty
    {
        /// <summary>
        /// Das System-Icon
        /// </summary>
        public TypeIcon SystemIcon { get; protected set; }

        /// <summary>
        /// Das benutzerdefinierte Icon
        /// </summary>
        public IUri UserIcon { get; protected set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="icon">Das System-Icon</param>
        public PropertyIcon(TypeIcon icon)
        {
            SystemIcon = icon;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="icon">Das benutzerdefinierte Icon</param>
        public PropertyIcon(IUri icon)
        {
            SystemIcon = TypeIcon.None;
            UserIcon = icon;
        }

        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <returns>Die zum Icon gehörende CSS-KLasse</returns>
        public virtual string ToClass()
        {
            if (SystemIcon != TypeIcon.None)
            {
                return SystemIcon.ToClass();
            }

            return null;
        }

        /// <summary>
        /// Umwandlung in einen CSS-Style
        /// </summary>
        /// <returns>Das zum Icon gehörende CSS-Style</returns>
        public virtual string ToStyle()
        {
            return null;
        }

        /// <summary>
        /// Prüft ob ein Icon gesetzt ist
        /// </summary>
        /// <returns>True wenn ein Icon gesetzt ist, false sonst</returns>
        public virtual bool HasIcon => SystemIcon != TypeIcon.None || UserIcon != null;

        /// <summary>
        /// Prüft ob ein benutzerdefiniertes Icon gesetzt ist
        /// </summary>
        /// <returns>True wenn ein benutzerdefiniertes Icon gesetzt ist, false sonst</returns>
        public virtual bool IsUserIcon => UserIcon != null;
    }
}
