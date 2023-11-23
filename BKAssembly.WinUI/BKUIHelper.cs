using Microsoft.UI;
using Microsoft.UI.Input;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinRT.Interop;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BKAssembly.WinUI;

public class BKUIHelper
{
    public static double GetScaleAdjustment(Window window)
    {
        IntPtr hWnd = WindowNative.GetWindowHandle(window);
        WindowId wndId = Win32Interop.GetWindowIdFromWindow(hWnd);
        DisplayArea displayArea = DisplayArea.GetFromWindowId(wndId, DisplayAreaFallback.Primary);
        IntPtr hMonitor = Win32Interop.GetMonitorFromDisplayId(displayArea.DisplayId);

        // Get DPI.
        int result = BKWindowsAPI.GetDpiForMonitor(hMonitor, BKWindowsAPI.Monitor_DPI_Type.MDT_Default, out uint dpiX, out uint _);
        if (result != 0)
        {
            throw new Exception("Could not get DPI for monitor.");
        }

        uint scaleFactorPercent = (uint)(((long)dpiX * 100 + (96 >> 1)) / 96);
        return scaleFactorPercent / 100.0;
    }

    // get dpi for an element
    public static double GetRasterizationScaleForElement(UIElement element)
    {
        if (element.XamlRoot != null)
        {
            return element.XamlRoot.RasterizationScale;
        }
        return 1.0;
    }

    public static void SetClickThroughRegions(Windows.Graphics.RectInt32[] rects, AppWindow appWindow)
    {
        var nonClientInputSrc = InputNonClientPointerSource.GetForWindowId(appWindow.Id);
        nonClientInputSrc.SetRegionRects(NonClientRegionKind.Passthrough, rects);
    }

    public static void ClearClickThroughRegions(AppWindow appWindow)
    {
        var nonClientInputSrc = InputNonClientPointerSource.GetForWindowId(appWindow.Id);
        nonClientInputSrc.ClearRegionRects(NonClientRegionKind.Passthrough);
    }
}
