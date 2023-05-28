namespace WebExpress.UI.WebControl
{
    public interface IControlButton : IControl
    {
        /// <summary>
        /// Returns or sets the color. der Schaltfläche
        /// </summary>
        PropertyColorButton BackgroundColor { get; set; }

        /// <summary>
        /// Returns or sets the size.
        /// </summary>
        TypeSizeButton Size { get; set; }

        /// <summary>
        /// Returns or sets the outline property
        /// </summary>
        bool Outline { get; set; }

        /// <summary>
        /// Returns or sets whether the button should take up the full width.
        /// </summary>
        TypeBlockButton Block { get; set; }

        /// <summary>
        /// Returns or sets the text.
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// Returns or sets the value.
        /// </summary>
        string Value { get; set; }

        /// <summary>
        /// Returns or sets the icon.
        /// </summary>
        PropertyIcon Icon { get; set; }

        /// <summary>
        /// Returns or sets the activation status of the button.
        /// </summary>
        TypeActive Active { get; set; }
    }
}