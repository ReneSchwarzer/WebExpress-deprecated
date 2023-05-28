namespace WebExpress.UI.WebControl
{
    public abstract class PropertySpacing : IProperty
    {
        /// <summary>
        /// Die möglichen Abstände
        /// </summary>
        public enum Space { None, Null, One, Two, Three, Four, Five, Auto };

        /// <summary>
        /// Der obere Abstand
        /// </summary>
        public Space Top { get; private set; }

        /// <summary>
        /// Der untere Abstand
        /// </summary>
        public Space Bottom { get; private set; }

        /// <summary>
        /// Der linke Abstand
        /// </summary>
        public Space Left { get; private set; }

        /// <summary>
        /// Der rechte Abstand
        /// </summary>
        public Space Right { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public PropertySpacing()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="size">Die Abstände</param>
        public PropertySpacing(Space size)
        {
            Top = Bottom = Left = Right = size;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="horizontal">Der horzontale Abstand</param>
        /// <param name="vertical">Der vertikale Abstand</param>
        public PropertySpacing(Space horizontal, Space vertical)
        {
            Left = Right = horizontal;
            Top = Bottom = vertical;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="left">Der linke Abstand</param>
        /// <param name="right">Der rechte Abstand</param>
        /// <param name="top">Der obere Abstand</param>
        /// <param name="bottom">Der untere Abstand</param>
        public PropertySpacing(Space left, Space right, Space top, Space bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        /// <summary>
        /// Konvertiert eine Abstandsangabe in einen String
        /// </summary>
        /// <param name="size"></param>
        /// <returns>Die Stringrepräsentation</returns>
        protected static string ConvertSize(Space size)
        {
            return size switch
            {
                Space.Null => "0",
                Space.One => "1",
                Space.Two => "2",
                Space.Three => "3",
                Space.Four => "4",
                Space.Five => "5",
                Space.Auto => "auto",
                _ => string.Empty,
            };
        }

        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        protected string ToClass(string prefix)
        {
            if (Top == Bottom && Top == Left && Top == Right && Top == Space.None)
            {
                return null;
            }
            if (Top == Bottom && Top == Left && Top == Right)
            {
                return prefix + "-" + ConvertSize(Top);
            }
            // 
            else if (Top == Bottom && Left == Right && Top == Space.None && Left != Space.None)
            {
                return prefix + "x-" + ConvertSize(Left);
            }
            else if (Top == Bottom && Left == Right && Top != Space.None && Left == Space.None)
            {
                return prefix + "y-" + ConvertSize(Top);
            }
            else if (Top == Bottom && Left == Right && Top != Space.None && Left != Space.None)
            {
                return prefix + "x-" + ConvertSize(Left) + " " + prefix + "y-" + ConvertSize(Top);
            }
            //
            else if (Top != Space.None && Bottom == Space.None && Left == Space.None && Right == Space.None)
            {
                return prefix + "t-" + ConvertSize(Top);
            }
            else if (Top != Space.None && Bottom != Space.None && Left == Space.None && Right == Space.None)
            {
                return prefix + "t -" + ConvertSize(Top) + " " + prefix + "b-" + ConvertSize(Bottom);
            }
            else if (Top != Space.None && Bottom == Space.None && Left != Space.None && Right == Space.None)
            {
                return prefix + "t-" + ConvertSize(Top) + " " + prefix + "s-" + ConvertSize(Left);
            }
            else if (Top != Space.None && Bottom == Space.None && Left == Space.None && Right != Space.None)
            {
                return prefix + "t-" + ConvertSize(Top) + " " + prefix + "e-" + ConvertSize(Right);
            }
            else if (Top != Space.None && Bottom != Space.None && Left != Space.None && Right == Space.None)
            {
                return prefix + "t-" + ConvertSize(Top) + " " + prefix + "b-" + ConvertSize(Bottom) + " " + prefix + "s-" + ConvertSize(Left);
            }
            else if (Top != Space.None && Bottom != Space.None && Left == Space.None && Right != Space.None)
            {
                return prefix + "t-" + ConvertSize(Top) + " " + prefix + "b-" + ConvertSize(Bottom) + " " + prefix + "e-" + ConvertSize(Right);
            }
            else if (Top != Space.None && Bottom != Space.None && Left != Space.None && Right != Space.None)
            {
                return prefix + "t-" + ConvertSize(Top) + " " + prefix + "b-" + ConvertSize(Bottom) + " " + prefix + "s-" + ConvertSize(Left) + " " + prefix + "e-" + ConvertSize(Right);
            }
            //
            else if (Top == Space.None && Bottom != Space.None && Left == Space.None && Right == Space.None)
            {
                return prefix + "b-" + ConvertSize(Bottom);
            }
            else if (Top == Space.None && Bottom != Space.None && Left != Space.None && Right == Space.None)
            {
                return prefix + "b-" + ConvertSize(Bottom) + " " + prefix + "s-" + ConvertSize(Left);
            }
            else if (Top == Space.None && Bottom != Space.None && Left == Space.None && Right != Space.None)
            {
                return prefix + "b-" + ConvertSize(Bottom) + " " + prefix + "e-" + ConvertSize(Right);
            }
            else if (Top == Space.None && Bottom != Space.None && Left != Space.None && Right != Space.None)
            {
                return prefix + "b-" + ConvertSize(Bottom) + " " + prefix + "s-" + ConvertSize(Left) + " " + prefix + "e-" + ConvertSize(Right);
            }
            //
            else if (Top == Space.None && Bottom == Space.None && Left != Space.None && Right == Space.None)
            {
                return prefix + "s-" + ConvertSize(Left);
            }
            else if (Top == Space.None && Bottom == Space.None && Left != Space.None && Right != Space.None)
            {
                return prefix + "s-" + ConvertSize(Left) + " " + prefix + "e-" + ConvertSize(Right);
            }
            //
            else if (Top == Space.None && Bottom == Space.None && Left == Space.None && Right != Space.None)
            {
                return prefix + "e-" + ConvertSize(Right);
            }

            return null;
        }

        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <returns>Die zum Spacing gehörende CSS-KLasse</returns>
        public abstract string ToClass();

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
