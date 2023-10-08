using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace WebExpress.WebApp.WebIndex.Wql
{
    public class WqlStatement<T> : IWqlStatement<T> where T : IIndexItem
    {
        /// <summary>
        /// Returns the index document.
        /// </summary>
        public IIndexDocument<T> IndexDocument { get; set; }

        /// <summary>
        /// Returns the original wql statement.
        /// </summary>
        public string Raw { get; internal set; }

        /// <summary>
        /// Returns the filter expression.
        /// </summary>
        public WqlExpressionNodeFilter<T> Filter { get; internal set; }

        /// <summary>
        /// Returns the order expression.
        /// </summary>
        public WqlExpressionNodeOrder<T> Order { get; internal set; }

        /// <summary>
        /// Returns the partitioning expression.
        /// </summary>
        public WqlExpressionNodePartitioning<T> Partitioning { get; internal set; }

        /// <summary>
        /// Returns true if there are any errors that occurred during parsing, false otherwise.
        /// </summary>
        public bool HasErrors => Error != null;

        /// <summary>
        /// Returns the part in error of the original wql statement.
        /// </summary>
        public WqlExpressionError Error { get; internal set; }

        /// <summary>
        /// Returns the culture in which to run the wql.
        /// </summary>
        public CultureInfo Culture { get; internal set; }

        /// <summary>
        /// Returns the syntax tree of the wql query.
        /// </summary>
        public IEnumerable<IWqlExpressionNode<T>> SyntaxTree
        {
            get
            {
                var nodes = new List<IWqlExpressionNode<T>>();

                if (Filter != null)
                {
                    nodes.Add(Filter);
                }

                if (Order != null)
                {
                    nodes.Add(Order);
                }

                if (Partitioning != null)
                {
                    nodes.Add(Partitioning);
                }

                return nodes;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="raw">The original wql statement.</param>
        internal WqlStatement(string raw)
        {
            Raw = raw;
        }

        /// <summary>
        /// Applies the filter to the index.
        /// </summary>
        /// <returns>The data from the index.</returns>
        public IQueryable<T> Apply()
        {
            var filtered = Enumerable.Empty<T>().AsQueryable();

            if (Filter != null)
            {
                filtered = Filter.Apply().Select(x => IndexDocument.ForwardIndex.GetItem(x)).AsQueryable();
            }
            else
            {
                filtered = IndexDocument?.ForwardIndex.All.AsQueryable();
            }

            if (Order != null)
            {
                filtered = Order.Apply(filtered);
            }

            if (Partitioning != null)
            {
                filtered = Partitioning.Apply(filtered);
            }

            return filtered;
        }

        /// <summary>
        /// Applies the filter to the unfiltered data object.
        /// </summary>
        /// <param name="unfiltered">The unfiltered data.</param>
        /// <returns>The filtered data.</returns>
        public IQueryable<T> Apply(IQueryable<T> unfiltered)
        {
            var filtered = unfiltered;

            if (Filter != null)
            {
                filtered = Filter.Apply(filtered);
            }

            if (Order != null)
            {
                filtered = Order.Apply(filtered);
            }

            if (Partitioning != null)
            {
                filtered = Partitioning.Apply(filtered);
            }

            return filtered;
        }

        /// <summary>
        /// Returns the sql query string.
        /// </summary>
        /// <returns>The sql part of the node.</returns>
        public string GetSqlQueryString()
        {
            var sql = new StringBuilder();
            var name = typeof(T).Name;

            sql.Append($"select * from {name}");

            if (Filter != null)
            {
                sql.Append($" where {Filter.GetSqlQueryString()}");
            }

            //if (Order != null)
            //{
            //    sql.Add(Order);
            //}

            //if (Partitioning != null)
            //{
            //    sql.Add(Partitioning);
            //}

            return sql.ToString();
        }

        /// <summary>
        /// Converts the WQL expression to a string.
        /// </summary>
        /// <returns>The WQL expression as a string.</returns>
        public override string ToString()
        {
            if (Error != null)
            {
                return Raw;
            }

            return string.Format
            (
                "{0} {1} {2} {3}",
                Filter != null ? Filter.ToString() : "",
                Order != null ? Order.ToString() : "",
                Partitioning != null ? Partitioning.ToString() : "",
                Error != null ? Error.ToString() : ""
            ).Trim();
        }
    }
}