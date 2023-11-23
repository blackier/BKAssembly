using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BKAssembly;

public class BKHttpClient
{
    private static Lazy<HttpClient> _defaultHttpClient = new(CreateHttpClient());
    public static HttpClient DefaultHttpClient => _defaultHttpClient.Value;

    public static HttpClient CreateHttpClient(HttpClientHandler? handler = null, TimeSpan timeout = new())
    {
        HttpClient newHttpClient;
        if (handler != null)
            newHttpClient = new(handler);
        else
        {
            SocketsHttpHandler socket_handler = new SocketsHttpHandler()
            {
                EnableMultipleHttp2Connections = true
            };

            newHttpClient = new(socket_handler)
            {
                DefaultRequestVersion = HttpVersion.Version20
            };

            if (timeout != TimeSpan.Zero)
                newHttpClient.Timeout = timeout;
            else
                newHttpClient.Timeout = new(0, 0, 30);
        }
        return newHttpClient;
    }

}
