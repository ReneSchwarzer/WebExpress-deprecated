using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFA.WebServer.Html
{
    public class HtmlElementScript : HtmlElement
    {
        /// <summary>
        /// Liefert oder setzt den Text
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Liefert oder setzt die Scriptsprache
        /// </summary>
        public string Language
        {
            get { return GetAttribute("language"); }
            set { SetAttribute("language", value); }
        }

        /// <summary>
        /// Liefert oder setzt den Medientyp
        /// </summary>
        public string Type
        {
            get { return GetAttribute("type"); }
            set { SetAttribute("type", value); }
        }

        /// <summary>
        /// Liefert oder setzt den Link auf die Scriptdatei
        /// </summary>
        public string Src
        {
            get { return GetAttribute("src"); }
            set { SetAttribute("src", value); }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementScript()
            : base("script")
        {
            Type = "text/javascript";
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="code">Der Text</param>
        public HtmlElementScript(string code)
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
            ToPreString(builder, deep);

            if (!string.IsNullOrWhiteSpace(Code))
            {
#if DEBUG
                builder.Append(Code);
#else
                builder.Append(Code.Replace("\r", "").Replace("\n", ""));
#endif
            }

            ToPostString(builder, deep, false);
        }
    }
}
