﻿using WebExpress.WebPage;

namespace WebExpress.UI.WebControl
{
    public class RenderContextFormularGroup : RenderContextFormular
    {
        /// <summary>
        /// Die Gruppe, indem das Steuerelement gerendert wird
        /// </summary>
        public ControlFormularItemGroup Group { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement gerendert wird</param>
        /// <param name="formular">Das Formular, indem das Steuerelement gerendert wird</param>
        /// <param name="group">Die Gruppe</param>
        public RenderContextFormularGroup(RenderContext context, ControlFormular formular, ControlFormularItemGroup group)
            : base(context, formular)
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement gerendert wird</param>
        /// <param name="group">Die Gruppe</param>
        public RenderContextFormularGroup(RenderContextFormular context, ControlFormularItemGroup group)
            : base(context)
        {
            Group = group;
        }
    }
}
