using WebExpress.WebCondition;
using WebExpress.Message;
using System;

namespace WebExpress.WebApp.WebCondition
{
    public class ConditionUnix : ICondition
    {
        /// <summary>
        /// Die Bedingung
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <returns>true wenn die Bedingung erfüllt ist, false sonst</returns>
        public bool Fulfillment(Request request)
        {
            return Environment.OSVersion.ToString().Contains("unix", StringComparison.OrdinalIgnoreCase);
        }
    }
}
