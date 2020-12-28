using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Internationalization;
using WebExpress.Module;
using WebExpress.WebResource;
using WebExpress.UI.WebControl;
using WebExpress.Uri;

namespace WebExpress.UI.WebResource
{
    public class ResourcePageBlank : ResourcePage
    {
        /// <summary>
        /// Liefert oder setzt den Inhalt
        /// </summary>
        protected new List<Control> Content { get; } = new List<Control>();

        /// <summary>
        /// Konstruktor
        /// </summary>
        public ResourcePageBlank()
        {
            
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Initialization()
        {
            base.Initialization();
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <returns>Die Seite als HTML-Baum</returns>
        public override IHtmlNode Render()
        {
            var html = new HtmlElementRootHtml();
            html.Head.Title = this.I18N(Title);
            //html.Head.Base = Context.ContextPath.ToString();
            html.Head.Styles = Styles.Select(x => new UriRelative(x).ToString());
            
            html.Head.Favicons = Favicons.Select(x => new Favicon(new UriRelative(x.Url).ToString(), x.Mediatype));
            html.Head.Meta = Meta;
            html.Head.Scripts = HeaderScripts;
            html.Body.Elements.AddRange(Content.Select(x => x?.Render(new RenderContext(this))));
            html.Body.Scripts = Scripts.Values.ToList();
            
            html.Head.CssLinks = CssLinks.Where(x => x != null).Select(x => x.ToString());
            html.Head.ScriptLinks = HeaderScriptLinks.Where(x => x != null).Select(x => x.ToString());

            return html;
        }
    }
}
