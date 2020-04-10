using System.Collections.Generic;
using WebExpress.Pages;
using WebServer.Html;

namespace WebExpress.UI.Controls
{
    public class ControlBreadcrumb : Control
    {
        /// <summary>
        /// Liefert oder setzt den Verzeichnispfad
        /// </summary>
        public Path Path { get; set; }

        /// <summary>
        /// Liefert oder setzt das Rootelement
        /// </summary>
        public string EmptyName { get; set; }

        /// <summary>
        /// Ersetzungen 
        /// </summary>
        private Dictionary<string, string> Replace { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlBreadcrumb(IPage page, string id = null)
            : base(page, id)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="path">Der Verzeichnispfad</param>
        public ControlBreadcrumb(IPage page, string id, Path path)
            : base(page, id)
        {
            Path = path;
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Replace = new Dictionary<string, string>();
            //ID = "path";
            Class = "";
        }

        ///// <summary>
        ///// Füght eine neue Ersetzung hinzu
        ///// </summary>
        ///// <param name="key">Der Schlüssel</param>
        ///// <param name="value">Die Ersetzung</param>
        //public void AddReplace(string key, string value)
        //{
        //    if (!Replace.ContainsKey(key.ToLower()))
        //    {
        //        Replace.Add(key.ToLower(), value);
        //    }
        //    else
        //    {
        //        Replace[key.ToLower()] = value;
        //    }
        //}

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode ToHtml()
        {
            var addCss = "breadcrumb";

            var html = new HtmlElementUl() { Class = Class + addCss };

            var basePath = new Path(Page.Context);

            foreach (var v in Path.Items)
            {
                basePath = new Path(Page.Context, v.Name, basePath, v.Fragment);

                html.Elements.Add
                (
                    new HtmlElementLi
                    (
                        new HtmlElementA(v.Name) { Href = basePath.ToString() }
                    )
                    {
                        Class = "breadcrumb-item"
                    }
                );
            }

            if (Path.Items.Count == 0)
            {
                html.Elements.Add
                (
                    new HtmlElementLi
                    (
                        new HtmlElementA(EmptyName) { Href = "/" }
                    )
                    {
                        Class = "breadcrumb-item"
                    }
                );
            }

            return html;
        }
    }
}
