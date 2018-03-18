using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFA.WebServer.Html
{
    public class HtmlElementStyle : HtmlElement
    {
        /// <summary>
        /// Liefert oder setzt den Text
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementStyle()
            : base("style")
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="code">Der Text</param>
        public HtmlElementStyle(string code)
            : this()
        {
            Code = code;
        }

        /// <summary>
        /// In String konvertieren unter Zuhilfenahme eines StringBuilder
        /// </summary>
        /// <param name="builder">Der StringBuilder</param>
        /// <param name="deep">Die Aufrufstiefe</param>
        public override void ToString(StringBuilder builder, int deep)
        {
            builder.Append(string.Empty.PadRight(deep));
            builder.Append("<");
            builder.Append(ElementName);
            builder.Append(">");

            if (!string.IsNullOrWhiteSpace(Code))
            {
                builder.Append("\n");
                builder.Append(Code);
                builder.Append("\n");
            }
            builder.Append(string.Empty.PadRight(deep));
            builder.Append("</");
            builder.Append(ElementName);
            builder.Append(">");
            builder.Append("\n");
        }
    }
}
