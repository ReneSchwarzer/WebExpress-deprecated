using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFA.WebServer.Html
{
    public class HtmlText : IHtmlNode
    {
        /// <summary>
        /// Liefert oder setzt die Text
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlText()
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="value">Der Text</param>
        public HtmlText(string value)
        {
            Value = value;
        }

        /// <summary>
        /// In String konvertieren unter Zuhilfenahme eines StringBuilder
        /// </summary>
        /// <param name="builder">Der StringBuilder</param>
        /// <param name="deep">Die Aufrufstiefe</param>
        public virtual void ToString(StringBuilder builder, int deep)
        {
            builder.Append(Value);
        }
    }
}
