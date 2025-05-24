using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
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
    public static IAsyncOperationWithProgress<HttpResponseMessage, HttpProgress> DownloadFile(
        Uri uri,
        Action<IAsyncOperationWithProgress<HttpResponseMessage, HttpProgress>, HttpProgress> progress,
        Action<IAsyncOperationWithProgress<HttpResponseMessage, HttpProgress>, AsyncStatus> completed
    )
    {
        // https://www.cnblogs.com/webabcd/p/3213590.html
        // https://learn.microsoft.com/zh-cn/windows/uwp/networking/httpclient
        HttpClient httpClient = new();
        //var filter = new HttpBaseProtocolFilter();
        //filter.CacheControl.ReadBehavior = HttpCacheReadBehavior.MostRecent;
        httpClient =
            new HttpClient( /*filter*/
            );
        httpClient.DefaultRequestHeaders.Clear();
        httpClient.DefaultRequestHeaders.Accept.Clear();
        httpClient.DefaultRequestHeaders.Accept.TryParseAdd(
            "image/avif,image/webp,image/apng,image/svg+xml,image/*,*/*;q=0.8"
        );
        httpClient.DefaultRequestHeaders.AcceptEncoding.Clear();
        httpClient.DefaultRequestHeaders.AcceptEncoding.TryParseAdd("gzip, deflate, br");
        httpClient.DefaultRequestHeaders.UserAgent.Clear();
        httpClient.DefaultRequestHeaders.UserAgent.ParseAdd(
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.6261.95 Safari/537.36"
            );

        HttpRequestMessage message = new() { RequestUri = uri };

        var result = httpClient.SendRequestAsync(message);
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
