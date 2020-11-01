using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Messages;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    public class ControlPagination : Control
    {
        /// <summary>
        /// Liefert oder setzt die Anzahl der Seiten
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Liefert oder setzt die Seitengröße
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Liefert oder setzt die aktuelle Seite
        /// </summary>
        public int Offset { get; set; }

        /// <summary>
        /// Liefert oder setzt die maximale Anzahl der Seitenschaltflächen
        /// </summary>
        public int MaxDisplayCount { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlPagination(string id = null)
            : base(id)
        {
            MaxDisplayCount = 5;

            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            //AddParam("count");
            //AddParam("size", ParameterScope.Session);
            //AddParam("offset", ParameterScope.Local);

            ////Count = GetParam("count", 0);
            //Size = GetParam("size", 50);
            //Offset = GetParam("offset", 0);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Classes.Add("pagination");
            Classes.Add(HorizontalAlignment.ToClass());

            var html = new HtmlElementTextContentUl()
            {
                Class = string.Join(" ", Classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                Role = Role
            };

            if (Offset >= Count)
            {
                Offset = Count - 1;
            }

            if (Offset < 0)
            {
                Offset = 0;
            }

            if (Offset > 0 && Count > 1)
            {
                html.Elements.Add
                (
                    new HtmlElementTextContentLi
                    (
                        new ControlLink()
                        {
                            Params = Parameter.Create(new Parameter("offset", Offset - 1) { Scope = ParameterScope.Local }),
                            Classes = new List<string>(new[] { "page-link", "fas fa-angle-left" })
                        }.Render(context)
                    )
                    {
                        Class = "page-item"
                    }
                );
            }
            else
            {
                html.Elements.Add
                (
                    new HtmlElementTextContentLi
                    (
                        new ControlLink()
                        {
                            Params = Parameter.Create(),
                            Classes = new List<string>(new[] { "page-link", "fas fa-angle-left" })
                        }.Render(context)
                    )
                    {
                        Class = "page-item disabled"
                    }
                );
            }

            var buf = new List<int>(MaxDisplayCount);

            var j = 0;
            var k = 0;

            buf.Add(Offset);
            while (buf.Count < Math.Min(Count, MaxDisplayCount))
            {
                if (Offset + j + 1 < Count)
                {
                    j += 1;
                    buf.Add(Offset + j);
                }

                if (Offset - k - 1 >= 0)
                {
                    k += 1;
                    buf.Add(Offset - k);
                }
            }

            buf.Sort();

            foreach (var v in buf)
            {
                if (v == Offset)
                {
                    html.Elements.Add
                    (
                        new HtmlElementTextContentLi
                        (
                            new ControlLink(null, (v + 1).ToString())
                            {
                                Params = Parameter.Create(new Parameter("offset", v) { Scope = ParameterScope.Local }),
                                Classes = new List<string>(new[] { "page-link" })
                            }.Render(context)
                        )
                        {
                            Class = "page-item active"
                        }
                    );
                }
                else
                {
                    html.Elements.Add
                    (
                        new HtmlElementTextContentLi
                        (
                            new ControlLink(null, (v + 1).ToString())
                            {
                                Params = Parameter.Create(new Parameter("offset", v) { Scope = ParameterScope.Local }),
                                Classes = new List<string>(new[] { "page-link" })
                            }.Render(context)
                        )
                        {
                            Class = "page-item"
                        }
                    );
                }
            }

            if (Offset < Count - 1)
            {
                html.Elements.Add
                (
                    new HtmlElementTextContentLi
                    (
                        new ControlLink()
                        {
                            Params = Parameter.Create(new Parameter("offset", Offset + 1) { Scope = ParameterScope.Local }),
                            Classes = new List<string>(new[] { "page-link", "fas fa-angle-right" })
                        }.Render(context)
                    )
                    {
                        Class = "page-item"
                    }
                );
            }
            else
            {
                html.Elements.Add
                (
                    new HtmlElementTextContentLi
                    (
                        new ControlLink()
                        {
                            Params = Parameter.Create(),
                            Classes = new List<string>(new[] { "page-link", "fas fa-angle-right" })
                        }.Render(context)
                    )
                    {
                        Class = "page-item disabled"
                    }
                );
            }

            return html;
        }
    }
}
