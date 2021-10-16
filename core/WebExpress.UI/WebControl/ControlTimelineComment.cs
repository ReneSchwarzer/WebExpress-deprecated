using System;
using WebExpress.Html;
using WebExpress.Uri;
using WebExpress.WebPage;

namespace WebExpress.UI.WebControl
{
    public class ControlTimelineComment : Control
    {
        /// <summary>
        /// Liefert oder setzt das Avatarbild
        /// </summary>
        public IUri Image { get; set; }

        /// <summary>
        /// Liefert oder setzt den Namen des Users
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Liefert oder setzt den Zeitstempel
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Liefert oder setzt den Text
        /// </summary>
        public string Post { get; set; }

        /// <summary>
        /// Liefert oder setzt die Anzahl der Gefällt-mir-Angaben
        /// </summary>
        public int Likes { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlTimelineComment(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            var profile = new ControlAvatar()
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

            var date = new ControlText()
            {
                Text = timespan,
                Title = "Am " + Timestamp.ToShortDateString() + " um " + Timestamp.ToShortTimeString() + " Uhr",
                Format = TypeFormatText.Span,
                TextColor = new PropertyColorText(TypeColorText.Muted)
            };

            var header = new HtmlElementTextContentDiv(profile.Render(context), date.Render(context))
            {
                Class = "header"
            };

            var body = new HtmlElementTextContentDiv(new HtmlText(Post))
            {
                Class = "post"
            };

            var likeText = "Gefällt mir" + (Likes > 0 ? " (" + Likes + ")" : string.Empty);

            var like = new ControlButtonLink()
            {
                Icon = new PropertyIcon(TypeIcon.ThumbsUp),
                Text = likeText,
                Uri = context.Request.Uri,
                Size = TypeSizeButton.Small,
                BackgroundColor = new PropertyColorButton(TypeColorButton.Light),
                Outline = true,
                TextColor = new PropertyColorText(TypeColorText.Primary)
            };

            var option = new HtmlElementTextContentDiv(Likes > 0 ? like.Render(context) : null)
            {
                Class = "options"
            };

            var html = new HtmlElementTextContentDiv(header, body, option)
            {
                Class = Css.Concatenate("comment", GetClasses()),
                Style = GetStyles(),
                Role = Role
            };

            return html;
        }
    }
}
