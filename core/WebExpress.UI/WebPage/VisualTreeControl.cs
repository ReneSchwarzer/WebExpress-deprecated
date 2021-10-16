using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.UI.WebControl;
using WebExpress.Uri;
using WebExpress.WebPage;
using WebExpress.WebResource;

namespace WebExpress.UI.WebPage
{
    /// <summary>
    /// Die inhaltliche Gestanltung einer Seite (Page) wird durch Steuerelemente (Controls) realisiert.
    /// </summary>
    public class VisualTreeControl : VisualTree
    {
        /// <summary>
        /// Liefert den Inhalt 
        /// </summary>
        public new List<Control> Content { get; } = new List<Control>();

        /// <summary>
        /// Konstruktor
        /// </summary>
        public VisualTreeControl()
        {
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        /// <returns>Die Seite als HTML-Baum</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var html = new HtmlElementRootHtml();
            html.Head.Title = Title;
            html.Head.Favicons = Favicons?.Select(x => new Favicon(new UriRelative(x.Url).ToString(), x.Mediatype));
            html.Head.Styles = Styles?.Select(x => new UriRelative(x).ToString());
            html.Head.Meta = Meta;
            html.Head.Scripts = HeaderScripts;
            html.Body.Elements.AddRange(Content?.Where(x => x.Enable).Select(x => x.Render(context)));
            html.Body.Scripts = Scripts.Values.ToList();

            html.Head.CssLinks = CssLinks.Where(x => x != null).Select(x => x.ToString());
            html.Head.ScriptLinks = HeaderScriptLinks?.Where(x => x != null).Select(x => x.ToString());

            return html;
        }
    }
}
