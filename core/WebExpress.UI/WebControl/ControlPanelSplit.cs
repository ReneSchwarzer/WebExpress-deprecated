using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Module;
using WebExpress.Uri;

namespace WebExpress.UI.WebControl
{
    /// <summary>
    /// Box mit Rahmen
    /// </summary>
    public class ControlPanelSplit : Control
    {
        /// <summary>
        /// Bestimmt ob der Splitter horziontal oder vertikal ausgerichtet ist. 
        /// </summary>
        public TypeOrientationSplit Orientation { get; set; }

        /// <summary>
        /// Liefert oder setzt die Farbe des Splitters
        /// </summary>
        public PropertyColorBackground SplitterColor { get; set; } = new PropertyColorBackground(TypeColorBackground.Light);

        /// <summary>
        /// Liefert oder setzt die Breite des Splitters
        /// </summary>
        public int SplitterSize { get; set; } = 6;

        /// <summary>
        /// Der linke oder obere Bereich im ControlPanelSplit
        /// </summary>
        public List<IControl> Panel1 { get; } = new List<IControl>();

        /// <summary>
        /// Die minimale Größe der linken oder des oberen Bereiches im ControlPanelSplit
        /// </summary>
        public int Panel1MinSize { get; set; }
        
        /// <summary>
        /// Die initiale Größe der linken oder des oberen Bereiches im ControlPanelSplit in %
        /// </summary>
        public int Panel1InitialSize { get; set; } = -1;

        /// <summary>
        /// Der rechte oder untere Bereich im ControlPanelSplit
        /// </summary>
        public List<IControl> Panel2 { get; } = new List<IControl>();

        /// <summary>
        /// Die minimale Größe des rechten oder des unteren Bereiches im ControlPanelSplit
        /// </summary>
        public int Panel2MinSize { get; set; }

        /// <summary>
        /// Die initiale Größe des rechten oder des unteren Bereiches im ControlPanelSplit in %
        /// </summary>
        public int Panel2InitialSize { get; set; } = -1;

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlPanelSplit(string id = null)
            : base(id)
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="panel1">Steuerelemente des linken oder oberen Bereiches</param>
        /// <param name="panel2">Steuerelemente des rechten oder unteren Bereiches</param>
        public ControlPanelSplit(string id, Control[] panel1, Control[] panel2)
            : base(id)
        {
            Panel1.AddRange(panel1);
            Panel2.AddRange(panel2);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="panel1">Steuerelemente des linken oder oberen Bereiches</param>
        /// <param name="panel2">Steuerelemente des rechten oder unteren Bereiches</param>
        public ControlPanelSplit(Control[] panel1, Control[] panel2)
            : this(null, panel1, panel2)
        {
        }

        /// <summary>
        /// Initialisiert das Formularelement
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        public void Initialize(RenderContext context)
        {
            var module = ModuleManager.GetModule("webexpress");
            if (module != null)
            {
                 context.Page.HeaderScriptLinks.Add(new UriResource(module.ContextPath, new UriRelative("/assets/js/split.min.js")));
            }

            Border = new PropertyBorder(true);

            var init1 = 0;
            var init2 = 0;

            if (Panel1InitialSize < 0 && Panel2InitialSize < 0)
            {
                init1 = init2 = 50;
            }
            else if (Panel1InitialSize < 0)
            {
                init1 = 100 - Panel2InitialSize;
                init2 = Panel2InitialSize;
            }
            else if (Panel2InitialSize < 0)
            {
                init1 = Panel1InitialSize;
                init2 = 100 - Panel1InitialSize;
            }

            context.Page.AddScript
            (
                ID, @"Split(['#" + ID + "-p1', '#" + ID + @"-p2'], {
                    sizes: [" + init1 + "," + init2 + @"],
                    minSize: [" + Panel1MinSize + ","+ Panel2MinSize + @"],
                    direction: '" + Orientation.ToString().ToLower() + @"',
                    gutter: function (index, direction) 
                    {
                        var gutter = document.createElement('div');
                        gutter.className = 'splitter splitter-' + direction + ' " + SplitterColor.ToClass() + @"';
                        gutter.style = '" + SplitterColor.ToStyle() + @"';
                        return gutter;
                    },
                    gutterSize: " + SplitterSize + @",
                });"
            );
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            Initialize(context);

            var html = new HtmlElementTextContentDiv()
            {
                ID = ID,
                Class = Css.Concatenate(Orientation == TypeOrientationSplit.Horizontal ? "d-flex split" : "split", GetClasses()),
                Style = GetStyles(),
                Role = Role
            };

            html.Elements.Add(new HtmlElementTextContentDiv(Panel1.Select(x => x.Render(context))) { ID = $"{ID}-p1" });
            html.Elements.Add(new HtmlElementTextContentDiv(Panel2.Select(x => x.Render(context))) { ID = $"{ID}-p2" });

            return html;
        }
    }
}
