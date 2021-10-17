using WebExpress.Uri;
using WebExpress.WebResource;

namespace WebExpress.WebPage
{
    public interface IPage : IResource
    {
        /// <summary>
        /// Liefert oder setzt den Titel
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Weiterleitung an eine andere Seite
        /// Die Funktion löst die RedirectException aus 
        /// </summary>
        /// <param name="url">Die URL zu der weitergeleitet werden soll</param>
        void Redirecting(IUri url);
    }
}
