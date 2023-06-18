using System.Collections.Generic;
using System.Linq;
using WebExpress.WebHtml;
using WebExpress.WebPage;

namespace WebExpress.UI.WebControl
{
    public class ControlPanel : Control
    {
        /// <summary>
        /// Returns or sets the content.
        /// </summary>
        public List<IControl> Content { get; private set; } = new List<IControl>();

        /// <summary>
        /// Liefert oder setzt die Anordnung des Inhaltes
        /// </summary>
        public TypeDirection Direction
        {
            get => (TypeDirection)GetProperty(TypeDirection.Default);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Fester oder Anpassung an die gesammte Breite
        /// </summary>
        public TypePanelContainer Fluid
        {
            get => (TypePanelContainer)GetProperty(TypePanelContainer.None);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        public ControlPanel(string id = null)
            : base(id)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="content">The content of the html element.</param>
        public ControlPanel(params IControl[] content)
            : this()
        {
            Content.AddRange(content.Where(x => x != null));
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="content">The content of the html element.</param>
        public ControlPanel(ICollection<IControl> content)
            : this()
        {
            Content.AddRange(content.Where(x => x != null));
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="content">The content of the html element.</param>
        public ControlPanel(string id, params IControl[] content)
            : this(id)
        {
            Content.AddRange(content.Where(x => x != null));
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="content">The content of the html element.</param>
        public ControlPanel(string id, IEnumerable<IControl> content)
            : this(id)
        {
            Content.AddRange(content.Where(x => x != null));
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="content">The content of the html element.</param>
        public ControlPanel(string id, List<IControl> content)
            : base(id)
        {
            Content = content;
        }

        /// <summary>
        /// Convert to html.
        /// </summary>
        /// <param name="context">The context in which the control is rendered.</param>
        /// <returns>The control as html.</returns>
        public override IHtmlNode Render(RenderContext context)
        {
            return new HtmlElementTextContentDiv(from x in Content select x.Render(context))
            {
                Id = Id,
                Class = GetClasses(),
                Style = string.Join("; ", Styles.Where(x => !string.IsNullOrWhiteSpace(x))),
                Role = Role
            };
        }
    }
}
