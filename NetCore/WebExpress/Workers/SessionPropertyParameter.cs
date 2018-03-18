using System;
using System.Collections.Generic;
using System.Text;
using WebExpress.Messages;

namespace WebExpress.Workers
{
    public class SessionPropertyParameter : SessionProperty
    {
        /// <summary>
        /// Setzt oder liefert die Parameter
        /// </summary>
        public Dictionary<string, Parameter> Params { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public SessionPropertyParameter()
        {
            Params = new Dictionary<string, Parameter>();
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="param">Die Parameter</param>
        public SessionPropertyParameter(Dictionary<string, Parameter> param)
        {
            Params = param;
        }
    }
}
