using System.Globalization;
using WebExpress.Internationalization;
using WebExpress.WebResource;

namespace WebExpress.UI.WebControl
{
    public class RenderContext : II18N
    {
        /// <summary>
        /// Die Seite, indem das Steuerelement gerendert wird
        /// </summary>
        public IPage Page { get; private set; }

        /// <summary>
        /// Liefert die I18N-PluginID
        /// </summary>
        public string I18N_PluginID => Page.Context.PluginID;

        /// <summary>
        /// Liefert die Kultur
        /// </summary>
        public CultureInfo Culture 
        {
            get
            {
                return Page.Culture;
            }
            set
            {

            }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die Seite, indem das Steuerelement gerendert wird</param>
        public RenderContext(IPage page)
        {
            Page = page;
        }
    }
}
