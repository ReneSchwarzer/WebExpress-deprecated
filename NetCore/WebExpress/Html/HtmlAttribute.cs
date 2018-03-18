using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFA.WebServer.Html
{
    public class HtmlAttribute : IHtmlAttribute
    {
        /// <summary>
        /// Liefert oder setzt den Namen des Attributes
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Liefert oder setzt den Wert
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlAttribute()
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="name">Der Name</param>
        public HtmlAttribute(string name)
        {
            Name = name;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="name">Der Name</param>
        /// <param name="value">Der Wert</param>
        public HtmlAttribute(string name, string value)
        {
            Name = name;
            Value = value;
        }

        /// <summary>
        /// In String konvertieren unter Zuhilfenahme eines StringBuilder
        /// </summary>
        /// <param name="builder">Der StringBuilder</param>
        /// <param name="deep">Die Aufrufstiefe</param>
        public virtual void ToString(StringBuilder builder, int deep)
        {
            builder.Append(Name);
            builder.Append("=\"");
            builder.Append(Value);
            builder.Append("\"");
        }
    }
}
