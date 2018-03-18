using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFA.WebServer.Html
{
    /// <summary>
    /// Ein Attribut ohne wert
    /// z.B. required in input <input required>
    /// </summary>
    public class HtmlAttributeNoneValue : IHtmlAttribute
    {
        /// <summary>
        /// Liefert oder setzt den Namen des Attributes
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlAttributeNoneValue()
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="name">Der Name</param>
        public HtmlAttributeNoneValue(string name)
        {
            Name = name;
        }

        /// <summary>
        /// In String konvertieren unter Zuhilfenahme eines StringBuilder
        /// </summary>
        /// <param name="builder">Der StringBuilder</param>
        /// <param name="deep">Die Aufrufstiefe</param>
        public virtual void ToString(StringBuilder builder, int deep)
        {
            builder.Append(Name);
        }
    }
}
