using System;
using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;

namespace WebExpress.UI.WebControl
{
    public class ControlFormularItemInputGroup : ControlFormularItemInput
    {
        /// <summary>
        /// Liefert oder setzt die Gruppe
        /// </summary>
        public ControlFormularItemGroup Group { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        public ControlFormularItemInputGroup(string id = null)
            : base(id)
        {
            Name = ID;
            Margin = new PropertySpacingMargin(PropertySpacing.Space.None, PropertySpacing.Space.Two, PropertySpacing.Space.None, PropertySpacing.Space.None);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="id">Die ID</param>
        /// <param name="group">Der Name der TextBox</param>
        public ControlFormularItemInputGroup(string id, ControlFormularItemGroup group)
            : base(id)
        {
            Name = id;
            Group = group;
        }

        /// <summary>
        /// Initialisiert das Formularelement
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        public override void Initialize(RenderContextFormular context)
        {
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement dargestellt wird</param>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode Render(RenderContextFormular context)
        {
            return Group?.Render(context);
        }

        /// <summary>
        /// Prüft das Eingabeelement auf Korrektheit der Daten
        /// </summary>
        public override void Validate()
        {
            if (Disabled)
            {
                return;
            }

            Group.Validate();
        }
    }
}
