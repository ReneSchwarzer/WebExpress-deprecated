using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WebExpress.Messages
{
    /// <summary>
    /// siehe RFC 2616
    /// </summary>
    public class Request
    {
        /// <summary>
        /// Setzt oder liefert den Anfragetyp
        /// </summary>
        public RequestMethod Method { get; private set; }

        /// <summary>
        /// Setzt oder liefert die URL
        /// </summary>
        public string URL { get; private set; }

        /// <summary>
        /// Setzt oder liefert den Client
        /// </summary>
        public string Client { get; private set; }

        /// <summary>
        /// Setzt oder liefert die Parameter
        /// </summary>
        internal Dictionary<string, Parameter> Param { get; private set; }

        /// <summary>
        /// Setzt oder liefert die HTTP-Version
        /// </summary>
        public string Version { get; private set; }

        /// <summary>
        /// Setzt oder liefert die Optionen im Header
        /// </summary>
        public RequestHeaderFields HeaderFields { get; private set; }

        /// <summary>
        /// Setzt oder liefert den Content
        /// </summary>
        public string Content { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        protected Request(RequestMethod type, string url, string version, RequestHeaderFields options, string content, string client)
        {
            Method = type;
            URL = url;
            Param = new Dictionary<string, Parameter>();
            Version = version;
            HeaderFields = options;
            Client = client;

            Content = content;
        }

        /// <summary>
        /// Parst den Request und erzeugt ein Request-Objekt
        /// </summary>
        /// <param name="request">Der Request in Stringform</param>
        /// <returns>Der Request als Objekt</returns>
        public static Request Parse(string request, string client)
        {
            if (string.IsNullOrWhiteSpace(request)) return null;

            var split = request.Split('\n');

            var methode = string.Join("\n", split.Take(1));
            var options = split.Skip(1).TakeWhile(x => !string.IsNullOrWhiteSpace(x)).ToList();
            var content = string.Join("\n", split.SkipWhile(x => !string.IsNullOrWhiteSpace(x))).Trim();

            var match = Regex.Match(methode, @"^(GET|POST|PUT|DELETE) (.*) (.*)$", RegexOptions.IgnoreCase);
            var type = RequestMethod.NONE;
            Match m = null;

            if (match.Groups.Count >= 2 && match.Groups[1].Success)
            {
                switch (match.Groups[1].Value.ToLower())
                {
                    case "get":
                        type = RequestMethod.GET;
                        break;
                    case "post":
                        type = RequestMethod.POST;
                        break;
                    case "put":
                        type = RequestMethod.PUT;
                        break;
                    case "delete":
                        type = RequestMethod.GET;
                        break;
                    case "head":
                        type = RequestMethod.HEAD;
                        break;
                }
            }

            var url = "";
            if (match.Groups.Count >= 3 && match.Groups[2].Success)
            {
                url = match.Groups[2].Value;
                m = Regex.Match(url, @"^(.*)\?(.*)$", RegexOptions.IgnoreCase);
                if (m.Success && m.Groups.Count >= 3 && match.Groups[1].Success && match.Groups[2].Success)
                {
                    url = m.Groups[1].Value;
                }
            }

            var version = "";
            if (match.Groups.Count >= 4 && match.Groups[3].Success)
            {
                version = match.Groups[3].Value;
            }

            var req = new Request(type, url, version, RequestHeaderFields.Parse(options), content, client);

            if (m.Success && m.Groups.Count >= 3 && match.Groups[1].Success && match.Groups[2].Success)
            {
                foreach (var v in m.Groups[2].Value.Split('&'))
                {
                    Regex regex = new Regex("[=]{1}");
                    var kv = regex.Split(v, 2);

                    req.AddParam(kv[0], kv.Count() > 1 ? kv[1] : "");
                }
            }

            return req;
        }

        /// <summary>
        /// Fügt ein Parameter hinzu
        /// </summary>
        /// <param name="name">Der Parametername</param>
        /// <param name="value">Der Wert</param>
        public void AddParam(string name, string value)
        {
            var decode = System.Web.HttpUtility.UrlDecode(value);

            if (!Param.ContainsKey(name.ToLower()))
            {
                Param.Add(name.ToLower(), new Parameter(name.ToLower(), decode) { Scope = ParameterScope.None });
            }
            else
            {
                Param[name.ToLower()] = new Parameter(name.ToLower(), decode) { Scope = ParameterScope.None };
            }
        }

        /// <summary>
        /// Liefert ein Parameter anhand seines Namens
        /// </summary>
        /// <param name="name">Der Name des Parameters</param>
        /// <returns>Der Wert</returns>
        public string GetParam(string name)
        {
            if (HasParam(name))
            {
                return Param[name.ToLower()].Value;
            }

            return string.Empty;
        }

        /// <summary>
        /// Prüft, ob ein Parameter vorhanden ist
        /// </summary>
        /// <param name="name">Der Name des Parameters</param>
        /// <returns>true wenn Parameter vorhanden ist, false sonst</returns>
        public bool HasParam(string name)
        {
            return Param.ContainsKey(name.ToLower());
        }
    }
}
