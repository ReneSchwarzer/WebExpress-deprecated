﻿using System.Collections.Generic;
using System.Linq;
using WebExpress.Html;
using WebExpress.Uri;
using WebExpress.WebPage;

namespace WebExpress.WebResource
{
    /// <summary>
    /// Die inhaltliche Gestanltung einer Seite (Page) wird durch den visuellen Baum bestimmt.
    /// </summary>
    public abstract class VisualTree : IVisualTree
    {
        /// <summary>
        /// Liefert oder setzt den Titel
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Liefert die Favicons
        /// </summary>
        public List<Favicon> Favicons { get; } = new List<Favicon>();

        /// <summary>
        /// Liefert die internen Stylesheets 
        /// </summary>
        public List<string> Styles { get; } = new List<string>();

        /// <summary>
        /// Liefert die Links auf die zu verwendenden JavaScript-Dateien, welche im Header eingefügt werden
        /// </summary>
        public List<IUri> HeaderScriptLinks { get; } = new List<IUri>();

        /// <summary>
        /// Liefert die Links auf die zu verwendenden JavaScript-Dateien
        /// </summary>
        public List<string> ScriptLinks { get; } = new List<string>();

        /// <summary>
        /// Liefert die Links auf die zu verwendenden JavaScript-Dateien, welche im Header eingefügt werden
        /// </summary>
        public List<string> HeaderScripts { get; } = new List<string>();

        /// <summary>
        /// Liefert die Links auf die zu verwendenden JavaScript-Dateien
        /// </summary>
        public IDictionary<string, string> Scripts { get; } = new Dictionary<string, string>();

        /// <summary>
        /// Liefert die Links auf die zu verwendenden Css-Dateien
        /// </summary>
        public List<IUri> CssLinks { get; } = new List<IUri>();

        /// <summary>
        /// Liefert die Metainformationen
        /// </summary>
        public List<KeyValuePair<string, string>> Meta { get; } = new List<KeyValuePair<string, string>>();

        /// <summary>
        /// Liefert den Inhalt 
        /// </summary>
        public IHtmlNode Content { get; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public VisualTree()
        {
        }

        /// <summary>
        /// Fügt eine Java-Script hinzu oder sersetzt dieses, falls vorhanden
        /// </summary>
        /// <param name="key">Der Schlüssel</param>
        /// <param name="code">Der Code</param>
        public virtual void AddScript(string key, string code)
        {
            if (key == null) return;

            var k = key.ToLower();
            var dict = Scripts;

            if (dict.ContainsKey(k))
            {
                dict[k] = code;
            }
            else
            {
                dict?.Add(k, code);
            }
        }

        /// <summary>
        /// Fügt eine Java-Script hinzu
        /// </summary>
        /// <param name="url">Der Link</param>
        public virtual void AddScriptLink(string url)
        {
            ScriptLinks?.Add(url);
        }

        /// <summary>
        /// In HTML konvertieren
        /// </summary>
        /// <param name="context">Der Kontext zum Rendern der Seite</param>
        /// <returns>Die Seite als HTML-Baum</returns>
        public virtual IHtmlNode Render(RenderContext context)
        {
            var html = new HtmlElementRootHtml();
            html.Head.Title = Title;
            html.Head.Favicons = Favicons?.Select(x => new Favicon(new UriRelative(x.Url).ToString(), x.Mediatype));
            //html.Head.Base = Context.ContextPath.ToString();
            html.Head.Styles = Styles?.Select(x => new UriRelative(x).ToString());
            html.Head.Meta = Meta;
            html.Head.Scripts = HeaderScripts;
            html.Body.Elements.Add(Content);
            html.Body.Scripts = Scripts.Values.ToList();

            html.Head.CssLinks = CssLinks.Where(x => x != null).Select(x => x.ToString());
            html.Head.ScriptLinks = HeaderScriptLinks?.Where(x => x != null).Select(x => x.ToString());

            return html;
        }
    }
}