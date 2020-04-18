using WebExpress.Html;
using WebExpress.Messages;
using WebExpress.Pages;

namespace WebExpress.UI.Controls
{
    public abstract class Control : IControl
    {
        /// <summary>
        /// Die horizontale Anordnung
        /// </summary>
        public TypesHorizontalAlignment HorizontalAlignment { get; set; }

        /// <summary>
        /// Liefert oder setzt die zugehörige Seite
        /// </summary>
        public IPage Page { get; private set; }

        /// <summary>
        /// Liefert oder setzt die ID
        /// </summary>
        public string ID { get; private set; }

        /// <summary>
        /// Liefert oder setzt die Css-Klasse
        /// </summary>
        public string Class { get; set; }

        /// <summary>
        /// Liefert oder setzt die Css-Style
        /// </summary>
        public string Style { get; set; }

        /// <summary>
        /// Liefert oder setzt die Rolle
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Liefert oder setzt die OnClick-Attribut
        /// </summary>
        public string OnClick { get; set; }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <returns>Das Control als HTML</returns>
        public abstract IHtmlNode ToHtml();

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="page">Die zugehörige Seite</param>
        /// <param name="id">Die ID</param>
        public Control(IPage page, string id)
        {
            Page = page;
            ID = id;

            Init();
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {

        }

        /// <summary>
        /// Fügt ein Parameter hinzu. Der Wert wird aus dem Request ermittelt
        /// </summary>
        /// <param name="name">Der Name des Parametern</param>
        /// <param name="scope">Der Gültigkeitsbereich des Parameters</param>
        public void AddParam(string name, ParameterScope scope = ParameterScope.Global)
        {
            Page.AddParam(name.ToLower(), scope);
        }

        /// <summary>
        /// Fügt ein Parameter hinzu.
        /// </summary>
        /// <param name="name">Der Name des Parametern</param>
        /// <param name="value">Der Wert</param>
        /// <param name="scope">Der Gültigkeitsbereich des Parameters</param>
        public void AddParam(string name, string value, ParameterScope scope = ParameterScope.Global)
        {
            Page.AddParam(name.ToLower(), value, scope);
        }

        /// <summary>
        /// Fügt ein Parameter hinzu. 
        /// </summary>
        /// <param name="param">Der Parameter</param>
        /// <param name="scope">Der Gültigkeitsbereich des Parameters</param>
        public void AddParam(Parameter param)
        {
            Page.AddParam(param);
        }

        /// <summary>
        /// Liefert ein Parameter anhand seines Namens
        /// </summary>
        /// <param name="name">Der Name des Parameters</param>
        /// <returns>Der Wert</returns>
        public string GetParam(string name)
        {
            return Page.GetParam(name?.ToLower());
        }

        /// <summary>
        /// Liefert ein Parameter anhand seines Namens
        /// </summary>
        /// <param name="name">Der Name des Parameters</param>
        /// <param name="defaultValue">Standardwert</param>
        /// <returns>Der Wert</returns>
        public int GetParam(string name, int defaultValue)
        {
            return Page.GetParam(name?.ToLower(), defaultValue);
        }
    }
}
