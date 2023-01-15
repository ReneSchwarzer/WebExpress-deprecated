using System;
using System.Collections.Generic;

namespace WebExpress.WebComponent
{
    /// <summary>
    /// Internal management of components.
    /// key = type of manager
    /// value = instance of manager
    /// </summary>
    public class ComponentDictionary : Dictionary<Type, IComponent>
    {

    }
}
