﻿using System;
using WebExpress.Html;
using WebExpress.Uri;
using WebExpress.WebPage;
using static WebExpress.Internationalization.InternationalizationManager;

namespace WebExpress.UI.WebControl
{
    public class ControlBreadcrumb : Control
    {
        /// <summary>
        /// Liefert oder setzt die Uri
        /// </summary>
        public IUri Uri { get; set; }

        /// <summary>
        /// Liefert oder setzt das Rootelement
        /// </summary>
        public string EmptyName { get; set; }

        /// <summary>
        /// Liefert oder setzt die Größe
        /// </summary>
        public TypeSizeButton Size
        {
            get => (TypeSizeButton)GetProperty(TypeSizeButton.Default);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Liefet oder setzt ein Präfix, welcher statisch vor den Links angezeigt wird.
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// Bestimmt wie viele Links angezeigt werden sollen. Es wird am Anfang der Linkkette abgeschniiten.
        /// </summary>
        public ushort TakeLast { get; set; } = ushort.MaxValue;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlBreadcrumb(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="uri">Der Verzeichnispfad</param>
        public ControlBreadcrumb(string id, IUri uri)
            : base(id)
        {
            Uri = uri;
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Size = TypeSizeButton.Small;
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var html = new HtmlElementTextContentUl()
            {
                Class = Css.Concatenate("breadcrumb rounded-0", GetClasses()),
                Style = GetStyles(),
            };

            if (!string.IsNullOrWhiteSpace(Prefix))
            {
                html.Elements.Add
                (
                    new HtmlElementTextSemanticsSpan(new HtmlText(I18N(context.Culture, Prefix))) { Class = "mr-2" }
                );
            }

            if (Uri is UriResource resourceUri)
            {
                //foreach (var part in resourceUri.Path.TakeLast((int)TakeLast))
                //{
                //    if (part.Display != null)
                //    {
                //        var display = I18N(context.Culture, part.Display);
                //        var href = part.ToString();

                //        html.Elements.Add
                //        (
                //            new HtmlElementTextContentLi
                //            (
                //                //new ControlIcon(Page)
                //                //{ 
                //                //    Icon = path.Icon
                //                //}.ToHtml(),
                //                new HtmlElementTextSemanticsA(display)
                //                {
                //                    Href = href
                //                }
                //            )
                //            {
                //                Class = "breadcrumb-item"
                //            }
                //        );
                //    }
                //}

                var takeLast = Math.Min(TakeLast, resourceUri.Path.Count);
                var from = resourceUri.Path.Count - takeLast;

                for (int i = from + 1; i < resourceUri.Path.Count + 1; i++)
                {
                    var path = resourceUri.Take(i);

                    if (path.Display != null)
                    {
                        var display = I18N(context.Culture, path.Display);
                        var href = path.ToString();

                        html.Elements.Add
                        (
                            new HtmlElementTextContentLi
                            (
                                //new ControlIcon(Page)
                                //{ 
                                //    Icon = path.Icon
                                //}.ToHtml(),
                                new HtmlElementTextSemanticsA(display)
                                {
                                    Href = href
                                }
                            )
                            {
                                Class = "breadcrumb-item"
                            }
                        );
                    }
                }
            }

            return html;
        }
    }
}
