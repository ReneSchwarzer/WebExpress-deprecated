using WebExpress.Messages;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    public interface IControlFormular
    {
        /// <summary>
        /// Liefert oder setzt die zugehörige Seite
        /// </summary>
        IPage Page { get; }

        /// <summary>
        /// Liefert oder setzt das Layout
        /// </summary>
        TypesLayoutForm Layout { get; set; }

        /// <summary>
        /// Liefert oder setzt den Gültigkeitsbereich der Formulardaten
        /// </summary>
        ParameterScope Scope { get; set; }
    }
}
