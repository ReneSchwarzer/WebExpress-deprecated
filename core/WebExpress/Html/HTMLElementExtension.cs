using System;
using System.Collections.Generic;
using System.Linq;

namespace WebExpress.Html
{
    /// <summary>
    /// Erweiterungsmethoden für HTMLElemente
    /// </summary>
    public static class HTMLElementExtension
    {
        /// <summary>
        /// Fügt eine Klasse hinzu
        /// </summary>
        /// <param name="html">Das zu erweiternde HTML-Element</param>
        /// <param name="cssClass">Die Klasse, welche hinzugefügt werden soll</param>
        /// <returns>Des um die Kasse erweiterte HTML-Element</returns>
        public static IHtmlNode AddClass(this IHtmlNode html, string cssClass)
        {
            if (html is HtmlElement)
            {
                var element = html as HtmlElement;

                var list = new List<string>(element.Class.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)).Select(x => x.ToLower()).ToList();

                if (!list.Contains(cssClass.ToLower()))
                {
                    list.Add(cssClass.ToLower());
                }

                var css = string.Join(' ', list);

                element.Class = css;
            }

            return html;
        }

        /// <summary>
        /// Entfernt eine Klasse
        /// </summary>
        /// <param name="html">Das HTML-Element</param>
        /// <param name="cssClass">Die Klasse, welche entfernt werden soll</param>
        /// <returns>Des um die Kasse reduzierte HTML-Element</returns>
        public static IHtmlNode RemoveClass(this IHtmlNode html, string cssClass)
        {
            if (html is HtmlElement)
            {
                var element = html as HtmlElement;

                var list = new List<string>(element.Class.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)).Select(x => x.ToLower()).ToList();

                if (list.Contains(cssClass.ToLower()))
                {
                    list.Remove(cssClass.ToLower());
                }

                var css = string.Join(' ', list);

                element.Class = css;
            }

            return html;
        }

        /// <summary>
        /// Fügt ein Style hinzu
        /// </summary>
        /// <param name="html">Das zu erweiternde HTML-Element</param>
        /// <param name="cssStyle">Die Klasse, welche hinzugefügt werden soll</param>
        /// <returns>Des um die Kasse erweiterte HTML-Element</returns>
        public static IHtmlNode AddStyle(this IHtmlNode html, string cssStyle)
        {
            if (html is HtmlElement)
            {
                var element = html as HtmlElement;

                var list = new List<string>(element.Style.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)).Select(x => x.ToLower()).ToList();

                if (!list.Contains(cssStyle.ToLower()))
                {
                    list.Add(cssStyle.ToLower());
                }

                var css = string.Join(' ', list);

                element.Style = css;
            }

            return html;
        }

        /// <summary>
        /// Entfernt ein Style
        /// </summary>
        /// <param name="html">Das HTML-Element</param>
        /// <param name="cssStyle">Der Style, welcher entfernt werden soll</param>
        /// <returns>Des um die Kasse reduzierte HTML-Element</returns>
        public static IHtmlNode RemoveStyle(this IHtmlNode html, string cssStyle)
        {
            if (html is HtmlElement)
            {
                var element = html as HtmlElement;

                var list = new List<string>(element.Style.Split(" ".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)).Select(x => x.ToLower()).ToList();

                if (list.Contains(cssStyle?.ToLower()))
                {
                    list.Remove(cssStyle.ToLower());
                }

                var css = string.Join(' ', list);

                element.Style = css;
            }

            return html;
        }
    }
}
