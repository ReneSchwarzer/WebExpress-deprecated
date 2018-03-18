using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFA.WebServer.Html
{
    public class HtmlElementHead : HtmlElement
    {
        /// <summary>
        /// Liefert oder setzt den Titel
        /// </summary>
        public string Title
        {
            get { return ElementTitle.Title; }
            set { ElementTitle.Title = value; }
        }

        /// <summary>
        /// Liefert oder setzt das TitelElement
        /// </summary>
        private HtmlElementTitle ElementTitle { get; set; }

        /// <summary>
        /// Liefert oder setzt das Favicon
        /// </summary>
        public string Favicon
        {
            get { return ElementFavicon.Href; }
            set { ElementFavicon.Href = value; }
        }

        /// <summary>
        /// Liefert oder setzt den Favicon-Link
        /// </summary>
        private HtmlElementLink ElementFavicon { get; set; }

        /// <summary>
        /// Liefert oder setzt das internes Stylesheet  
        /// </summary>
        public List<string> Styles
        {
            get { return (from x in ElementStyles select x.Code).ToList(); }
            set { ElementStyles.Clear(); ElementStyles.AddRange(from x in value select new HtmlElementStyle(x)); }
        }

        /// <summary>
        /// Liefert oder setzt die Style-Elemente
        /// </summary>
        private List<HtmlElementStyle> ElementStyles { get; set; }

        /// <summary>
        /// Liefert oder setzt die Scripte  
        /// </summary>
        public List<string> Scripts
        {
            get { return (from x in ElementScripts select x.Code).ToList(); }
            set { ElementScripts.Clear(); ElementScripts.AddRange(from x in value select new HtmlElementScript(x)); }
        }

        /// <summary>
        /// Liefert oder setzt die Script-Elemente
        /// </summary>
        private List<HtmlElementScript> ElementScripts { get; set; }

        /// <summary>
        /// Liefert oder setzt das text/javascript
        /// </summary>
        public List<string> ScriptLinks
        {
            get { return (from x in ElementScriptLinks select x.Src).ToList(); }
            set { ElementScriptLinks.Clear(); ElementScriptLinks.AddRange(from x in value
                                                                          select new HtmlElementScript() { Language = "javascript", Src = x, Type = "text/javascript" });
            }
        }

        /// <summary>
        /// Liefert oder setzt die externen Scripts
        /// </summary>
        private List<HtmlElementScript> ElementScriptLinks { get; set; }

        /// <summary>
        /// Liefert oder setzt das internes Stylesheet  
        /// </summary>
        public List<string> CssLinks
        {
            get { return (from x in ElementCssLinks select x.Href).ToList(); }
            set
            {
                ElementCssLinks.Clear(); ElementCssLinks.AddRange(from x in value
                                                                  select new HtmlElementLink() { Rel = "stylesheet", Href = x, Type = "text/css" });
            }
        }

        /// <summary>
        /// Liefert oder setzt den Css-Link
        /// </summary>
        private List<HtmlElementLink> ElementCssLinks { get; set; }

        /// <summary>
        /// Liefert oder setzt die Metadaten
        /// </summary>
        public List<KeyValuePair<string, string>> Meta
        {
            get { return (from x in ElementMeta select new KeyValuePair<string, string>(x.Key, x.Value)).ToList(); }
            set
            {
                ElementMeta.Clear(); ElementMeta.AddRange(from x in value
                                                                  select new HtmlElementMeta(x.Key, x.Value));
            }
        }

        /// <summary>
        /// Liefert oder setzt die Metadaten-Elemente
        /// </summary>
        private List<HtmlElementMeta> ElementMeta { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public HtmlElementHead()
            : base("head")
        {
            ElementTitle = new HtmlElementTitle();
            ElementFavicon = new HtmlElementLink() { ID = "favicon", Rel = "shortcut icon", Type = "image/x-icon" };
            ElementStyles = new List<HtmlElementStyle>();
            ElementScripts = new List<HtmlElementScript>();
            ElementScriptLinks = new List<HtmlElementScript>();
            ElementCssLinks = new List<HtmlElementLink>();
            ElementMeta = new List<HtmlElementMeta>();
        }

        /// <summary>
        /// In String konvertieren unter Zuhilfenahme eines StringBuilder
        /// </summary>
        /// <param name="builder">Der StringBuilder</param>
        /// <param name="deep">Die Aufrufstiefe</param>
        public override void ToString(StringBuilder builder, int deep)
        {
            ToPreString(builder, deep);

            if (!string.IsNullOrWhiteSpace(Title))
            {
                ElementTitle.ToString(builder, deep + 1);
            }

            if (!string.IsNullOrWhiteSpace(Favicon))
            {
                ElementFavicon.ToString(builder, deep + 1);
            }

            foreach(var v in ElementStyles)
            {
                v.ToString(builder, deep + 1);
            }
            
            foreach (var v in ElementScriptLinks)
            {
                v.ToString(builder, deep + 1);
            }

            foreach (var v in ElementScripts)
            {
                v.ToString(builder, deep + 1);
            }

            foreach (var v in ElementCssLinks)
            {
                v.ToString(builder, deep + 1);
            }

            foreach (var v in ElementMeta)
            {
                v.ToString(builder, deep + 1);
            }

            ToPostString(builder, deep, true);
        }
    }
}
