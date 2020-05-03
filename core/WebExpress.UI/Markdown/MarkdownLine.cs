using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("WebExpress.Test")]
namespace WebExpress.UI.Markdown
{
    internal class MarkdownLine
    {
        /// <summary>
        /// Liefert oder setzt den Text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Liefert oder setzt die Fragmente der Zeile
        /// </summary>
        public List<MarkdownFragment> Fragments { get; set; }
    }
}
