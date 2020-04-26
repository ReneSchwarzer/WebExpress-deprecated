using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    /// <summary>
    /// Grid aus 12-Zellen pro Zeile
    /// </summary>
    public class ControlGrid : Control
    {
        /// <summary>
        /// Liefert oder setzt den Inhalt
        /// </summary>
        private Dictionary<int, List<Control>> Content { get; set; }

        /// <summary>
        /// Fester oder Anpassung an die gesammte Breite
        /// </summary>
        public bool Fluid { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlGrid(IPage page, string id = null)
            : base(page, id)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Content = new Dictionary<int, List<Control>>();
        }

        /// <summary>
        /// Fügt ein Inhalt hinzu
        /// </summary>
        /// <param name="row">Die Zeile</param>
        /// <param name="content">Der Inhalt</param>
        public void Add(int row, params Control[] content)
        {
            if (!Content.ContainsKey(row))
            {
                Content[row] = new List<Control>();
            }

            var div = new ControlPanel(Page, content) { Classes = new List<string>(new[] { "col-sm" }) };

            Content[row].Add(div);
        }

        /// <summary>
        /// Fügt ein Inhalt hinzu
        /// </summary>
        /// <param name="row">Die Zeile</param>
        /// <param name="spanSize">Die Anzalh der zu überspannenden Zellen</param>
        /// <param name="content">Der Inhalt</param>
        public void Add(int row, int spanSize, params Control[] content)
        {
            if (!Content.ContainsKey(row))
            {
                Content[row] = new List<Control>();
            }

            var div = new ControlPanel(Page, content) { Classes = new List<string>(new[] { "col-sm-" + spanSize }) };

            Content[row].Add(div);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode ToHtml()
        {
            if (!Fluid)
            {
                Classes.Add("container");
            }
            else
            {
                Classes.Add("container-fluid");
            }

            var rows = Content.OrderBy(x => x.Key).Select(x => x.Value);

            var html = new HtmlElementTextContentDiv(from x in rows select new HtmlElementTextContentDiv(x.Select(y => y.ToHtml())) { Class = "row" })
            {
                ID = ID,
                Class = GetClasses(),
                Style = GetStyles(),
                Role = Role
            };

            return html;
        }
    }
}
