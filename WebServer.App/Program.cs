namespace WebServer.App
{
    class Program
    {
        static void Main(string[] args)
        {
            WebServer webserver = new WebServer();
            webserver.Start();
        }
    }
}
