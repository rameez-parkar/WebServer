using System;
using System.Net;

namespace WebServer.App
{
    public class HttpRequestListener
    {
        private HttpListener _listener;
        private ServerConfiguration _config;
        private HttpRequestParser _parser;
        private Dispatcher _dispatcher;
        private HttpListenerContext _context;

        public HttpRequestListener()
        {
            _listener = new HttpListener();
            _config = new ServerConfiguration();
            foreach(string prefix in _config.Mapper.Keys)
            {
                _listener.Prefixes.Add(prefix);
            }
        }
        public void Start()
        {
            _listener.Start();
            while (true)
            {
                Console.WriteLine("Waiting for request...");
                ParseRequest();
                DispatchResponse();
            }
        }
        public void ParseRequest()
        {
            _context = _listener.GetContext();
            _parser = new HttpRequestParser(_listener, _config, _context);
            _parser.ParseRequest();
        }
        public void DispatchResponse()
        {
            _dispatcher = new Dispatcher(_context, _parser);
            _dispatcher.BuildHttpResponse();
            _dispatcher.SendHttpResponse();
        }
    }
}
