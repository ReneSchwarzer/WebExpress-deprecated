using System;
using System.Collections.Generic;
using System.Text;

namespace WebExpress.UI.Controls
{
    public static class LayoutSchema
    {
        /// <summary>
        /// Hintergrundfarbe des Headers
        /// </summary>
        public static PropertyColorBackground HeaderBackground => new PropertyColorBackground(TypeColorBackground.Dark);

        /// <summary>
        /// Farbe des Headers-Titels
        /// </summary>
        public static PropertyColorText HeaderTitle => new PropertyColorText(TypeColorText.Light);

        /// <summary>
        /// Farbe des Headers-Links
        /// </summary>
        public static PropertyColorText HeaderNavigationLink => new PropertyColorText(TypeColorText.Light);

        /// <summary>
        /// Farbe des Headers-Titels
        /// </summary>
        public static PropertyColorBackground HeaderNavigationActiveBackground => new PropertyColorBackground();

        /// <summary>
        /// Farbe des Headers-Titels
        /// </summary>
        public static PropertyColorText HeaderNavigationActive => new PropertyColorText(TypeColorText.Info);

        /// <summary>
        /// Farbe des Headers-Titels
        /// </summary>
        public static PropertyColorText NavigationLink => new PropertyColorText(TypeColorText.Primary);

        /// <summary>
        /// Farbe des Headers-Titels
        /// </summary>
        public static PropertyColorBackground NavigationActiveBackground => new PropertyColorBackground(TypeColorBackground.Primary);

        /// <summary>
        /// Farbe des Headers-Titels
        /// </summary>
        public static PropertyColorText NavigationActive => new PropertyColorText(TypeColorText.Light);

        /// <summary>
        /// Farbe des Headers-Titels
        /// </summary>
        public static PropertyColorText Link => new PropertyColorText(TypeColorText.Dark);

        /// <summary>
        /// Hintergrundfarbe des Content-Bereiches
        /// </summary>
        public static PropertyColorBackground SidebarBackground => new PropertyColorBackground(TypeColorBackground.Light);

        /// <summary>
        /// Farbe des Headers-Titels
        /// </summary>
        public static PropertyColorBackground SidebarNavigationActiveBackground => new PropertyColorBackground(TypeColorBackground.Info);

        /// <summary>
        /// Farbe des Headers-Titels
        /// </summary>
        public static PropertyColorText SidebarNavigationActive => new PropertyColorText(TypeColorText.Light);

            /// <summary>
            /// Farbe des Headers-Titels
            /// </summary>
        public static PropertyColorText SidebarNavigationLink => new PropertyColorText(TypeColorText.Dark);

        /// <summary>
        /// Hintergrundfarbe des Content-Bereiches
        /// </summary>
        public static PropertyColorBackground BreadcrumbBackground => new PropertyColorBackground();

        /// <summary>
        /// Hintergrundfarbe des Content-Bereiches
        /// </summary>
        public static TypeSizeButton BreadcrumbSize => TypeSizeButton.Small;

        /// <summary>
        /// Hintergrundfarbe des Content-Bereiches
        /// </summary>
        public static PropertyColorBackground ToolbarBackground => new PropertyColorBackground(TypeColorBackground.Secondary);

        /// <summary>
        /// Hintergrundfarbe des Main-Bereiches
        /// </summary>
        public static PropertyColorBackground MainBackground => new PropertyColorBackground();

        /// <summary>
        /// Hintergrundfarbe des Content-Bereiches
        /// </summary>
        public static PropertyColorBackground ContentBackground => new PropertyColorBackground();

        /// <summary>
        /// Hintergrundfarbe einer Schaltfläche
        /// </summary>
        public static PropertyColorButton ButtonBackground => new PropertyColorButton(TypeColorButton.Success);

        /// <summary>
        /// Hintergrundfarbe einer Schaltfläche
        /// </summary>
        public static PropertyColorButton SubmitButtonBackground => new PropertyColorButton(TypeColorButton.Success);

        /// <summary>
        /// Hintergrundfarbe einer Schaltfläche
        /// </summary>
        public static PropertyColorButton NextButtonBackground => new PropertyColorButton(TypeColorButton.Success);

        /// <summary>
        /// Hintergrundfarbe einer Schaltfläche
        /// </summary>
        public static PropertyColorButton CancelButtonBackground => new PropertyColorButton(TypeColorButton.Secondary);

        /// <summary>
        /// Hintergrundfarbe einer Schaltfläche
        /// </summary>
        public static PropertyColorButton CloseButtonBackground => new PropertyColorButton(TypeColorButton.Primary);

        /// <summary>
        /// Hintergrundfarbe eines Validation-Meldung
        /// </summary>
        public static PropertyColorBackground ValidationErrorBackground => new PropertyColorBackgroundAlert(TypeColorBackground.Danger);

        /// <summary>
        /// Hintergrundfarbe eines Validation-Meldung
        /// </summary>
        public static PropertyColorBackground ValidationWarningBackground => new PropertyColorBackgroundAlert(TypeColorBackground.Warning);

        /// <summary>
        /// Hintergrundfarbe eines Validation-Meldung
        /// </summary>
        public static PropertyColorBackground ValidationSuccessBackground => new PropertyColorBackgroundAlert(TypeColorBackground.Success);

        /// <summary>
        /// Hintergrundfarbe der Fußzeile
        /// </summary>
        public static PropertyColorBackground FooterBackground => new PropertyColorBackground(TypeColorBackground.Dark);
    }
}
