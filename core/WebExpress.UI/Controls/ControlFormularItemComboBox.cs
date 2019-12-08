using System.Collections.Generic;
using System.Linq;
using WebServer.Html;

namespace WebExpress.UI.Controls
{
    public class ControlFormularItemComboBox : ControlFormularItemInput
    {
        /// <summary>
        /// Liefert oder setzt die Einträge
        /// </summary>
        public List<ControlFormularItemComboBoxItem> Items { get; private set; }

        /// <summary>
        /// Liefert oder setzt das ausgewählte Element
        /// </summary>
        public string Selected { get; set; }

        /// <summary>
        /// Liefert oder setzt die OnChange-Attribut
        /// </summary>
        public string OnChange { get; set; }

        /// <summary>
        /// Liefert oder setzt das ausgewählte Element anhand des Wertes
        /// </summary>
        public string SelectedValue
        {
            get
            {
                var param = GetParam(Name);

                if (!string.IsNullOrWhiteSpace(param))
                {
                    return param;
                }

                // erstes Element auswählen
                var item = Items.FirstOrDefault();
                if (item != null)
                {
                    var subitem = item.SubItems.FirstOrDefault();
                    if (subitem != null)
                    {
                        return subitem.Value;
                    }

                    return item.Value;
                }

                return string.Empty;
            }
            set
            {
                var v = GetParam(Name);

                if (string.IsNullOrWhiteSpace(v))
                {
                    AddParam(Name, value, Formular.Scope);
                }
            }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="formular">Das zugehörige Formular</param>
        /// <param name="id">Die ID</param>
        public ControlFormularItemComboBox(IControlFormular formular, string id = null)
            : base(formular, id)
        {
            Init();

            Name = id;

            //AddParam(Name, Formular.Scope);
            //SelectedValue = Page.GetParam(Name);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="formular">Das zugehörige Formular</param>
        /// <param name="id">Die ID</param>
        /// <param name="items">Die ComboBox-Einträge</param>
        public ControlFormularItemComboBox(IControlFormular formular, string id, params string[] items)
            : this(formular, id)
        {
            Items.AddRange(from v in items select new ControlFormularItemComboBoxItem() { Text = v });
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="formular">Das zugehörige Formular</param>
        /// <param name="items">Die ComboBox-Einträge</param>
        public ControlFormularItemComboBox(IControlFormular formular, string id, params ControlFormularItemComboBoxItem[] items)
            : this(formular, id)
        {
            Items.AddRange(items);
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="formular">Das zugehörige Formular</param>
        /// <param name="id">Die ID</param>
        /// <param name="items">Die ComboBox-Einträge</param>
        public ControlFormularItemComboBox(IControlFormular formular, string id, string name, IEnumerable<ControlFormularItemComboBoxItem> items)
            : this(formular, id, name)
        {
            Items.AddRange(items);
        }

        /// <summary>
        /// Initialisierung
        /// </summary>
        private void Init()
        {
            Items = new List<ControlFormularItemComboBoxItem>();
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <returns>Das Control als HTML</returns>
        public override IHtmlNode ToHtml()
        {
            var classes = new List<string>
            {
                Class,
                "custom-select"
            };

            var select = string.IsNullOrWhiteSpace(SelectedValue) ? Selected : SelectedValue;

            var html = new HtmlElementSelect()
            {
                ID = ID,
                Name = Name,
                Class = string.Join(" ", classes.Where(x => !string.IsNullOrWhiteSpace(x))),
                Disabled = Disabled,
                OnChange = OnChange
            };

            foreach (var v in Items)
            {
                if (v.SubItems.Count > 0)
                {
                    html.Elements.Add(new HtmlElementOptgroup() { Label = v.Text });
                    foreach (var s in v.SubItems)
                    {
                        html.Elements.Add(new HtmlElementOption() { Value = s.Value, Text = s.Text, Selected = (s.Value == select) });
                    }
                }
                else
                {
                    html.Elements.Add(new HtmlElementOption() { Value = v.Value, Text = v.Text, Selected = (v.Value == select) });
                }
            }

            return html;
        }

        /// <summary>
        /// Prüft das Eingabeelement auf Korrektheit der Daten
        /// </summary>
        public override void Validate()
        {
            base.Validate();
        }
    }
}
