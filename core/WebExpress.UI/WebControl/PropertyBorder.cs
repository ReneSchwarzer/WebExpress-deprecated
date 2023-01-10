using System.Collections.Generic;

namespace WebExpress.UI.WebControl
{
    public class PropertyBorder : IProperty
    {
        /// <summary>
        /// Der obere Abstand
        /// </summary>
        public bool Top { get; private set; }

        /// <summary>
        /// Der untere Abstand
        /// </summary>
        public bool Bottom { get; private set; }

        /// <summary>
        /// Der linke Abstand
        /// </summary>
        public bool Left { get; private set; }

        /// <summary>
        /// Der rechte Abstand
        /// </summary>
        public bool Right { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public PropertyBorder()
        {
            Top = Bottom = Left = Right = false;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="showBorder">Bestimmt, ob ein einheitlicher Rahmen angezeigt werden soll</param>
        public PropertyBorder(bool showBorder = true)
        {
            Top = Bottom = Left = Right = showBorder;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="horizontal">Der horzontale Rahmen</param>
        /// <param name="vertical">Der vertikale Rahmen</param>
        public PropertyBorder(bool horizontal, bool vertical)
        {
            Left = Right = horizontal;
            Top = Bottom = vertical;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="left">Der linke Rahmen</param>
        /// <param name="right">Der rechte Rahmen</param>
        /// <param name="top">Der obere Rahmen</param>
        /// <param name="bottom">Der untere Rahmen</param>
        public PropertyBorder(bool left, bool right, bool top, bool bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <returns>Die zum Rahmen gehörende CSS-KLasse</returns>
        public string ToClass()
        {
            if (Top == Bottom && Top == Left && Top == Right && Top == false)
            {
                return "border-0";
            }
            else if (Top == Bottom && Top == Left && Top == Right && Top == true)
            {
                return "border";
            }

            var c = new List<string>();

            if (Top)
            {
                c.Add("border-top");
            }
            else
            {
                c.Add("border-top-0");
            }

            if (Right)
            {
                c.Add("border-right");
            }
            else
            {
                c.Add("border-right-0");
            }

            if (Bottom)
            {
                c.Add("border-bottom");
            }
            else
            {
                c.Add("border-bottom-0");
            }

            if (Left)
            {
                c.Add("border-left");
            }
            else
            {
                c.Add("border-left-0");
            }

            return string.Join(" ", c);
        }

        /// <summary>
        /// Umwandlung in einen CSS-Style
        /// </summary>
        /// <returns>Der zur Farbe gehörende CSS-Style</returns>
        public virtual string ToStyle()
        {
            return null;
        }
    }
}
