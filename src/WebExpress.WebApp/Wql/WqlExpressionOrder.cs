﻿using System.Collections.Generic;
using System.Linq;

namespace WebExpress.WebApp.Wql
{
    /// <summary>
    /// Describes the order expression of a wql statement.
    /// </summary>
    public class WqlExpressionOrder : IWqlExpression
    {
        /// <summary>
        /// Returns the order attribute expressions.
        /// </summary>
        public IReadOnlyList<WqlExpressionOrderAttribute> Attributes { get; internal set; }

        /// <summary>
        /// Constructor
        /// </summary>
        internal WqlExpressionOrder()
        {
        }

        /// <summary>
        /// Applies the filter to the unfiltered data object.
        /// </summary>
        /// <param name="unfiltered">The unfiltered data.</param>
        /// <returns>The filtered data.</returns>
        public IQueryable<T> Apply<T>(IQueryable<T> unfiltered)
        {
            var filtered = unfiltered;

            foreach (var attribute in Attributes)
            {
                filtered = attribute.Apply(filtered);
            }

            return filtered.AsQueryable();
        }

        /// <summary>
        /// Converts the order expression to a string.
        /// </summary>
        /// <returns>The order expression as a string.</returns>
        public override string ToString()
        {
            return string.Format("order by {0}", string.Join(", ", Attributes)).Trim();
        }
    }
}