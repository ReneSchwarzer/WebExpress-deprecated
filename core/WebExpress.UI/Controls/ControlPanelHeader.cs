using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    public class ControlPanelHeader : ControlPanel
    {
        /// <summary>
        /// Die fixierte Anordnung
        /// </summary>
        public virtual TypeFixed Fixed
        {
            get => (TypeFixed)GetProperty(TypeFixed.None);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Fixiert die Anordnung, wenn sich die Toolbar am oberen Rand befindet
        /// </summary>
        public virtual TypeSticky Sticky
        {
            get => (TypeSticky)GetProperty(TypeSticky.None);
            set => SetProperty(value, () => value.ToClass());
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlPanelHeader(string id = null)
            : base(id)
        {
            Init();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="content">Der Inhalt</param>
        public ControlPanelHeader(params Control[] content)
            : this()
        {
            Content.AddRange(content);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="content">Der Inhalt</param>
        public ControlPanelHeader(string id, params Control[] content)
            : this(id)
        {
            Content.AddRange(content);
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
            return new HtmlElementSectionHeader(from x in Content select x.Render(context))
            {
                ID = ID,
                Class = GetClasses(),
                Style = GetStyles(),
                Role = Role
            };
        }
    }
}
