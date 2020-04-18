using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    /// <summary>
    /// Zeile der Tabelle
    /// </summary>
    public class ControlTableColumn : Control
    {
        /// <summary>
        /// Liefert oder setzt das Layout
        /// </summary>
        public TypesLayoutTableRow Layout { get; set; }

        /// <summary>
        /// Liefert oder setzt den Text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Liefert oder setzt das Icon
        /// </summary>
        public Icon Icon { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlTableColumn(IPage page, string id)
            : base(page, id)
        {

        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode ToHtml()
        {
            var classes = new List<string>
            {
                Class
            };

            switch (Layout)
            {
                case TypesLayoutTableRow.Primary:
                    classes.Add("table-primary");
                    break;
                case TypesLayoutTableRow.Secondary:
                    classes.Add("table-secondary");
                    break;
                case TypesLayoutTableRow.Success:
                    classes.Add("table-success");
                    break;
                case TypesLayoutTableRow.Info:
                    classes.Add("table-info");
                    break;
                case TypesLayoutTableRow.Warning:
                    classes.Add("table-warning");
                    break;
                case TypesLayoutTableRow.Danger:
                    classes.Add("table-danger");
                    break;
                case TypesLayoutTableRow.Light:
                    classes.Add("table-light");
                    break;
                case TypesLayoutTableRow.Dark:
                    classes.Add("table-dark");
                    break;
            }

            var html = new HtmlElementDiv()
            {
                ID = ID,
                Class = string.Join(" ", classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Role = Role
            };

            if (Icon != Icon.None && !string.IsNullOrWhiteSpace(Text))
            {
                html.Elements.Add(new HtmlElementSpan() { Class = Icon.ToClass() });

                html.Elements.Add(new HtmlNbsp());
            }
            else if (Icon != Icon.None && string.IsNullOrWhiteSpace(Text))
            {
                html.AddClass(Icon.ToClass());
            }

            if (!string.IsNullOrWhiteSpace(Text))
            {
                html.Elements.Add(new HtmlText(Text));
            }

            return new HtmlElementTh(html)
            {
                ID = ID,
                Class = string.Join(" ", classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Role = Role
            };
        }
    }
}
