using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using WebExpress.Html;
using WebExpress.Session;
using WebExpress.Uri;

namespace WebExpress.Message
{
    /// <summary>
    /// siehe RFC 2616, Die Request-Klasse kapselt den orginalen Request des HttpListener-Aufrufes und erweitert diesen.
    /// </summary>
    public class Request
    {
        /// <summary>
        /// Liefert den orginalen Request
        /// </summary>
        private HttpListenerRequest RawRequuest { get; }

        /// <summary>
        /// Liefert den Anfragetyp
        /// </summary>
        public RequestMethod Method { get; private set; }

        /// <summary>
        /// Liefert die URL
        /// </summary>
        public IUri Uri { get; internal set; }

        /// <summary>
        /// Liefert die URL
        /// </summary>
        public IUri BaseUri { get; internal set; }

        /// <summary>
        /// Liefert den Client
        /// </summary>
        public string Client { get; private set; }

        /// <summary>
        /// Setzt oder liefert die Parameter
        /// </summary>
        private ParameterDictionary Param { get; } = new ParameterDictionary();

        /// <summary>
        /// Liefert die Session
        /// </summary>
        public Session.Session Session { get; private set; }

        /// <summary>
        /// Setzt oder liefert die HTTP-Version
        /// </summary>
        public Version Version => RawRequuest?.ProtocolVersion;

        /// <summary>
        /// Setzt oder liefert die Optionen im Header
        /// </summary>
        public RequestHeaderFields Header { get; private set; }

        /// <summary>
        /// Ruft die IP-Adresse und Anschlussnummer des Servers ab, an den die Anforderung gerichtet ist.
        /// </summary>
        public IPEndPoint LocalEndPoint => RawRequuest?.LocalEndPoint;

        /// <summary>
        /// Ruft die IP-Adresse und Anschlussnummer des Clients ab, von dem die Anforderung stammt.
        /// </summary>
        public IPEndPoint RemoteEndPoint => RawRequuest?.RemoteEndPoint;

        /// <summary>
        /// Ruft einen Boolean-Wert ab, der angibt, ob der diese Anforderung sendende Client authentifiziert ist.
        /// </summary>
        public bool IsAuthenticated => RawRequuest.IsAuthenticated;

        /// <summary>
        /// Ruft einen Boolean-Wert ab, der angibt, ob die Anforderung vom lokalen Computer gesendet wurde.
        /// </summary>
        public bool IsLocal => RawRequuest.IsLocal;

        /// <summary>
        /// Ruft einen Boolean-Wert ab, der angibt, ob die TCP-Verbindung, mit der die Anforderung gesendet wird, das SSL (Secure Sockets Layer)-Protokoll verwendet.
        /// </summary>
        public bool IsSecureConnection => RawRequuest.IsSecureConnection;

        /// <summary>
        /// Ruft einen Boolean-Wert ab, der angibt, ob die TCP Verbindung eine WEbSocket Anforderung war.
        /// </summary>
        public bool IsWebSocketRequest => RawRequuest.IsWebSocketRequest;

        /// <summary>
        /// Ruft einen Boolean-Wert ab, der angibt, ob der Client eine permanente Verbindung anfordert.
        /// </summary>
        public bool KeepAlive => RawRequuest.KeepAlive;

        /// <summary>
        /// Ruft den Anforderungsbezeichner der eingehenden HTTP-Anforderung ab.
        /// </summary>
        public Guid RequestTraceIdentifier => RawRequuest.RequestTraceIdentifier;

        /// <summary>
        /// Ruft den Anforderungsbezeichner der eingehenden HTTP-Anforderung ab.
        /// </summary>
        public string UserHostName => RawRequuest?.UserHostName;

        /// <summary>
        /// Ruft die IP-Adresse und Anschlussnummer des Servers ab, an den die Anforderung gerichtet ist.
        /// </summary>
        public string UserHostAddress => RawRequuest?.UserHostAddress;

