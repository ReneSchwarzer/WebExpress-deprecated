using WebExpress.Html;
using WebExpress.Module;
using WebExpress.Uri;

namespace WebExpress.UI.WebResource
{
    /// <summary>
    /// Seite, die aus einem vertikal angeordneten Kopf-, Inhalt- und Fuss-Bereich besteht
    /// </summary>
    public abstract class ResourcePageTemplate : ResourcePageBlank
    {
        /// <summary>
        /// Konstruktor
        /// </summary>
        public ResourcePageTemplate()
        {
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Initialization()
        {
            base.Initialization();
        }

        ///// <summary>
        ///// Verarbeitung
        ///// </summary>
        //public override void Process()
        //{
        //    base.Process();

        //    base.Content.Clear();
        //}

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <returns>Die Seite als HTML-Baum</returns>
        public override IHtmlNode Render()
        {
            return base.Render();
        }
    }
}
