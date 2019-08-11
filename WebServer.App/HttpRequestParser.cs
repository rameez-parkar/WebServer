using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web;

namespace WebServer.App
{
    public class HttpRequestParser
    {
        private HttpListener _listener;
        private ServerConfiguration _config;
        private HttpListenerContext _context;
        public string BaseUrl { get; set; }
        public string Filename { get; set; }
        public string RootDirectory { get; set; }

        public string Data { get; set; }
        public string Type { get; set; }

        public HttpRequestParser(HttpListener listener, ServerConfiguration config, HttpListenerContext context)
        {
            _listener = listener;
            _context = context;
            _config = config;
        }

        public (string, string) RequestData()
        {
            var data = new StreamReader(_context.Request.InputStream, _context.Request.ContentEncoding).ReadToEnd();
            var requestType = _context.Request.HttpMethod;
            return (HttpUtility.UrlDecode(data), requestType);
        }

        public void ParseRequest()
        {
            BaseUrl = _context.Request.Url.Scheme + "://" + _context.Request.Url.Authority + "/";
            Data = RequestData().Item1;
            Type = RequestData().Item2;
            if (Type.ToUpper() == "GET")
            {
                Filename = _context.Request.Url.LocalPath;
                RootDirectory = _config.Mapper.GetValueOrDefault(BaseUrl);
            }
            else if(Type.ToUpper() == "POST")
            {

            }
        }
    }
}
