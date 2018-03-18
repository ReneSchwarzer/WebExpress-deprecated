using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFA.WebServer.Html
{
    /// <summary>
    /// Liste von HTML-Elementen
    /// </summary>
    public class HtmlList : IHtmlNode
    {
        /// <summary>
        /// Liefert die Elemente
        /// </summary>
        public List<IHtmlNode> Elements { get; private set; }
    
        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlList()
        {
            Elements = new List<IHtmlNode>();
        }
    
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlList(params IHtmlNode[] nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="nodes">Der Inhalt</param>
        public HtmlList(IEnumerable<IHtmlNode> nodes)
            : this()
        {
            Elements.AddRange(nodes);
        }

        /// <summary>
        /// In String konvertieren unter Zuhilfenahme eines StringBuilder
        /// </summary>
        /// <param name="builder">Der StringBuilder</param>
        /// <param name="deep">Die Aufrufstiefe</param>
        /// <param name="nl">Abschlustag auf neuer Zeile beginnen</param>
        public void ToString(StringBuilder builder, int deep)
        {
            foreach (var v in Elements)
            {
                v.ToString(builder, deep);
            }
        }
    }
}
