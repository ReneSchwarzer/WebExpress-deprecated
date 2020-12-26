using WebExpress.WebResource;

namespace WebExpress.UI.WebControl
{
    public class RenderContextFormularGroup : RenderContextFormular
    {
        /// <summary>
        /// Die Gruppe, indem das Steuerelement gerendert wird
        /// </summary>
        public ControlFormularItemGroup Group { get; private set; }

        /// <summary>
        /// Liefert oder setzt das Layout
        /// </summary>
        public override TypeLayoutFormular Layout
        {
            get => Group.Layout;
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die Seite, indem das Steuerelement gerendert wird</param>
        /// <param name="formular">Das Formular, indem das Steuerelement gerendert wird</param>
        /// <param name="group">Die Gruppe</param>
        public RenderContextFormularGroup(IPage page, ControlFormular formular, ControlFormularItemGroup group)
            : base(page, formular)
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement gerendert wird</param>
        /// <param name="formular">Das Formular, indem das Steuerelement gerendert wird</param>
        /// <param name="group">Die Gruppe</param>
        public RenderContextFormularGroup(RenderContext context, ControlFormular formular, ControlFormularItemGroup group)
            : base(context?.Page, formular)
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement gerendert wird</param>
        /// <param name="group">Die Gruppe</param>
        public RenderContextFormularGroup(RenderContextFormular context, ControlFormularItemGroup group)
            : base(context.Page, context.Formular)
        {
            Group = group;
        }
    }
}
