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
        /// Konstruktor
        /// </summary>
        public PropertySpacing()
        {

        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="size">Die Abstände</param>
        public PropertySpacing(Space size)
        {
            Top = Bottom = Left = Right = size;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="horizontal">Der horzontale Abstand</param>
        /// <param name="vertical">Der vertikale Abstand</param>
        public PropertySpacing(Space horizontal, Space vertical)
        {
            Left = Right = horizontal;
            Top = Bottom = vertical;
        }

        /// <summary>
        /// Konstruktor
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
        protected string ConvertSize(Space size)
        {
            switch (size)
            {
                case Space.Null:
                    return "0";
                case Space.One:
                    return "1";
                case Space.Two:
                    return "2";
                case Space.Three:
                    return "3";
                case Space.Four:
                    return "4";
                case Space.Five:
                    return "5";
                case Space.Auto:
                    return "auto";
            }

            return string.Empty;
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
            else if (Top != Space.None && Bottom != Space.None && Left == Space.None && Right == Space.None)
            {
                return prefix + "t-" + ConvertSize(Top);
            }
            else if (Top != Space.None && Bottom != Space.None && Left == Space.None && Right == Space.None)
            {
                return prefix + "t -" + ConvertSize(Top) + " " + prefix + "b-" + ConvertSize(Bottom);
            }
            else if (Top != Space.None && Bottom == Space.None && Left != Space.None && Right == Space.None)
            {
                return prefix + "t-" + ConvertSize(Top) + " " + prefix + "l-" + ConvertSize(Left);
            }
            else if (Top != Space.None && Bottom == Space.None && Left == Space.None && Right != Space.None)
            {
                return prefix + "t-" + ConvertSize(Top) + " " + prefix + "r-" + ConvertSize(Right);
            }
            else if (Top != Space.None && Bottom != Space.None && Left != Space.None && Right == Space.None)
            {
                return prefix + "t-" + ConvertSize(Top) + " " + prefix + "b-" + ConvertSize(Bottom) + " " + prefix + "l-" + ConvertSize(Left);
            }
            else if (Top != Space.None && Bottom != Space.None && Left == Space.None && Right != Space.None)
            {
                return prefix + "t-" + ConvertSize(Top) + " " + prefix + "b-" + ConvertSize(Bottom) + " " + prefix + "r-" + ConvertSize(Right);
            }
            else if (Top != Space.None && Bottom != Space.None && Left != Space.None && Right != Space.None)
            {
                return prefix + "t-" + ConvertSize(Top) + " " + prefix + "b-" + ConvertSize(Bottom) + " " + prefix + "l-" + ConvertSize(Left) + " " + prefix + "r-" + ConvertSize(Right);
            }
            //
            else if (Top == Space.None && Bottom != Space.None && Left == Space.None && Right == Space.None)
            {
                return prefix + "b-" + ConvertSize(Bottom);
            }
            else if (Top == Space.None && Bottom != Space.None && Left != Space.None && Right == Space.None)
            {
                return prefix + "b-" + ConvertSize(Bottom) + " " + prefix + "l-" + ConvertSize(Left);
            }
            else if (Top == Space.None && Bottom != Space.None && Left == Space.None && Right != Space.None)
            {
                return prefix + "b-" + ConvertSize(Bottom) + " " + prefix + "r-" + ConvertSize(Right);
            }
            else if (Top == Space.None && Bottom != Space.None && Left != Space.None && Right != Space.None)
            {
                return prefix + "b-" + ConvertSize(Bottom) + " " + prefix + "l-" + ConvertSize(Left) + " " + prefix + "r-" + ConvertSize(Right);
            }
            //
            else if (Top == Space.None && Bottom == Space.None && Left != Space.None && Right == Space.None)
            {
                return prefix + "l-" + ConvertSize(Left);
            }
            else if (Top == Space.None && Bottom == Space.None && Left != Space.None && Right != Space.None)
            {
                return prefix + "l-" + ConvertSize(Left) + " " + prefix + "r-" + ConvertSize(Right);
            }
            //
            else if (Top == Space.None && Bottom == Space.None && Left == Space.None && Right != Space.None)
            {
                return prefix + "r-" + ConvertSize(Right);
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
