namespace WebExpress.UI.WebControl
{
    public class PropertySpacingPadding : PropertySpacing
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public PropertySpacingPadding()
        {

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="size">Die Abstände</param>
        public PropertySpacingPadding(Space size)
            : base(size)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="horizontal">Der horzontale Abstand</param>
        /// <param name="vertical">Der vertikale Abstand</param>
        public PropertySpacingPadding(Space horizontal, Space vertical)
            : base(horizontal, vertical)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="left">Der linke Abstand</param>
        /// <param name="right">Der rechte Abstand</param>
        /// <param name="top">Der obere Abstand</param>
        /// <param name="bottom">Der untere Abstand</param>
        public PropertySpacingPadding(Space left, Space right, Space top, Space bottom)
            : base(left, right, top, bottom)
        {
        }

        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <returns>Die zum Layout gehörende CSS-KLasse</returns>
        public override string ToClass()
        {
            return ToClass("p");
        }
    }
}
