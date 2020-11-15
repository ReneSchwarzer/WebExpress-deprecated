using System;

namespace WebExpress.Html
{
    /// <summary>
    /// Steht für einen Bitmap-Bereich, der von Skripts verwendet werden kann, um beispielsweise Diagramme, Spielegraphiken oder andere visuellen Effekte dynamisch darzustellen.
    /// </summary>
    public class HtmlElementScriptingCanvas : HtmlElement, IHtmlElementScripting
    {
        /// <summary>
        /// Liefert oder setzt die Weite
        /// </summary>
        public int Width
        {
            get => Convert.ToInt32(GetAttribute("width"));
            set => SetAttribute("width", value.ToString());
        }

        /// <summary>
        /// Liefert oder setzt die Weite
        /// </summary>
        public int Height
        {
            get => Convert.ToInt32(GetAttribute("height"));
            set => SetAttribute("height", value.ToString());
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementScriptingCanvas()
            : base("canvas", false)
        {

        }
    }
}
