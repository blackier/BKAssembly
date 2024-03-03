using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.Web.Http;
using Windows.Web.Http.Filters;

namespace BKAssembly.WinUI;

public class BKDownloader
{
    private static HttpClient _httpClient;
    public static IAsyncOperationWithProgress<HttpResponseMessage, HttpProgress> DownloadFile(
        Uri uri,
        Action<IAsyncOperationWithProgress<HttpResponseMessage, HttpProgress>, HttpProgress> progress,
        Action<IAsyncOperationWithProgress<HttpResponseMessage, HttpProgress>, AsyncStatus> completed)
    {
        // https://www.cnblogs.com/webabcd/p/3213590.html
        // https://learn.microsoft.com/zh-cn/windows/uwp/networking/httpclient
        if (_httpClient == null)
        {
            var filter = new HttpBaseProtocolFilter();
            filter.CacheControl.ReadBehavior = HttpCacheReadBehavior.MostRecent;
            _httpClient = new HttpClient(filter);
        }

        HttpRequestMessage message = new() { RequestUri = uri };

        var result = _httpClient.SendRequestAsync(message);
        result.Progress = (asyncInfo, progressInfo) =>
        {
            progress(asyncInfo, progressInfo);
        };

        result.Completed = (asyncInfo, asyncStatus) =>
        {
            completed(asyncInfo, asyncStatus);
        };
        return result;
    }
}
