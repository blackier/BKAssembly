using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Dispatching;
using Microsoft.UI.Xaml;

namespace BKAssembly.WinUI.Extensions;

public static class TaskExtensions
{
    public static void ContinueInUI(this Task _, DispatcherQueueHandler callback)
    {
        WindowExtensions.MainWindow?.DispatcherQueue?.TryEnqueue(DispatcherQueuePriority.Normal, callback);
    }
}
