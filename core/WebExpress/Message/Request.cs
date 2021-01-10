using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using WebExpress.Html;

namespace WebExpress.Message
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
        /// Liefert den Inhalt
        /// </summary>
        public byte[] Content { get; private set; }

        /// <summary>
        /// Konstruktor
        /// </summary>
        protected Request(RequestMethod type, string url, string version, RequestHeaderFields options, string client)
        {
            Method = type;
            URL = url;
            Param = new Dictionary<string, Parameter>();
            Version = version;
            HeaderFields = options;
            Client = client;
        }

        /// <summary>
        /// Extrahiert aus dem Stream die Http-Anfrage
        /// </summary>
        /// <param name="reader">Der Reader</param>
        /// <param name="client">Die Client-IP</param>
        /// <returns>Die Anfrage</returns>
        public static Request Create(BinaryReader reader, string client)
        {
            var capacity = 1024;
            var header = new List<string>();
            var buffer = new byte[capacity];
            var line = new byte[capacity];
            var counter = 0;
            var readCount = 0;

            // Lese Header
            do
            {
                readCount = reader.Read(buffer, 0, capacity);

                for (var i = 0; i < readCount; i++)
                {
                    if (buffer[i] == '\r')
                    {
                        header.Add(Encoding.UTF8.GetString(line, 0, counter));
                    }
                    else if (buffer[i] == '\n' && counter > 0)
                    {
                        counter = 0;
                    }
                    else if (buffer[i] == '\n' && counter == 0)
                    {
                        // Ende des Headers
                        var request = Request.Parse(header, client);

                        // ContentLength ermitteln
                        var contentLength = request.HeaderFields.ContentLength;
                        if (contentLength > 0)
                        {
                            var content = new byte[contentLength /*- 2*/]; // Leerzeile des Contentbereiches '\r\n' wird nicht mitgezählt
                            var offset = readCount - i;

                            //var b = Encoding.UTF8.GetString(buffer, 0, 1024);

                            // Lese bereits gelesenen Content
                            Buffer.BlockCopy(buffer, i + 1, content, 0, offset - 1);

                            if (i + contentLength > buffer.Length)
                            {
                                // Lese neuen Content
                                do
                                {
                                    offset += reader.Read(content, offset - 1, content.Length - offset + 1);
                                }
                                while (offset < content.Length);
                            }

                            ParseRequestParams(request, content);

                            // Leerzeile lesen
                            //readCount = reader.Read(buffer, 0, capacity);
                        }

                        return request;
                    }
                    else
                    {
                        line[counter] = buffer[i];
                        counter++;
                    }
                }
            }
            while (!(readCount < capacity));

            // Ohne Content
            return Request.Parse(header, client);
        }

        /// <summary>
        /// Parst den Request und erzeugt ein Request-Objekt
        /// </summary>
        /// <param name="request">Der Request in Stringform</param>
        /// <returns>Der Request als Objekt</returns>
        private static Request Parse(ICollection<string> request, string client)
        {
            if (request == null || request.Count == 0)
            {
                return null;
            }

            var type = RequestMethod.NONE;
            var url = string.Empty;
            var version = string.Empty;
            var urlMatch = null as Match;
            var methode = request.Take(1).FirstOrDefault();
            var options = request.Skip(1).TakeWhile(x => !string.IsNullOrWhiteSpace(x)).ToList();
            var len = methode.Length;

            var match = Regex.Match(methode, @"(GET|POST|PUT|DELETE) (.*) (.*)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

            if (match.Groups[1].Success)
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

            if (match.Groups[2].Success)
            {
                url = match.Groups[2].Value;
                urlMatch = Regex.Match(url, @"^(.*)\?(.*)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);

                if (urlMatch.Groups[1].Success)
                {
                    url = urlMatch.Groups[1].Value;
                }
            }

            if (match.Groups[3].Success)
            {
                version = match.Groups[3].Value;
            }

            var req = new Request(type, url, version, RequestHeaderFields.Parse(options), client);

            if (urlMatch.Groups[2].Success && urlMatch.Groups[2].Length > 0)
            {
                foreach (var v in urlMatch.Groups[2].Value.Split('&'))
                {
                    var regex = new Regex("[=]{1}");
                    var kv = regex.Split(v, 2);

                    req.AddParam(kv[0], kv.Count() > 1 ? kv[1] : "");
                }
            }

            return req;
        }

        /// <summary>
        /// Ermittelt den Content einer Anfrage
        /// </summary>
        /// <param name="request">Die Anfrage</param>
        /// <param name="content">Die Contentdaten</param>
        private static void ParseRequestParams(Request request, byte[] content)
        {
            var contentType = request.HeaderFields.ContentType?.Split(';');
            request.Content = content;

            switch (TypeEnctypeExtensions.Convert(contentType.FirstOrDefault()))
            {
                case TypeEnctype.None:
                    {
                        var boundary = contentType.Skip(1)?.FirstOrDefault();
                        var boundaryValue = "--" + boundary?.Split('=').Skip(1)?.FirstOrDefault();
                        var offset = 0;
                        int pos = 0;
                        var dispositions = new List<Tuple<int, int>>(); // Item1=Position, Item2=Länge

                        // ermittle Dispositionen
                        for (var i = 0; i < content.Length; i++)
                        {
                            if (content[i] == '\r')
                            {
                                var c = Encoding.UTF8.GetString(content, offset, boundaryValue.Length).Trim();
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
                            else if (content[i] == '\n')
                            {
                                offset = i + 1;
                            }
                            else if (i == content.Length - 1)
                            {
                                // Am Ende
                                var c = Encoding.UTF8.GetString(content, offset, boundaryValue.Length).Trim();
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

                            var str = Encoding.UTF8.GetString(content, item.Item1, item.Item2 > 256 ? 256 : item.Item2);
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
                                    var value = Encoding.UTF8.GetString(content, item.Item1 + offset, item.Item2 - offset - 2);

                                    var param = new Parameter(name, value.TrimEnd()) { Scope = ParameterScope.Local };
                                    request.AddParam(param);
                                }
                                else
                                {
                                    var param = new Parameter(name, string.Empty) { Scope = ParameterScope.Local };
                                    request.AddParam(param);
                                }
                            }
                            else
                            {
                                offset += 2; // + Leerzeile
                                if (item.Item2 - offset - 1 >= 0)
                                {
                                    var bytes = new byte[item.Item2 - offset - 2];
                                    Buffer.BlockCopy(content, item.Item1 + offset, bytes, 0, item.Item2 - offset - 2);

                                    var param = new ParameterFile(name, filename) { Scope = ParameterScope.Local, ContentType = contenttype, Data = bytes };
                                    request.AddParam(param);
                                }
                                else
                                {
                                    var param = new Parameter(name, filename) { Scope = ParameterScope.Local };
                                    request.AddParam(param);
                                }
                            }
                        }

                        break;
                    }
                case TypeEnctype.Text:
                    {
                        var lines = new List<string>();
                        var offset = 0;

                        for (var i = 0; i < content.Length; i++)
                        {
                            if (content[i] == '\r')
                            {
                                lines.Add(Encoding.UTF8.GetString(content, offset, i - offset));
                            }
                            else if (content[i] == '\n')
                            {
                                offset = i + 1;
                            }
                        }

                        // wenn noch nicht alle Bytes gelesen wurden
                        if (offset < content.Length)
                        {
                            lines.Add(Encoding.UTF8.GetString(content, offset, content.Length - offset));
                        }

                        var last = null as Parameter;

                        foreach (var v in lines)
                        {
                            var match = Regex.Match(v, @"([\w-]*)=(.*)", RegexOptions.Compiled);
                            if (match.Groups[1].Success && match.Groups[2].Success)
                            {
                                last = new Parameter(match.Groups[1].ToString().Trim(), match.Groups[2].ToString().Trim()) { Scope = ParameterScope.Local };
                                request.AddParam(last);
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
                        var str = Encoding.UTF8.GetString(content, 0, content.Length);
                        var param = str.Replace('+', ' ');

                        foreach (var v in param.Split('&'))
                        {
                            var s = v.Split('=');
                            request.AddParam(s[0], s.Count() > 1 ? s[1]?.TrimEnd() : string.Empty);
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
        /// Fügt ein Parameter hinzu
        /// </summary>
        /// <param name="param">Der Parameter</param>
        public void AddParam(Parameter param)
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
        public Parameter GetParam(string name)
        {
            if (HasParam(name))
            {
                return Param[name.ToLower()];
            }

            return null;
        }

        /// <summary>
        /// Liefert ein Parameter anhand seines Namens
        /// </summary>
        /// <param name="name">Der Name des Parameters</param>
        /// <returns>Der Wert</returns>
        public string GetParamValue(string name)
        {
            return GetParam(name)?.Value;
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
