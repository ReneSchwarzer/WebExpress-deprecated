using WebExpress.Message;
using WebExpress.Uri;
using WebExpress.WebResource;

namespace WebExpress.WebPage
{
    /// <summary>
    /// Der Prototyp einer Webseite
    /// </summary>
    /// <typeparam name="T">Eine Implementation des Visualisierungsbaumes</typeparam>
    public abstract class ResourcePage<T> : Resource, IPage where T : IVisualTree, new()
    {
        /// <summary>
        /// Liefert oder setzt den Titel
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="uri">Die Uri</param>
        /// <param name="context">Der Kontext</param>
        public ResourcePage()
        {

        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        /// <param name="context">Der Kontext</param>
        public override void Initialization(IResourceContext context)
        {
            base.Initialization(context);
        }

        /// <summary>
        /// Weiterleitung an eine andere Seite
        /// Die Funktion löst die RedirectException aus
        /// </summary>
        /// <param name="uri">Die URL zu der weitergeleitet werden soll</param>
        public void Redirecting(IUri uri)
        {
            throw new RedirectException(uri?.ToString());
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>Die Antwort</returns>
        public override Response Process(Request request)
        {
            var context = new RenderContext(this, request, new T());

            Process(context);

            return new ResponseOK()
            {
                Content = context.VisualTree.Render(context)
            };
        }

        /// <summary>
        /// Verarbeitung
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        public abstract void Process(RenderContext context);
    }
}
