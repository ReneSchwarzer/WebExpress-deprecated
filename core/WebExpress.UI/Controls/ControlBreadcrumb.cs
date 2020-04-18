using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    public class ControlBreadcrumb : Control
    {
        /// <summary>
        /// Liefert oder setzt die Uri
        /// </summary>
        public IUri Uri { get; set; }

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
        /// <param name="uri">Der Verzeichnispfad</param>
        public ControlBreadcrumb(IPage page, string id, IUri uri)
            : base(page, id)
        {
            Uri = uri;
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
            var classes = new List<string>
            {
                Class,
                "breadcrumb"
            };

            var html = new HtmlElementUl()
            {
                Class = string.Join(" ", classes.Where(x => !string.IsNullOrWhiteSpace(x)))
            };

            for (int i = 1; i <= Page.Uri.Path.Count ; i++)
            {
                var path = Page.Uri.Take(i);

                html.Elements.Add
                (
                    new HtmlElementLi
                    (
                        new HtmlElementA(path.Display) { Href = path.ToString() }
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
