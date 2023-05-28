using WebExpress.WebPage;

namespace WebExpress.UI.WebControl
{
    public class RenderContextFormularGroup : RenderContextFormular
    {
        /// <summary>
        /// Die Gruppe, indem das Steuerelement gerendert wird
        /// </summary>
        public ControlFormItemGroup Group { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement gerendert wird</param>
        /// <param name="formular">Das Formular, indem das Steuerelement gerendert wird</param>
        /// <param name="group">Die Gruppe</param>
        public RenderContextFormularGroup(RenderContext context, ControlForm formular, ControlFormItemGroup group)
            : base(context, formular)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement gerendert wird</param>
        /// <param name="group">Die Gruppe</param>
        public RenderContextFormularGroup(RenderContextFormular context, ControlFormItemGroup group)
            : base(context)
        {
            Group = group;
        }
    }
}
