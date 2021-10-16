using System.Text;

namespace WebExpress.UI.WebControl
{
    public class PropertySizeIcon : IProperty
    {
        /// <summary>
        /// Die Einhelten
        /// </summary>
        public TypeSizeUnit Unit { get; protected set; } = TypeSizeUnit.Pixel;

        /// <summary>
        /// Die System-Größe
        /// </summary>
        public int Width { get; protected set; } = -1;

        /// <summary>
        /// Die benutzerdefinierte Größe
        /// </summary>
        public int Height { get; protected set; } = -1;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="size">Die Größe</param>
        /// <param name="unit">Die Einheit</param>
        public PropertySizeIcon(int size, TypeSizeUnit unit)
        {
            Width = size;
            Height = size;
            Unit = unit;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="width">Die Weite in Pixel</param>
        /// <param name="height">Die Höhe in Pixel</param>
        /// <param name="unit">Die Einheit</param>
        public PropertySizeIcon(int width, int height, TypeSizeUnit unit)
        {
            Width = width;
            Height = height;
            Unit = unit;
        }

        /// <summary>
        /// Umwandlung in eine CSS-Klasse
        /// </summary>
        /// <returns>Die zur Farbe gehörende CSS-KLasse</returns>
        public virtual string ToClass()
        {
            return null;
        }

        /// <summary>
        /// Umwandlung in einen CSS-Style
        /// </summary>
        /// <returns>Das zur Farbe gehörende CSS-Style</returns>
        public virtual string ToStyle()
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
                style.Append($"width: {Width}{unit}; ");
            }

            if (Height > -1)
            {
                style.Append($"height: {Height}{unit};");
            }

            return style.ToString();
        }

    }
}
