using System;
using System.Collections.Generic;

namespace WebExpress.Workers
{
    public class Session
    {
        /// <summary>
        /// Liefert oder setzt die ID
        /// </summary>
        public Guid ID { get; private set; }

        /// <summary>
        /// Liefert oder setzt die Erstellungszeit
        /// </summary>
        public DateTime Created { get; private set; }

        /// <summary>
        /// Liefert oder setzt Eingenschaften zur Session
        /// </summary>
        public Dictionary<Type, ISessionProperty> Properties { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Session()
            : this(Guid.NewGuid())
        {
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        public Session(Guid id)
        {
            ID = id;
            Created = DateTime.Now;

            Properties = new Dictionary<Type, ISessionProperty>();
        }

        /// <summary>
        /// Liefert ein Property
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetProperty<T>() where T : class, ISessionProperty, new()
        {
            if (Properties.ContainsKey(typeof(T)))
            {
                return Properties[typeof(T)] as T;
            }

            return default;
        }

        /// <summary>
        /// Setzt ein Property
        /// </summary>
        /// <param name="property">Die zu setzende Eigenschaft</param>
        public void SetProperty(ISessionProperty property)
        {
            if (!Properties.ContainsKey(property.GetType()))
            {
                Properties.Add(property.GetType(), property);
            }

            Properties[property.GetType()] = property;
        }

        /// <summary>
        /// Entfernt ein Property
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public void RemoveProperty<T>() where T : class, ISessionProperty, new()
        {
            if (Properties.ContainsKey(typeof(T)))
            {
                Properties.Remove(typeof(T));
            }
        }

    }
}
