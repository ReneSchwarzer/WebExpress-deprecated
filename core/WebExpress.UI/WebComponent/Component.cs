﻿using WebExpress.UI.WebControl;

namespace WebExpress.UI.WebComponent
{
    public class Component<T> : IComponent where T : Control, new()
    {
        /// <summary>
        /// Liefert der Kontext
        /// </summary>
        public ComponentContext Context { get; set; }
    }
}