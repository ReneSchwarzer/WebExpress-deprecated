using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;

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
        /// <param name="id">Die ID</param>
        public ControlTimelineItem(string id = null)
            : base(id)
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
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Classes.Add("post");

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

            var headerText = new HtmlElementTextContentP
            (
                new ControlText()
                {
                    Text = Action,
                    TextColor = new PropertyColorText(TypeColorText.Info),
                    Format = TypeFormatText.Span
                }.Render(context),
                date.Render(context)
            );

            var setting = new ControlDropdown()
            {
                //Icon = new PropertyIcon(TypeIcon.Cog),
                BackgroundColor = new PropertyColorButton(TypeColorButton.Light),
                HorizontalAlignment = TypeHorizontalAlignment.Right,
                Size = TypeSizeButton.Small
            };
            setting.Add(new ControlDropdownItemLink()
            {
                Text = "Löschen",
                Icon = new PropertyIcon(TypeIcon.TrashAlt),
                TextColor = new PropertyColorText(TypeColorText.Danger),
                Uri = context.Page.Uri
            });

            var header = new HtmlElementTextContentDiv(setting.Render(context), profile.Render(context), headerText)
            {
                Class = "header"
            };

            var body = new HtmlElementTextContentDiv(new HtmlText(Post))
            {
                Class = string.Join(" ", Classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                Role = Role
            };

            var likeText = "Gefällt mir" + (Likes > 0 ? " (" + Likes + ")" : string.Empty);
            var like = new ControlButtonLink()
            {
                Icon = new PropertyIcon(TypeIcon.ThumbsUp),
                Text = likeText,
                Uri = context.Page.Uri,
                Size = TypeSizeButton.Small,
                BackgroundColor = new PropertyColorButton(TypeColorButton.Light),
                Outline = true,
                TextColor = new PropertyColorText(TypeColorText.Primary)
            };

            var option = new HtmlElementTextContentDiv(like.Render(context))
            {
                Class = "options"
            };

            var html = new HtmlList(header, body, option);

            html.Elements.AddRange(from x in Comments select x.Render(context));

            var form = new ControlFormular()
            {
                Name = !string.IsNullOrWhiteSpace(Name) ? Name : "form",
                EnableCancelButton = false
            };

            form.SubmitButton.Icon = new PropertyIcon(TypeIcon.PaperPlane);
            form.SubmitButton.Text = "Antworten";
            form.SubmitButton.Outline = true;
            form.SubmitButton.Size = TypeSizeButton.Small;
            //form.SubmitButton.HorizontalAlignment = TypeHorizontalAlignment.Default;

            form.Add(new ControlFormularItemInputTextBox() { Format = TypesEditTextFormat.Multiline, Placeholder = "Kommentieren..." });

            html.Elements.Add(form.Render(context));

            return html;
        }
    }
}
