using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("WebExpress.Test")]
namespace WebExpress.UI.Markdown
{

    internal class MarkdownToken
    {
        public int Position { get; set; }
        public string Text { get; set; }
        public char Token => Text[Position];

        /// <summary>
        /// Prüft ob das Ende der Zeile erreicht wurde
        /// </summary>
        public bool HasNext => Position + 1 < Text.Length;

        /// <summary>
        /// Prüft ob das Ende der Zeile überschritten wurde
        /// </summary>
        public bool EoL => Position >= Text.Length;

        /// <summary>
        /// Prüft ob ein Inhalt verhanden ist
        /// </summary>
        public bool Empty => string.IsNullOrEmpty(Text);

        public bool Next()
        {
            if (!HasNext) return false;

            Position++;

            return true;
        }

    }
}
