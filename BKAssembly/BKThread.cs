﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BKAssembly;

public class BKThread : IDisposable
{
    private Thread _thread;
    private AutoResetEvent _waitEvent;
    private ConcurrentQueue<Action> _taskQueue;
    private bool _disposed = false;

    public BKThread()
    {
        _thread = new Thread(ThreadProc);
        _waitEvent = new AutoResetEvent(false);
        _taskQueue = new ConcurrentQueue<Action>();
        _thread.Start();
    }

    private void ThreadProc()
    {
        while (!_disposed)
        {
            while (!_disposed && _taskQueue.TryDequeue(out Action? task))
                task();
            _waitEvent.WaitOne();
        }
    }

    public void AddTask(Action task)
    {
        _taskQueue.Enqueue(task);
        _waitEvent.Set();
    }

    public void ClearTask()
    {
        _taskQueue.Clear();
    }

    public void Dispose()
    {
        // https://learn.microsoft.com/zh-cn/dotnet/api/system.idisposable?view=net-8.0#definition
        if (_disposed)
            return;

        _disposed = true;
        _waitEvent.Set();
        GC.SuppressFinalize(this);
    }
}
