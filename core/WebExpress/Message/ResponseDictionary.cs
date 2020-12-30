using System;
using System.Collections.Generic;
using WebExpress.Application;

namespace WebExpress.Message
{
    public class ResponseDictionary : Dictionary<IApplicationContext, Dictionary<int, Type>>
    {
    }
}
