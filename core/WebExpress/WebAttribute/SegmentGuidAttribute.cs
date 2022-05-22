using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using WebExpress.Internationalization;
using WebExpress.WebResource;

namespace WebExpress.WebAttribute
{
    public class SegmentGuidAttribute : System.Attribute, IResourceAttribute, ISegmentAttribute
    {
        /// <summary>
        /// Die Anzeigeformate der Guid
        /// </summary>
        public enum Format { Full, Simple }

        /// <summary>
        /// Liefert oder setzt den Namen der Variable
        /// </summary>
        private string VariableName { get; set; }

        /// <summary>
        /// Liefert oder setzt den Anzeigestring
        /// </summary>
        private string Display { get; set; }


        /// <summary>
        /// Liefert oder setzt das Anzeigeformat
        /// </summary>
        private Format DisplayFormat { get; set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="variableName">Der Name der Variabel</param>
        /// <param name="display">Der Anzeigestring</param>
        /// <param name="displayStyle">Das Anzeigeformat</param>
        public SegmentGuidAttribute(string variableName, string display, Format displayFotmat = Format.Simple)
        {
            VariableName = variableName;
            Display = display;
            DisplayFormat = displayFotmat;
        }

        /// <summary>
        /// Umwandlung in ein Pfadsegment
        /// </summary>
        /// <returns>Das Pfadsegment</returns>
        public IPathSegment ToPathSegment()
        {
            var expression = @"^(\{){0,1}(([0-9a-fA-F]{8})\-([0-9a-fA-F]{4})\-([0-9a-fA-F]{4})\-([0-9a-fA-F]{4})\-([0-9a-fA-F]{12}))(\}){0,1}$";

            var callBackDisplay = new Func<string, string, CultureInfo, string>((segment, moduleID, culture) =>
            {
                var match = Regex.Match(segment, expression, RegexOptions.IgnoreCase | RegexOptions.Compiled);

                if (match.Success && string.IsNullOrWhiteSpace(Display))
                {
                    return match.Groups[7].ToString();
                }
                else if (match.Success)
                {
                    var display = string.Format
                    (
                        InternationalizationManager.I18N(culture, moduleID, Display),
                        DisplayFormat == Format.Simple ? match.Groups[7].ToString() : match.Groups[2].ToString()
                    );

                    return display;
                }

                return null;
            });

            var callBackValiables = new Func<string, IDictionary<string, string>>(segment =>
            {
                var match = Regex.Match(segment, expression, RegexOptions.IgnoreCase | RegexOptions.Compiled);

                if (match.Success)
                {
                    var dict = new Dictionary<string, string>
                    {
                        { VariableName, match.Groups[2].ToString() }
                    };

                    return dict;
                }

                return null;
            });

            return new PathSegmentVariable(expression, callBackDisplay, callBackValiables);
        }
    }
}
