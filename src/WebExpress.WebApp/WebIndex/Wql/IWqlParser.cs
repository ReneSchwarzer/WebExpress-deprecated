using WebExpress.WebApp.WebIndex.Wql.Condition;
using WebExpress.WebApp.WebIndex.Wql.Function;

namespace WebExpress.WebApp.WebIndex.Wql
{
    public interface IWqlParser<T> where T : IIndexItem
    {
        /// <summary>
        /// Parses a given wql query.
        /// </summary>
        /// <param name="input">An input string that contains a wql query.</param>
        /// <param name="culture">The culture in which to run the wql.</param>
        /// <returns>A wql object that represents the structure of the query.</returns>
        IWqlStatement<T> Parse(string input);

        /// <summary>
        /// Registers a condition expression.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="WqlParseException"></exception>
        void RegisterCondition<C>() where C : WqlExpressionNodeFilterCondition<T>, new();

        /// <summary>
        /// Registers a function expression.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="WqlParseException"></exception>
        void RegisterFunction<F>() where F : WqlExpressionNodeFilterFunction<T>, new();

        /// <summary>
        /// Removes a condition expression.
        /// </summary>
        /// <param name="op">The operator to be derisgistrated.</param>
        void RemoveCondition(string op);

        /// <summary>
        /// Removes a function expression.
        /// </summary>
        /// <param name="name">The function name to be derisgistrated.</param>
        void RemoveFunction(string name);
    }
}
