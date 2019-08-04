using System.IO;

namespace WebServer.App
{
    public class StaticFileSystem
    {
        private HttpRequestParser _parser;
        public bool isFileFound = false;

        public StaticFileSystem(HttpRequestParser parser)
        {
            _parser = parser;
        }

        public string ReadFile()
        {
            TextReader tr;
            string message;
            string _filePath = _parser.RootDirectory + _parser.Filename;
            if (File.Exists(_filePath))
            {
                isFileFound = true;
                tr = new StreamReader(_filePath);
                message = tr.ReadToEnd();
            }
            else
            {
                isFileFound = false;
                _filePath = @"C:\Users\rparkar\Desktop\Assignments\WebServer\WebApps\Error\404.html";
                tr = new StreamReader(_filePath);
                message = tr.ReadToEnd();
            }
            return message;
        }
    }
}
