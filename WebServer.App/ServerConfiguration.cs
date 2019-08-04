using System.Collections.Generic;

namespace WebServer.App
{
    public class ServerConfiguration
    {
        public Dictionary<string, string> Mapper = new Dictionary<string, string>()
        {
            {"http://localhost:1111/", @"C:\Users\rparkar\Desktop\Assignments\WebServer\WebApps\Localhost\" },
            {"http://127.0.0.1:1111/", @"C:\Users\rparkar\Desktop\Assignments\WebServer\WebApps\Localhost\"  }
        };
    }
}
