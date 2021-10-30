using System;
using System.Collections.Generic;

namespace WebExpress.Session
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
        /// Liefert oder setzt die Zeit des letzten Zugriffes
        /// </summary>
        public DateTime Updated { get; set; }

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
            Updated = DateTime.Now;

            Properties = new Dictionary<Type, ISessionProperty>();
        }

        /// <summary>
        /// Liefert ein Property
        /// </summary>
        /// <typeparam name="T">Der Type der Property</typeparam>
        /// <returns>Die Property oder null</returns>
        public T GetProperty<T>() where T : class, ISessionProperty, new()
        {
            lock (Properties)
            {
                if (Properties.ContainsKey(typeof(T)))
                {
                    return Properties[typeof(T)] as T;
                }
            }

            return default;
        }

        /// <summary>
        /// Liefert ein Property wenn dise bereits existiert. Ansonsten wird eine neue Property erstellt.
        /// </summary>
        /// <typeparam name="T">Der Type der Property</typeparam>
        /// <returns>Die Property</returns>
        public T GetOrCreateProperty<T>() where T : class, ISessionProperty, new()
        {
            lock (Properties)
            {
                if (Properties.ContainsKey(typeof(T)))
                {
                    return Properties[typeof(T)] as T;
                }

                var property = new T();
                SetProperty(property);

                return property;
            }
        }

        /// <summary>
        /// Setzt ein Property
        /// </summary>
        /// <param name="property">Die zu setzende Eigenschaft</param>
        public void SetProperty(ISessionProperty property)
        {
            lock (Properties)
            {
                if (!Properties.ContainsKey(property.GetType()))
                {
                    Properties.Add(property.GetType(), property);
                }

                Properties[property.GetType()] = property;
            }
        }

        /// <summary>
        /// Entfernt ein Property
        /// </summary>
        /// <typeparam name="T">Der Type der Property</typeparam>
        public void RemoveProperty<T>() where T : class, ISessionProperty, new()
        {
            lock (Properties)
            {
                if (Properties.ContainsKey(typeof(T)))
                {
                    Properties.Remove(typeof(T));
                }
            }
        }

    }
}