        /// <summary>
        /// Liefert den Inhalt
        /// </summary>
        public byte[] Content { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="request">Der Request, welcher vom HttpListener erzeugt wurde</param>
        internal Request(HttpListenerRequest request)
        {
            RawRequuest = request;
            Method = request.HttpMethod.ToLower() switch
            {
                "get" => RequestMethod.GET,
                "post" => RequestMethod.POST,
                "put" => RequestMethod.PUT,
                "delete" => RequestMethod.GET,
                "head" => RequestMethod.HEAD,
                _ => RequestMethod.GET
            };

            Uri = new UriRelative(RawRequuest.RawUrl);
            BaseUri = new UriAbsolute(RawRequuest.Url.OriginalString.Substring(0, RawRequuest.Url.OriginalString.Length - RawRequuest.RawUrl.Length));
            Header = new RequestHeaderFields(request);

            Content = GetContent(request);
            ParseRequestParams();
            ParseSessionParams();
        }

        /// <summary>
        /// Ermittelt den Content
        /// </summary>
        /// <param name="request">Der Request, welcher vom HttpListener erzeugt wurde</param>
        /// <returns>Der Content</returns>
        private static byte[] GetContent(HttpListenerRequest request)
        {
            var capacity = 1024;
            var buffer = new byte[capacity];
            var offset = 0;

            var content = new byte[request.ContentLength64];

            int readCount;
            // Lese Content
            do
            {
                readCount = request.InputStream.Read(buffer, offset, capacity);

                if (readCount > 0)
                {
                    Buffer.BlockCopy(buffer, 0, content, offset, readCount);

                    offset += readCount;
                }
            }
            while (readCount < 0);

            return content;
        }

        /// <summary>
        /// Ermittelt den Content einer Anfrage
        /// </summary>
        private void ParseRequestParams()
        {
            if (string.IsNullOrWhiteSpace(Header.ContentType))
            {
                return;
            }
            
            var contentType = Header.ContentType?.Split(';');

            switch (TypeEnctypeExtensions.Convert(contentType.FirstOrDefault()))
            {
                case TypeEnctype.None:
                    {
                        var boundary = Header.ContentType;
                        var boundaryValue = "--" + boundary?.Split('=').Skip(1)?.FirstOrDefault();
                        var offset = 0;
                        int pos = 0;
                        var dispositions = new List<Tuple<int, int>>(); // Item1=Position, Item2=Länge

                        // ermittle Dispositionen
                        for (var i = 0; i < Content.Length; i++)
                        {
                            if (Content[i] == '\r')
                            {
                                var c = Encoding.UTF8.GetString(Content, offset, boundaryValue.Length).Trim();
                                if (c.StartsWith(boundaryValue))
                                {
                                    if (i - boundaryValue.Length - pos > 0)
                                    {
                                        dispositions.Add(new Tuple<int, int>(pos, i - boundaryValue.Length - pos));
                                    }

                                    pos = i + 2;

                                    if (c.EndsWith("--"))
                                    {
                                        break;
                                    }
                                }
                            }
                            else if (Content[i] == '\n')
                            {
                                offset = i + 1;
                            }
                            else if (i == Content.Length - 1)
                            {
                                // Am Ende
                                var c = Encoding.UTF8.GetString(Content, offset, boundaryValue.Length).Trim();
                                if (c.StartsWith(boundaryValue))
                                {
                                    dispositions.Add(new Tuple<int, int>(pos, i - boundaryValue.Length - pos));
                                }
                            }
                        }

                        foreach (var item in dispositions)
                        {
                            var disposition = string.Empty;
                            var name = string.Empty;
                            var filename = string.Empty;
                            var contenttype = string.Empty;
                            offset = 0;

                            var str = Encoding.UTF8.GetString(Content, item.Item1, item.Item2 > 256 ? 256 : item.Item2);
                            var match = Regex.Match(str, @"^Content-Disposition: (.*)$", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
                            if (match.Groups[1].Success)
                            {
                                offset += match.Length + 1; // + Zeilenende
                                var dispositionParam = match.Groups[1].ToString().Split(';');
                                disposition = dispositionParam.FirstOrDefault();
                                foreach (var v in dispositionParam.Skip(1))
                                {
                                    match = Regex.Match(v.Trim(), @"^name=""(.*)""$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                                    if (match.Groups[1].Success)
                                    {
                                        name = match.Groups[1].ToString();
                                    }

                                    match = Regex.Match(v.Trim(), @"^filename=""(.*)""$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                                    if (match.Groups[1].Success)
                                    {
                                        filename = match.Groups[1].ToString();
                                    }
                                }
                            }

                            match = Regex.Match(str, @"^Content-Type: (.*)$", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Compiled);
                            if (match.Groups[1].Success)
                            {
                                offset += match.Length + 1; // + Zeilenende
                                contenttype = match.Groups[1].ToString().Trim();
                            }

                            if (string.IsNullOrWhiteSpace(filename))
                            {
                                offset += 2; // + Leerzeile
                                if (item.Item2 - offset - 1 >= 0)
                                {
                                    var value = Encoding.UTF8.GetString(Content, item.Item1 + offset, item.Item2 - offset - 2);

                                    var param = new Parameter(name, value.TrimEnd(), ParameterScope.Parameter);
                                    AddParameter(param);
                                }
                                else
                                {
                                    var param = new Parameter(name, string.Empty, ParameterScope.Parameter);
                                    AddParameter(param);
                                }
                            }
                            else
                            {
                                offset += 2; // + Leerzeile
                                if (item.Item2 - offset - 1 >= 0)
                                {
                                    var bytes = new byte[item.Item2 - offset - 2];
                                    Buffer.BlockCopy(Content, item.Item1 + offset, bytes, 0, item.Item2 - offset - 2);

                                    var param = new ParameterFile(name, filename, ParameterScope.Parameter) { ContentType = contenttype, Data = bytes };
                                    AddParameter(param);
                                }
                                else
                                {
                                    var param = new Parameter(name, filename, ParameterScope.Parameter);
                                    AddParameter(param);
                                }
                            }
                        }

                        break;
                    }
                case TypeEnctype.Text:
                    {
                        var lines = new List<string>();
                        var offset = 0;

                        for (var i = 0; i < Content.Length; i++)
                        {
                            if (Content[i] == '\r')
                            {
                                lines.Add(Encoding.UTF8.GetString(Content, offset, i - offset));
                            }
                            else if (Content[i] == '\n')
                            {
                                offset = i + 1;
                            }
                        }

                        // wenn noch nicht alle Bytes gelesen wurden
                        if (offset < Content.Length)
                        {
                            lines.Add(Encoding.UTF8.GetString(Content, offset, Content.Length - offset));
                        }

                        var last = null as Parameter;

                        foreach (var v in lines)
                        {
                            var match = Regex.Match(v, @"([\w-]*)=(.*)", RegexOptions.Compiled);
                            if (match.Groups[1].Success && match.Groups[2].Success)
                            {
                                last = new Parameter(match.Groups[1].ToString().Trim(), match.Groups[2].ToString().Trim(), ParameterScope.Parameter);
                                AddParameter(last);
                            }
                            else if (last != null)
                            {
                                last.Value += "\r\n" + v;

                            }
                        }

                        if (last != null)
                        {
                            last.Value = last.Value.TrimEnd();
                        }

                        break;
                    }
                case TypeEnctype.UrLEncoded:
                    {
                        var str = Encoding.UTF8.GetString(Content, 0, Content.Length);
                        var param = str.Replace('+', ' ');

                        foreach (var v in param.Split('&'))
                        {
                            var s = v.Split('=');
                            AddParameter(new Parameter
                            (
                                s[0],
                                s.Length > 1 ? s[1]?.TrimEnd() : string.Empty,
                                ParameterScope.Parameter
                            ));
                        }

                        break;
                    }
                default:
                    {

                        break;
                    }
            }
        }

        /// <summary>
        /// Ermittelt den die Parameter aus der Session
        /// </summary>
        private void ParseSessionParams()
        {
            Session = SessionManager.GetSession(this);

            var property = Session.GetProperty<SessionPropertyParameter>();
            if (property != null && property.Params != null)
            {
                foreach (var param in property.Params)
                {
                    AddParameter(new Parameter(param.Key?.ToLower(), param.Value.Value, ParameterScope.Session));
                }
            }
        }

        /// <summary>
        /// Fügt mehrere Parameter hinzu
        /// </summary>
        /// <param name="param">Die Parameter</param>
        public void AddParameter(IEnumerable<Parameter> param)
        {
            foreach (var p in param)
            {
                AddParameter(p);
            }
        }

        /// <summary>
        /// Fügt ein Parameter hinzu
        /// </summary>
        /// <param name="param">Der Parameter</param>
        public void AddParameter(Parameter param)
        {
            if (!Param.ContainsKey(param.Key.ToLower()))
            {
                Param.Add(param.Key.ToLower(), param);
            }
            else
            {
                Param[param.Key.ToLower()] = param;
            }
        }

        /// <summary>
        /// Liefert ein Parameter anhand seines Namens
        /// </summary>
        /// <param name="name">Der Name des Parameters</param>
        /// <returns>Der Wert</returns>
        public Parameter GetParameter(string name)
        {
            if (HasParameter(name))
            {
                return Param[name.ToLower()];
            }

            return null;
        }

        /// <summary>
        /// Prüft, ob ein Parameter vorhanden ist
        /// </summary>
        /// <param name="name">Der Name des Parameters</param>
        /// <returns>true wenn Parameter vorhanden ist, false sonst</returns>
        public bool HasParameter(string name)
        {
            return Param.ContainsKey(name.ToLower());
        }
    }
}
