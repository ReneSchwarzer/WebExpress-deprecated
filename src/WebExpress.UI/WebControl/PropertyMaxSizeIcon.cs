using System.Text;

namespace WebExpress.UI.WebControl
{
    public class PropertyMaxSizeIcon : PropertySizeIcon
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="size">Die Größe</param>
        /// <param name="unit">Die Einheit</param>
        public PropertyMaxSizeIcon(int size, TypeSizeUnit unit)
            : base(size, unit)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="width">Die Weite in Pixel</param>
        /// <param name="height">Die Höhe in Pixel</param>
        /// <param name="unit">Die Einheit</param>
        public PropertyMaxSizeIcon(int width, int height, TypeSizeUnit unit)
            : base(width, height, unit)
        {
        }

        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <returns>Die zur Farbe gehörende CSS-KLasse</returns>
        public override string ToClass()
        {
            return null;
        }

        /// <summary>
        /// Umwandlung in einen CSS-Style
        /// </summary>
        /// <returns>Das zur Farbe gehörende CSS-Style</returns>
        public override string ToStyle()
        {
            var style = new StringBuilder();

            var unit = Unit switch
            {
                TypeSizeUnit.Pixel => "px",
                TypeSizeUnit.Percent => "%",
                TypeSizeUnit.Em => "em",
                TypeSizeUnit.Rem => "rem",
                _ => string.Empty
            };

            if (Width > -1)
            {
                style.Append($"max-width: {Width}{unit}; ");
            }

            if (Height > -1)
            {
                style.Append($"max-height: {Height}{unit};");
            }

            return style.ToString();
        }

    }
}
