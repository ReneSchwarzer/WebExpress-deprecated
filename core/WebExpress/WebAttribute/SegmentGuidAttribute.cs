using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using WebExpress.Internationalization;
using WebExpress.WebResource;

namespace WebExpress.WebAttribute
{
    public class SegmentGuidAttribute : Attribute, IResourceAttribute, ISegmentAttribute
    {
        /// <summary>
        /// The display formats of the guid.
        /// </summary>
        public enum Format { Full, Simple }

        /// <summary>
        /// Returns or sets the name of the variable.
        /// </summary>
        private string VariableName { get; set; }

        /// <summary>
        /// Returns or sets the display string.
        /// </summary>
        private string Display { get; set; }


        /// <summary>
        /// Delivers or sets the display format.
        /// </summary>
        private Format DisplayFormat { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="variableName">The name of the variable.</param>
        /// <param name="display">The display string.</param>
        /// <param name="displayFormat">The display format.</param>
        public SegmentGuidAttribute(string variableName, string display, Format displayFormat = Format.Simple)
        {
            VariableName = variableName;
            Display = display;
            DisplayFormat = displayFormat;
        }

        /// <summary>
        /// Conversion to a path segment.
        /// </summary>
        /// <returns>The path segment.</returns>
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
