using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    public class ControlTimelineItem : Control
    {
        /// <summary>
        /// Liefert oder setzt den Namen
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Liefert oder setzt das Avatarbild
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Liefert oder setzt den Namen des Users
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Liefert oder setzt die Aktion des Eintrages
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// Liefert oder setzt den Zeitstempel
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Liefert oder setzt den Text
        /// </summary>
        public string Post { get; set; }

        /// <summary>
        /// Liefert oder setzt die Kommentare
        /// </summary>
        public List<ControlTimelineComment> Comments { get; private set; }

        /// <summary>
        /// Liefert oder setzt die Anzahl der Gefällt-mir-Angaben
        /// </summary>
        public int Likes { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public ControlTimelineItem(IPage page, string id = null)
            : base(page, id)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Comments = new List<ControlTimelineComment>();
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode ToHtml()
        {
            var classes = new List<string>
            {
                Class,
                "post"
            };

            var profile = new ControlAvatar(Page)
            {
                User = User,
                Image = Image
            };

            var timespan = string.Empty;
            var days = (DateTime.Now - Timestamp).Days;
            if (days == 1)
            {
                timespan = "vor ein Tag";
            }
            else if (days < 1)
            {
                var hours = (DateTime.Now - Timestamp).Hours;
                if (hours == 1)
                {
                    timespan = "vor einer Stunde";
                }
                else if (hours < 1)
                {
                    var minutes = (DateTime.Now - Timestamp).Minutes;

                    if (minutes == 1)
                    {
                        timespan = "vor einer Minute";
                    }
                    else if (minutes < 1)
                    {
                        timespan = "gerade ebend";
                    }
                    else
                    {
                        timespan = "vor " + minutes + " Minuten";
                    }
                }
                else
                {
                    timespan = "vor " + hours + " Stunden";
                }
            }
            else
            {
                timespan = "vor " + days + " Tagen";
            }

            var date = new ControlText(Page)
            {
                Text = timespan,
                Tooltip = "Am " + Timestamp.ToShortDateString() + " um " + Timestamp.ToShortTimeString() + " Uhr",
                Format = TypesTextFormat.Span,
                Color = TypesTextColor.Muted
            };

            var headerText = new HtmlElementP
            (
                new ControlText(Page) { Text = Action, Color = TypesTextColor.Info, Format = TypesTextFormat.Span }.ToHtml(),
                date.ToHtml()
            );

            var setting = new ControlDropdownMenu(Page)
            {
                Icon = Icon.Cog,
                Layout = TypesLayoutButton.Light,
                HorizontalAlignment = TypesHorizontalAlignment.Right,
                Size = TypesSize.Small
            };
            setting.Add(new ControlLink(Page) { Text = "Löschen", Icon = Icon.TrashAlt, Color = TypesTextColor.Danger, Uri = Page.Uri });

            var header = new HtmlElementDiv(setting.ToHtml(), profile.ToHtml(), headerText)
            {
                Class = "header"
            };

            var body = new HtmlElementDiv(new HtmlText(Post))
            {
                Class = string.Join(" ", classes.Where(x => !string.IsNullOrWhiteSpace(x)))
            };

            var likeText = "Gefällt mir" + (Likes > 0 ? " (" + Likes + ")" : string.Empty);
            var like = new ControlButtonLink(Page) { Icon = Icon.ThumbsUp, Text = likeText, Uri = Page.Uri, Size = TypesSize.Small, Layout = TypesLayoutButton.Light, Outline = true, Color = TypesTextColor.Primary };
            //var comment = new ControlButtonLink(Page) { Icon = "fas fa-comment", Text = "Antworten", Url = Page.GetUrl(), Size = TypesSize.Small, Layout = TypesLayoutButton.Light, Color = TypesTextColor.Primary };

            var option = new HtmlElementDiv(like.ToHtml())
            {
                Class = "options"
            };

            var html = new HtmlList(header, body, option);
            //if (Comments.Count > 0)
            //{
            //    html.Elements.Add(new HtmlElementHr());
            //}
            html.Elements.AddRange(from x in Comments select x.ToHtml());

            var form = new ControlPanelFormular(Page)
            {
                Name = !string.IsNullOrWhiteSpace(Name) ? Name : "form",
                EnableCancelButton = false
            };

            form.SubmitButton.Icon = "fas fa-paper-plane";
            form.SubmitButton.Text = "Antworten";
            form.SubmitButton.Outline = true;
            form.SubmitButton.Size = TypesSize.Small;
            form.SubmitButton.HorizontalAlignment = TypesHorizontalAlignment.Default;

            form.Add(new ControlFormularItemTextBox(form) { Format = TypesEditTextFormat.Multiline, Placeholder = "Kommentieren..." });

            html.Elements.Add(form.ToHtml());

            return html;
        }
    }
}
