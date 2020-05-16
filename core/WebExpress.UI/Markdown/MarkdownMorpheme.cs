using System.Text;

namespace WebExpress.UI.Markdown
{
    internal class MarkdownMorpheme
    {
        /// <summary>
        /// Liefert doer setzt den Type
        /// </summary>
        public MarkdownMorphemeState Type { get; set; }

        /// <summary>
        /// Liefert oder setzt den Text
        /// </summary>
        public StringBuilder Text { get; private set; } = new StringBuilder();

        /// <summary>
        /// Erweitert das Morphem
        /// </summary>
        /// <param name="text">Der Text</param>
        public void Append(string text)
        {
            if (Text.Length > 0)
            {
                Text.Append(" ");
            }

            Text.Append(text?.Trim());
        }
    }
}
