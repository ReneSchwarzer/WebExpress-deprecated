using System.Collections.Generic;
using WebExpress.WebMessage;
using WebExpress.WebPage;

namespace WebExpress.UI.WebControl
{
    public class RenderContextFormular : RenderContext
    {
        /// <summary>
        /// Das Formular, indem das Steuerelement gerendert wird
        /// </summary>
        public IControlForm Formular { get; private set; }

        /// <summary>
        /// Liefert oder setzt die Links auf die zu verwendenden JavaScript-Dateien
        /// </summary>
        public IDictionary<string, string> Scripts { get; } = new Dictionary<string, string>();

        /// <summary>
        /// Liefert die Validierungsfehler
        /// </summary>
        public ICollection<ValidationResult> ValidationResults { get; } = new List<ValidationResult>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="page">Die Seite, indem das Steuerelement gerendert wird</param>
        /// <param name="request">The request.</param>
        /// <param name="visualTree">Der visuelle Baum</param>
        /// <param name="formular">Das Formular, indem das Steuerelement gerendert wird</param>
        public RenderContextFormular(IPage page, Request request, IVisualTree visualTree, IControlForm formular)
                : base(page, request, visualTree)
        {
            Formular = formular;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Der Kontext, indem das Steuerelement gerendert wird</param>
        /// <param name="formular">Das Formular, indem das Steuerelement gerendert wird</param>
        public RenderContextFormular(RenderContext context, IControlForm formular)
            : base(context)
        {
            Formular = formular;
        }

        /// <summary>
        /// Copy-Constructor
        /// </summary>
        /// <param name="context">Der zu kopierende Kontext/param>
        public RenderContextFormular(RenderContextFormular context)
            : this(context, context?.Formular)
        {
            Scripts = context.Scripts;
        }

        /// <summary>
        /// Fügt eine Java-Script hinzu oder sersetzt dieses, falls vorhanden
        /// </summary>
        /// <param name="key">Der Schlüssel</param>
        /// <param name="code">Der Code</param>
        public void AddScript(string key, string code)
        {
            if (!Scripts.ContainsKey(key))
            {
                Scripts.Add(key, code);
            }
        }

    }
}
