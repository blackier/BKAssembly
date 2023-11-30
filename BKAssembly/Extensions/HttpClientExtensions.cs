using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKAssembly.Extensions;

public static class HttpClientExtensions
{
    public static bool TrySend<T>(this HttpClient httpClient, HttpRequestMessage request, out HttpResponseMessage response, out T? result)
    {
        try
        {
            response = httpClient.Send(request);
        }
        catch
        {
            response = null;
            result = default;
            return false;
        }
        try
        {
            result = BKMisc.JsonDeserialize<T>(response.Content.ReadAsStringAsync().Result);
            return true;
        }
        catch
        {
            result = default;
            return false;
        }
    }
}
