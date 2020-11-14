namespace WebExpress.UI.Controls
{
    public interface IControlButton : IControl
    {
        /// <summary>
        /// Liefert oder setzt die Farbe der Schaltfläche
        /// </summary>
        PropertyColorButton BackgroundColor { get; set; }

        /// <summary>
        /// Liefert oder setzt die Größe
        /// </summary>
        TypeSizeButton Size { get; set; }

        /// <summary>
        /// Liefert oder setzt die Outline-Eigenschaft
        /// </summary>
        bool Outline { get; set; }

        /// <summary>
        /// Liefert oder setzt ob die Schaltfläche die volle Breite einnehmen soll
        /// </summary>
        TypeBlockButton Block { get; set; }

        /// <summary>
        /// Liefert oder setzt den Text der TextBox
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// Liefert oder setzt den Wert
        /// </summary>
        string Value { get; set; }

        /// <summary>
        /// Liefert oder setzt das Icon
        /// </summary>
        PropertyIcon Icon { get; set; }

        /// <summary>
        /// Liefert oder setzt den Aktivierungsstatus der Schaltfläche
        /// </summary>
        TypeActive Active { get; set; }
    }
}