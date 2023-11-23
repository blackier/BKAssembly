using System;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;

namespace BKAssembly.WinUI.Extensions;

public static class WindowExtensions
{
    public static Window MainWindow { get; set; }
    public static IntPtr Handle(this Window window)
    {
        return WinRT.Interop.WindowNative.GetWindowHandle(window);
    }

    private static AppWindow GetAppWindowFromWindowHandle(IntPtr windowHandle)
    {
        WindowId windowId = Win32Interop.GetWindowIdFromWindow(windowHandle);
        return AppWindow.GetFromWindowId(windowId);
    }

    public static void Show(this Window window)
    {
        window.AppWindow.Show();
    }

    public static void Hide(this Window window)
    {
        window.AppWindow.Hide();
    }
}
