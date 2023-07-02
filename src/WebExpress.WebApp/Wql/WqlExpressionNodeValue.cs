using System;
using System.Collections.Generic;
using System.Globalization;

namespace WebExpress.WebApp.Wql
{
    /// <summary>
    /// Describes the value expression of a wql statement.
    /// </summary>
    public class WqlExpressionNodeValue : IWqlExpressionNode
    {
        /// <summary>
        /// Returns the value as string.
        /// </summary>
        public string StringValue { get; internal set; }

        /// <summary>
        /// Returns the value as double.
        /// </summary>
        public double? NumberValue { get; internal set; }

        /// <summary>
        /// Returns the culture in which to run the wql.
        /// </summary>
        public CultureInfo Culture { get; internal set; }

        /// <summary>
        /// Constructor
        /// </summary>
        internal WqlExpressionNodeValue()
        {
        }

        /// <summary>
        /// Returns the value.
        /// </summary>
        /// <returns>The value.</returns>
        public object GetValue()
        {
            return NumberValue.HasValue ? 
                NumberValue.Value : (object)StringValue;
        }

        /// <summary>
        /// Converts the value to a string.
        /// </summary>
        /// <param name="value">The value.</param>
        public static explicit operator string(WqlExpressionNodeValue value)
        {
            return value.GetValue().ToString();
        }

        /// <summary>
        /// Converts the value expression to a string.
        /// </summary>
        /// <returns>The value expression as a string.</returns>
        public override string ToString()
        {
            var value = GetValue();

            return string.Format
            (
                "{0}",
                value is string ? "'" + value + "'" : Convert.ToString(value, Culture)
            ).Trim();
        }
    }
}