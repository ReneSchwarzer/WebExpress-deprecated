namespace WebExpress.UI.Controls
{
    /// <summary>
    /// Aufzähliung der unterstützten Gerätetypen
    /// </summary>
    public enum TypeDevice
    {
        /// <summary>
        /// Keine Auswahl
        /// </summary>
        None,
        /// <summary>
        /// Automatische Anpassung
        /// </summary>
        Auto,
        /// <summary>
        /// Für Telepfone geeignet < 576px
        /// </summary>
        ExtraSmall,
        /// <summary>
        /// Für Tablets geeignet >= 576px
        /// </summary>
        Small,
        /// <summary>
        /// Für kleine Laptops geeignet >= 768px
        /// </summary>
        Medium,
        /// <summary>
        /// Für Laptops & Desktops geeignet >= 992px
        /// </summary>
        Large,
        /// <summary>
        /// Für Laptops & Desktops geeignet >= 1200px
        /// </summary>
        ExtraLarge
    }
}
