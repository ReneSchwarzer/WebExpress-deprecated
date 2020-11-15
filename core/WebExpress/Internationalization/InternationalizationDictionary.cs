using System.Collections.Generic;

namespace WebExpress.Internationalization
{
    public class InternationalizationDictionary : Dictionary<string, InternationalizationDictionaryItem>
    {
        // public  Dictionary { get; private set; } = new Dictionary<string, InternationalizationDictionaryItem>();

        /// <summary>
        /// Instanz des einzigen Modells
        /// </summary>
        private static InternationalizationDictionary _this = null;

        /// <summary>
        /// Lifert die einzige Instanz der Modell-Klasse
        /// </summary>
        public static InternationalizationDictionary Instance
        {
            get
            {
                if (_this == null)
                {
                    _this = new InternationalizationDictionary();
                }

                return _this;
            }
        }

        /// <summary>
        /// Konstruktor
        /// </summary>
        private InternationalizationDictionary()
        {

        }
    }
}
