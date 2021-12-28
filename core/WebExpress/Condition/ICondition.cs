using WebExpress.Message;

namespace WebExpress.Condition
{
    /// <summary>
    /// Stellt eine Bedingiung dar, die erfüllt sein muss
    /// </summary>
    public interface ICondition
    {
        /// <summary>
        /// Die Bedingung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>true wenn die Bedingung erfüllt ist, false sonst</returns>
        bool Fulfillment(Request request);
    }
}
