using System.Globalization;
using WebExpress.Application;
using WebExpress.Internationalization;
using WebExpress.Message;
using WebExpress.Uri;
using WebExpress.WebResource;

namespace WebExpress.WebPage
{
    public class RenderContext : II18N
    {
        /// <summary>
        /// Die Seite, indem das Steuerelement gerendert wird
        /// </summary>
        public IPage Page { get; internal set; }

        /// <summary>
        /// Liefert die Anfrage
        /// </summary>
        public Request Request { get; internal set; }

        /// <summary>
        /// Die Uir der Seite
        /// </summary>
        public IUri Uri => Request?.Uri;

        /// <summary>
        /// Liefert die I18N-PluginID
        /// </summary>
        public string I18NKey => Page?.Context.Plugin.PluginID;

        /// <summary>
        /// Liefert die Kultur
        /// </summary>
        public CultureInfo Culture
        {
            get { return Page?.Culture; }
            set { }
        }

        /// <summary>
        /// Liefert den Kontext der zugehörigen Anwendung
        /// </summary>
        public IApplicationContext Application => Page?.Context?.Application;

        /// <summary>
        /// Liefert oder setzt die Inhalte einer Seite
        /// </summary>
        public IVisualTree VisualTree { get; protected set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public RenderContext()
        {
        }

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
    }
}
