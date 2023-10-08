using System.Collections.Generic;
using WebExpress.WebApp.WebIndex;

namespace WebExpress.WebApp.WebIndex.Wql.Function
{
    /// <summary>
    /// Describes the function expression of a wql statement.
    /// </summary>
    public abstract class WqlExpressionNodeFilterFunction<T> : IWqlExpressionNode<T> where T : IIndexItem
    {
        /// <summary>
        /// Returns the function name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Returns the parameter expressions.
        /// </summary>
        public IEnumerable<WqlExpressionNodeParameter<T>> Parameters { get; internal set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The function name</param>
        protected WqlExpressionNodeFilterFunction(string name)
        {
            Name = name.ToLower();
        }

        /// <summary>
        /// Executes the function.
        /// </summary>
        /// <returns>The return value.</returns>
        public abstract object Execute();

        /// <summary>
        /// Converts the function expression to a string.
        /// </summary>
        /// <returns>The function expression as a string.</returns>
        public override string ToString()
        {
            return string.Format("{0}({1})", Name, string.Join(", ", Parameters)).Trim();
        }
    }
}