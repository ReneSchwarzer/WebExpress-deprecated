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
        /// Returns or sets the text.
        /// </summary>
        public StringBuilder Text { get; private set; } = new StringBuilder();

        /// <summary>
        /// Erweitert das Morphem
        /// </summary>
        /// <param name="text">The text.</param>
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
