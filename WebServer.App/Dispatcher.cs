using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json.Linq;

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
            _response = _context.Response;
        }

        public void BuildHttpResponse()
        {
            string message = "";
            if (_parser.Type.ToUpper() == "POST")
            {
                try
                {
                    string leapYear = "";
                    JObject jsonObj = JObject.Parse(_parser.Data);
                    Dictionary<string, string> dictObj = jsonObj.ToObject<Dictionary<string, string>>();
                    foreach (var item in dictObj)
                    {
                        int year = int.Parse(item.Value);
                        if(year % 400 != 0)
                        {
                            if((year % 4 ==0 ) && (year % 100 != 0))
                            {
                                leapYear += $"{year} is a leap year.\n";
                            }
                            else
                            {
                                leapYear += $"{year} is not a leap year.\n";
                            }
                        }
                        else
                        {
                            leapYear += $"{year} is a leap year.\n";
                        }
                    }
                    message = leapYear;
                }
                catch
                {

                }
            }
            else
            {
                _file = new StaticFileSystem(_parser);
                message = _file.ReadFile();
            }
            buffer = Encoding.UTF8.GetBytes(message);
            _response.ContentLength64 = buffer.Length;
        }
        public void SendHttpResponse()
        {
            Stream st = _response.OutputStream;
            st.Write(buffer, 0, buffer.Length);
            string type = _parser.RequestData().Item2;
            if (type.ToUpper() == "GET")
            {
                if (_file.isFileFound)
                {
                    _context.Response.StatusCode = (int)HttpStatusCode.OK;
                }
                else
                {
                    _context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                }
            }
            else
            {
                _context.Response.StatusCode = (int)HttpStatusCode.OK;
            }
            Console.WriteLine("Request served...");
            _context.Response.Close();
        }
    }
}
