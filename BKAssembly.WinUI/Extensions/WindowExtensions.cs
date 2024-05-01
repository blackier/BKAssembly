using System;
using Microsoft.UI;
using Microsoft.UI.Input;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using WinRT.Interop;

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

    public static double DpiScale(this Window window)
    {
        IntPtr hWnd = WindowNative.GetWindowHandle(window);
        WindowId wndId = Win32Interop.GetWindowIdFromWindow(hWnd);
        DisplayArea displayArea = DisplayArea.GetFromWindowId(wndId, DisplayAreaFallback.Primary);
        IntPtr hMonitor = Win32Interop.GetMonitorFromDisplayId(displayArea.DisplayId);

        // Get DPI.
        int result = BKWindowsAPI.GetDpiForMonitor(hMonitor, BKWindowsAPI.Monitor_DPI_Type.MDT_Default, out uint dpiX, out uint _);
        if (result != 0)
        {
            return 1.0;
        }

        uint scaleFactorPercent = (uint)(((long)dpiX * 100 + (96 >> 1)) / 96);
        return scaleFactorPercent / 100.0;
    }

    public static void SetClickThroughRegions(this Window window, Windows.Graphics.RectInt32[] rects)
    {
        var nonClientInputSrc = InputNonClientPointerSource.GetForWindowId(window.AppWindow.Id);
        nonClientInputSrc.SetRegionRects(NonClientRegionKind.Passthrough, rects);
    }

    public static void ClearClickThroughRegions(this Window window)
    {
        var nonClientInputSrc = InputNonClientPointerSource.GetForWindowId(window.AppWindow.Id);
        nonClientInputSrc.ClearRegionRects(NonClientRegionKind.Passthrough);
    }

    public static void ApplyTheme(this Window window, ElementTheme theme)
    {
        if (window.Content is FrameworkElement rootElement)
        {
            rootElement.RequestedTheme = theme;
        }
    }

    public static ElementTheme RequestedTheme(this Window window)
    {
        if (window.Content is FrameworkElement rootElement)
            return rootElement.RequestedTheme;

        return ElementTheme.Default;
    }
}
