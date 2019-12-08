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
        /// In String konvertieren
        /// </summary>
        /// <returns>Das Objekt als String</returns>
        public override string ToString()
        {
            var html = new HtmlElementHtml();
            html.Head.Title = Title;
            html.Head.Styles = Styles;
            html.Head.CssLinks = CssLinks;
            html.Head.ScriptLinks = HeaderScriptLinks;
            html.Head.Favicons = Favicons;
            html.Head.Meta = Meta;
            html.Body.Elements.AddRange(Content.Select(x => x.ToHtml()));

            return html.ToString();
        }
    }
}
