using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFA.WebServer.Html
{
    public class HtmlElementTable : HtmlElement
    {
        /// <summary>
        /// Liefert oder setzt die Spalten
        /// </summary>
        public HtmlElementTr Columns { get; set; }

        /// <summary>
        /// Liefert oder setzt die Zeilen
        /// </summary>
        public List<HtmlElementTr> Rows { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementTable()
            : base("table")
        {
            Columns = new HtmlElementTr();
            Rows = new List<HtmlElementTr>();
        }

        /// <summary>
        /// In String konvertieren unter Zuhilfenahme eines StringBuilder
        /// </summary>
        /// <param name="builder">Der StringBuilder</param>
        /// <param name="deep">Die Aufrufstiefe</param>
        public override void ToString(StringBuilder builder, int deep)
        {
            ToPreString(builder, deep);

            var column = new HtmlElementTHead(Columns);
            column.ToString(builder, deep + 1);

            var body = new HtmlElementTBody(Rows);
            body.ToString(builder, deep + 1);

            ToPostString(builder, deep);
        }
    }
}
