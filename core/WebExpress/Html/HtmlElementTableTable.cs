using System.Collections.Generic;
using System.Text;

namespace WebExpress.Html
{
    /// <summary>
    /// Markiert eine Tabelle, d.h. Daten mit mehr als einer Dimension.
    /// </summary>
    public class HtmlElementTableTable : HtmlElement, IHtmlElementTable
    {
        /// <summary>
        /// Liefert oder setzt die Spalten
        /// </summary>
        public HtmlElementTableTr Columns { get; set; }

        /// <summary>
        /// Liefert oder setzt die Zeilen
        /// </summary>
        public List<HtmlElementTableTr> Rows { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementTableTable()
            : base("table")
        {
            Columns = new HtmlElementTableTr();
            Rows = new List<HtmlElementTableTr>();
        }

        /// <summary>
        /// In String konvertieren unter Zuhilfenahme eines StringBuilder
        /// </summary>
        /// <param name="builder">Der StringBuilder</param>
        /// <param name="deep">Die Aufrufstiefe</param>
        public override void ToString(StringBuilder builder, int deep)
        {
            ToPreString(builder, deep);

            var column = new HtmlElementTableThead(Columns);
            column.ToString(builder, deep + 1);

            var body = new HtmlElementTableTbody(Rows);
            body.ToString(builder, deep + 1);

            ToPostString(builder, deep);
        }
    }
}
