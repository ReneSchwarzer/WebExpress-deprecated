using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    /// <summary>
    /// Zeile der Tabelle
    /// </summary>
    public class ControlTableRow : Control
    {
        public TypesLayoutTableRow Layout { get; set; }
        public List<Control> Cells { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlTableRow(string id = null)
            : base(id)
        {
            Cells = new List<Control>();
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            switch (Layout)
            {
                case TypesLayoutTableRow.Primary:
                    Classes.Add("table-primary");
                    break;
                case TypesLayoutTableRow.Secondary:
                    Classes.Add("table-secondary");
                    break;
                case TypesLayoutTableRow.Success:
                    Classes.Add("table-success");
                    break;
                case TypesLayoutTableRow.Info:
                    Classes.Add("table-info");
                    break;
                case TypesLayoutTableRow.Warning:
                    Classes.Add("table-warning");
                    break;
                case TypesLayoutTableRow.Danger:
                    Classes.Add("table-danger");
                    break;
                case TypesLayoutTableRow.Light:
                    Classes.Add("table-light");
                    break;
                case TypesLayoutTableRow.Dark:
                   Classes.Add("table-dark");
                    break;
            }

            return new HtmlElementTableTr(from c in Cells select new HtmlElementTableTd(c.Render(context)))
            {
                ID = ID,
                Class = string.Join(" ", Classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                Role = Role
            };
        }
    }
}
