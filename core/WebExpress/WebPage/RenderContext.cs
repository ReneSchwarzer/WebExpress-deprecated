using System.Globalization;
using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.Uri;
using WebExpress.WebPage;
using WebExpress.WebResource;

namespace WebExpress.WebPage
{
    public class RenderContext : II18N
    {
        /// <summary>
        /// Die Seite, indem das Steuerelement gerendert wird
        /// </summary>
        public IPage Page { get; private set; }

        /// <summary>
        /// Liefert die Anfrage
        /// </summary>
        public Request Request { get; private set; }

        /// <summary>
        /// Die Uir der Seite
        /// </summary>
        public IUri Uri => Page?.Uri;

        /// <summary>
        /// Liefert die I18N-PluginID
        /// </summary>
        public string I18N_PluginID => Page.Context.PluginID;

        /// <summary>
        /// Liefert die Kultur
        /// </summary>
        public CultureInfo Culture { get; set; }

        /// <summary>
        /// Liefert die AnwendungsID
        /// </summary>
        public string ApplicationID => Page?.Context?.ApplicationID;

        /// <summary>
        /// Liefert oder setzt die Inhalte einer Seite
        /// </summary>
        public IVisualTree VisualTree { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die Seite, indem das Steuerelement gerendert wird</param>
        /// <param name="request">Die Anfrage</param>
        /// <param name="visualTree">Der visuelle Baum</param>
        public RenderContext(IPage page, Request request, IVisualTree visualTree)
        {
            Page = page;
            Request = request;
            VisualTree = visualTree;
            Culture = (Page as Resource).Culture;
        }

        /// <summary>
        /// Copy-Konstruktor
        /// </summary>
        /// <param name="context">Der zu kopierende Kontext/param>
        public RenderContext(RenderContext context)
            : this(context?.Page, context?.Request, context?.VisualTree)
        {
        }

        /// <summary>
        /// Liefert den Visualisierungsbaum
        /// </summary>
        /// <returns>Der Visualisierungsbaum</returns>
        public T GetVisualTree<T>() where T : IVisualTree
        {
            return (T)VisualTree; 
        }
    }
}
