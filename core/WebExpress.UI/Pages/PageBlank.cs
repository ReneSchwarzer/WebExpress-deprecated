using System.Collections.Generic;
using System.Linq;
using WebExpress.Pages;
using WebExpress.UI.Controls;
using WebServer.Html;

namespace WebExpress.UI.Pages
{
    public class PageBlank : Page
    {
        /// <summary>
        /// Liefert oder setzt den Inhalt
        /// </summary>
        protected List<Control> Content { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public PageBlank()
        {
            Content = new List<Control>();

            CssLinks.Add("/Assets/css/fontawesome.css");
            CssLinks.Add("/Assets/css/bootstrap.min.css");
            CssLinks.Add("/Assets/css/express.css");
            CssLinks.Add("/Assets/css/express.form.css");
            CssLinks.Add("/Assets/css/solid.css");
            CssLinks.Add("/Assets/css/summernote-bs4.css");

            HeaderScriptLinks.Add("/Assets/js/jquery-3.3.1.min.js");
            HeaderScriptLinks.Add("/Assets/js/popper.min.js");
            HeaderScriptLinks.Add("/Assets/js/bootstrap.min.js");
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        public override void Init()
        {
            base.Init();
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <returns>Die Seite als HTML-Baum</returns>
        public override IHtmlNode ToHtml()
        {
            var html = new HtmlElementHtml();
            html.Head.Title = Title;
            html.Head.Base = Context.UrlBasePath;
            html.Head.Styles = Styles.Select(x => new Path(Context, x).ToString());;
            html.Head.CssLinks = CssLinks.Select(x => new Path(Context, x).ToString());
            html.Head.ScriptLinks = HeaderScriptLinks.Select(x => new Path(Context, x).ToString());
            html.Head.Favicons = Favicons.Select(x => new Html.Favicon(new Path(Context, x.Url).ToString(), x.Mediatype));
            html.Head.Meta = Meta;
            html.Head.Scripts = HeaderScripts;
            html.Body.Elements.AddRange(Content.Select(x => x.ToHtml()));
            html.Body.Scripts = Scripts.Values.ToList();

            return html;
        }
    }
}
