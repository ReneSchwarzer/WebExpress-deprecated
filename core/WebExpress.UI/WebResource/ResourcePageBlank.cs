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
            var module = ModuleManager.GetModule("webexpress");
            if (module != null)
            {
                CssLinks.Add(new UriResource(module.ContextPath, new UriRelative("/assets/css/fontawesome.min.css")));
                CssLinks.Add(new UriResource(module.ContextPath, new UriRelative("/assets/css/bootstrap.min.css")));
                CssLinks.Add(new UriResource(module.ContextPath, new UriRelative("/assets/css/express.css")));
                CssLinks.Add(new UriResource(module.ContextPath, new UriRelative("/assets/css/express.form.css")));
                CssLinks.Add(new UriResource(module.ContextPath, new UriRelative("/assets/css/solid.css")));
                CssLinks.Add(new UriResource(module.ContextPath, new UriRelative("/assets/css/summernote-bs4.min.css")));

                HeaderScriptLinks.Add(new UriResource(module.ContextPath, new UriRelative("/assets/js/jquery-3.5.1.min.js")));
                HeaderScriptLinks.Add(new UriResource(module.ContextPath, new UriRelative("/assets/js/popper.min.js")));
                HeaderScriptLinks.Add(new UriResource(module.ContextPath, new UriRelative("/assets/js/bootstrap.min.js")));
                HeaderScriptLinks.Add(new UriResource(module.ContextPath, new UriRelative("/assets/js/summernote-bs4.min.js")));
            }
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
            html.Head.CssLinks = CssLinks.Where(x => x != null).Select(x => x.ToString());
            html.Head.ScriptLinks = HeaderScriptLinks.Where(x => x != null).Select(x => x.ToString());
            html.Head.Favicons = Favicons.Select(x => new Favicon(new UriRelative(x.Url).ToString(), x.Mediatype));
            html.Head.Meta = Meta;
            html.Head.Scripts = HeaderScripts;
            html.Body.Elements.AddRange(Content.Select(x => x?.Render(new RenderContext(this))));
            html.Body.Scripts = Scripts.Values.ToList();

            return html;
        }
    }
}
