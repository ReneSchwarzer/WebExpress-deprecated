using WebExpress.UI.WebControl;

namespace WebExpress.WebApp.WebPage
{
    public static class LayoutSchema
    {
        /// <summary>
        /// Hintergrundfarbe des Headers
        /// </summary>
        public static PropertyColorBackground HeaderBackground => new(TypeColorBackground.Dark);

        /// <summary>
        /// Farbe des Headers-Titels
        /// </summary>
        public static PropertyColorText HeaderTitle => new(TypeColorText.Light);

        /// <summary>
        /// Farbe des Headers-Links
        /// </summary>
        public static PropertyColorText HeaderNavigationLink => new(TypeColorText.Light);

        /// <summary>
        /// Farbe des Headers-Titels
        /// </summary>
        public static PropertyColorBackground HeaderNavigationActiveBackground => new();

        /// <summary>
        /// Farbe des Headers-Titels
        /// </summary>
        public static PropertyColorText HeaderNavigationActive => new(TypeColorText.Info);

        /// <summary>
        /// Hintergrundfarbe einer Schaltfläche
        /// </summary>
        public static PropertyColorButton HeaderQuickCreateButtonBackground => new(TypeColorButton.Success);

        /// <summary>
        /// Hintergrundfarbe einer Schaltfläche
        /// </summary>
        public static TypeSizeButton HeaderQuickCreateButtonSize => TypeSizeButton.Small;

        /// <summary>
        /// Farbe des Headers-Titels
        /// </summary>
        public static PropertyColorText NavigationLink => new(TypeColorText.Primary);

        /// <summary>
        /// Farbe des Headers-Titels
        /// </summary>
        public static PropertyColorBackground NavigationActiveBackground => new(TypeColorBackground.Primary);

        /// <summary>
        /// Farbe des Headers-Titels
        /// </summary>
        public static PropertyColorText NavigationActive => new(TypeColorText.Light);

        /// <summary>
        /// Farbe des Headers-Titels
        /// </summary>
        public static PropertyColorText Link => new(TypeColorText.Dark);

        /// <summary>
        /// Hintergrundfarbe des Sidbar-Bereiches
        /// </summary>
        public static PropertyColorBackground SidebarBackground => new(TypeColorBackground.Light);

        /// <summary>
        /// Farbe des Sidbar-Titels
        /// </summary>
        public static PropertyColorBackground SidebarNavigationActiveBackground => new(TypeColorBackground.Info);

        /// <summary>
        /// Farbe des Sidbar-Titels
        /// </summary>
        public static PropertyColorText SidebarNavigationActive => new(TypeColorText.Light);

        /// <summary>
        /// Farbe des Sidbar-Titels
        /// </summary>
        public static PropertyColorText SidebarNavigationLink => new(TypeColorText.Dark);

        /// <summary>
        /// Farbe des Splitters
        /// </summary>
        public static PropertyColorBackground SplitterColor => new(TypeColorBackground.Light);

        /// <summary>
        /// Hintergrundfarbe des Headline-Bereiches
        /// </summary>
        public static PropertyColorBackground HeadlineBackground => new();

        /// <summary>
        /// Hintergrundfarbe des Headline-Bereiches
        /// </summary>
        public static PropertyColorText HeadlineTitle => new(TypeColorText.Dark);

        /// <summary>
        /// Hintergrundfarbe des Property-Bereiches
        /// </summary>
        public static PropertyColorBackground PropertyBackground => new("#C6E2FF");

        /// <summary>
        /// Farbe des Property-Titels
        /// </summary>
        public static PropertyColorBackground PropertyNavigationActiveBackground => new(TypeColorBackground.Info);

        /// <summary>
        /// Farbe des Property-Titels
        /// </summary>
        public static PropertyColorText PropertyNavigationActive => new(TypeColorText.Light);

        /// <summary>
        /// Farbe des Property-Titels
        /// </summary>
        public static PropertyColorText PropertyNavigationLink => new(TypeColorText.Dark);

        /// <summary>
        /// Hintergrundfarbe des Content-Bereiches
        /// </summary>
        public static PropertyColorBackground BreadcrumbBackground => new();

        /// <summary>
        /// Hintergrundfarbe des Content-Bereiches
        /// </summary>
        public static TypeSizeButton BreadcrumbSize => TypeSizeButton.Small;

        /// <summary>
        /// Hintergrundfarbe der Werkzeugleiste
        /// </summary>
        public static PropertyColorBackground ToolbarBackground => new();

        /// <summary>
        /// Linkfarbe der Werkzeugleiste
        /// </summary>
        public static PropertyColorText ToolbarLink => new(TypeColorText.Dark);

        /// <summary>
        /// Hintergrundfarbe des Content-Bereiches
        /// </summary>
        public static PropertyColorBackground ContentBackground => new();

        /// <summary>
        /// Hintergrundfarbe des Content-Bereiches
        /// </summary>
        public static PropertyColorBackground FormularBackground => new(TypeColorBackground.Light);

        /// <summary>
        /// Hintergrundfarbe einer Schaltfläche
        /// </summary>
        public static PropertyColorButton ButtonBackground => new(TypeColorButton.Success);

        /// <summary>
        /// Hintergrundfarbe einer Schaltfläche
        /// </summary>
        public static PropertyColorButton SubmitButtonBackground => new(TypeColorButton.Success);

        /// <summary>
        /// Hintergrundfarbe einer Schaltfläche
        /// </summary>
        public static PropertyColorButton NextButtonBackground => new(TypeColorButton.Success);

        /// <summary>
        /// Hintergrundfarbe einer Schaltfläche
        /// </summary>
        public static PropertyColorButton CancelButtonBackground => new(TypeColorButton.Secondary);

        /// <summary>
        /// Hintergrundfarbe einer Schaltfläche
        /// </summary>
        public static PropertyColorButton CloseButtonBackground => new(TypeColorButton.Primary);

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
        public static PropertyColorBackground FooterBackground => new(TypeColorBackground.Light);

        /// <summary>
        /// Textfarbe der Fußzeile
        /// </summary>
        public static PropertyColorText FooterText => new(TypeColorText.Secondary);
    }
}
