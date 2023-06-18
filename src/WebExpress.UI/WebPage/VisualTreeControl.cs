using System.Collections.Generic;
using System.Linq;
using WebExpress.WebHtml;
using WebExpress.Internationalization;
using WebExpress.UI.WebControl;
using WebExpress.WebPage;
using WebExpress.WebResource;

namespace WebExpress.UI.WebPage
{
    /// <summary>
    /// The content design of a page is realized by controls.
    /// </summary>
    public class VisualTreeControl : VisualTree
    {
        /// <summary>
        /// Returns the content.
        /// </summary>
        public new List<Control> Content { get; } = new List<Control>();

        /// <summary>
        /// Constructor
        /// </summary>
        public VisualTreeControl()
        {
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context for rendering the page.</param>
        /// <returns>The page as an html tree.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var html = new HtmlElementRootHtml();
            html.Head.Title = InternationalizationManager.I18N(context.Request, context.Page?.Title);
            html.Head.Favicons = Favicons?.Select(x => new Favicon(x.Url, x.Mediatype));
            html.Head.Styles = Styles;
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
