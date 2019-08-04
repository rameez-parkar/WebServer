using System.Threading;

namespace WebServer.App
{
    public class WebServer
    {
        private HttpRequestListener _requestListener;
        public WebServer()
        {
            _requestListener = new HttpRequestListener();
        }
        public void Start()
        {
            Thread listenerThread = new Thread(new ThreadStart(() => _requestListener.Start()));
            listenerThread.Start();
        }
    }
}
