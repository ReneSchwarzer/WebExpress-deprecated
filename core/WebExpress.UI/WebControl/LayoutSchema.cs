namespace WebExpress.UI.WebControl
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
        /// Hintergrundfarbe einer Schaltfläche
        /// </summary>
        public static PropertyColorButton HeaderQuickCreateButtonBackground => new PropertyColorButton(TypeColorButton.Success);

        /// <summary>
        /// Hintergrundfarbe einer Schaltfläche
        /// </summary>
        public static TypeSizeButton HeaderQuickCreateButtonSize => TypeSizeButton.Small;

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
        /// Hintergrundfarbe des Sidbar-Bereiches
        /// </summary>
        public static PropertyColorBackground SidebarBackground => new PropertyColorBackground(TypeColorBackground.Light);

        /// <summary>
        /// Farbe des Sidbar-Titels
        /// </summary>
        public static PropertyColorBackground SidebarNavigationActiveBackground => new PropertyColorBackground(TypeColorBackground.Info);

        /// <summary>
        /// Farbe des Sidbar-Titels
        /// </summary>
        public static PropertyColorText SidebarNavigationActive => new PropertyColorText(TypeColorText.Light);

        /// <summary>
        /// Farbe des Sidbar-Titels
        /// </summary>
        public static PropertyColorText SidebarNavigationLink => new PropertyColorText(TypeColorText.Dark);

        /// <summary>
        /// Hintergrundfarbe des Headline-Bereiches
        /// </summary>
        public static PropertyColorBackground HeadlineBackground => new PropertyColorBackground();

        /// <summary>
        /// Hintergrundfarbe des Headline-Bereiches
        /// </summary>
        public static PropertyColorText HeadlineTitle => new PropertyColorText(TypeColorText.Dark);

        /// <summary>
        /// Hintergrundfarbe des Property-Bereiches
        /// </summary>
        public static PropertyColorBackground PropertyBackground => new PropertyColorBackground(TypeColorBackground.Light);

        /// <summary>
        /// Farbe des Property-Titels
        /// </summary>
        public static PropertyColorBackground PropertyNavigationActiveBackground => new PropertyColorBackground(TypeColorBackground.Info);

        /// <summary>
        /// Farbe des Property-Titels
        /// </summary>
        public static PropertyColorText PropertyNavigationActive => new PropertyColorText(TypeColorText.Light);

        /// <summary>
        /// Farbe des Property-Titels
        /// </summary>
        public static PropertyColorText PropertyNavigationLink => new PropertyColorText(TypeColorText.Dark);

        /// <summary>
        /// Hintergrundfarbe des Content-Bereiches
        /// </summary>
        public static PropertyColorBackground BreadcrumbBackground => new PropertyColorBackground();

        /// <summary>
        /// Hintergrundfarbe des Content-Bereiches
        /// </summary>
        public static TypeSizeButton BreadcrumbSize => TypeSizeButton.Small;

        /// <summary>
        /// Hintergrundfarbe der Werkzeugleiste
        /// </summary>
        public static PropertyColorBackground ToolbarBackground => new PropertyColorBackground();

        /// <summary>
        /// Linkfarbe der Werkzeugleiste
        /// </summary>
        public static PropertyColorText ToolbarLink => new PropertyColorText(TypeColorText.Dark);

        /// <summary>
        /// Hintergrundfarbe des Content-Bereiches
        /// </summary>
        public static PropertyColorBackground ContentBackground => new PropertyColorBackground();

        /// <summary>
        /// Hintergrundfarbe des Content-Bereiches
        /// </summary>
        public static PropertyColorBackground FormularBackground => new PropertyColorBackground(TypeColorBackground.Light);

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
        public static PropertyColorBackground FooterBackground => new PropertyColorBackground(TypeColorBackground.Light);

        /// <summary>
        /// Textfarbe der Fußzeile
        /// </summary>
        public static PropertyColorText FooterText => new PropertyColorText(TypeColorText.Secondary);
    }
}
