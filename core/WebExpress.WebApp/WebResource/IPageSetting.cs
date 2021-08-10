using WebExpress.UI.WebControl;

namespace WebExpress.WebApp.WebResource
{
    /// <summary>
    /// Kennzeichnet eine Klasse als Einstellungsseite
    /// </summary>
    public interface IPageSetting
    {
        /// <summary>
        /// Das Symbol der Einstellungsseite
        /// </summary>
        PropertyIcon Icon { get; set; }
    }
}
