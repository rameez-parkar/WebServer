using System.Collections.Generic;
using System.Net;

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

        public HttpRequestParser(HttpListener listener, ServerConfiguration config, HttpListenerContext context)
        {
            _listener = listener;
            _context = context;
            _config = config;
        }

        public void ParseRequest()
        {
            BaseUrl = _context.Request.Url.Scheme + "://" + _context.Request.Url.Authority + "/";
            Filename = _context.Request.Url.LocalPath;
            RootDirectory = _config.Mapper.GetValueOrDefault(BaseUrl);
        }
    }
}
