using System;
using System.IO;
using System.Net;
using System.Text;

namespace WebServer.App
{
    public class Dispatcher
    {
        private StaticFileSystem _file;
        private HttpListenerContext _context;
        private HttpListenerResponse _response;
        private HttpRequestParser _parser;
        byte[] buffer;
        public Dispatcher(HttpListenerContext context, HttpRequestParser parser)
        {
            _context = context;
            _parser = parser;
            _file = new StaticFileSystem(_parser);
            _response = _context.Response;
        }

        public void BuildHttpResponse()
        {
            string message = _file.ReadFile();
            buffer = Encoding.UTF8.GetBytes(message);
            _response.ContentLength64 = buffer.Length;
        }
        public void SendHttpResponse()
        {
            Stream st = _response.OutputStream;
            st.Write(buffer, 0, buffer.Length);
            if (_file.isFileFound)
            {
                _context.Response.StatusCode = (int)HttpStatusCode.OK;
            }
            else
            {
                _context.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            Console.WriteLine("Request served...");
            _context.Response.Close();
        }
    }
}
