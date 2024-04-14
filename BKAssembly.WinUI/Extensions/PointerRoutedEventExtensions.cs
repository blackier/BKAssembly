using Microsoft.UI.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKAssembly.WinUI.Extensions;

public static class PointerRoutedEventExtensions
{
    public static bool IsLeftClick(this UIElement sender, PointerRoutedEventArgs e)
    {
        if (e.Pointer.PointerDeviceType == PointerDeviceType.Mouse)
        {
            PointerPoint ptrPt = e.GetCurrentPoint(sender);
            if (ptrPt.Properties.IsLeftButtonPressed)
            {
                return true;
            }
        }
        return false;
    }

    public static bool IsRightClick(this UIElement sender, PointerRoutedEventArgs e)
    {
        if (e.Pointer.PointerDeviceType == PointerDeviceType.Mouse)
        {
            PointerPoint ptrPt = e.GetCurrentPoint(sender);
            if (ptrPt.Properties.IsRightButtonPressed)
            {
                return true;
            }
        }
        return false;
    }

    public static bool IsForwardClick(this UIElement sender, PointerRoutedEventArgs e)
    {
        if (e.Pointer.PointerDeviceType == PointerDeviceType.Mouse)
        {
            PointerPoint ptrPt = e.GetCurrentPoint(sender);
            if (ptrPt.Properties.IsXButton2Pressed)
            {
                return true;
            }
        }
        return false;
    }

    public static bool IsBackClick(this UIElement sender, PointerRoutedEventArgs e)
    {
        if (e.Pointer.PointerDeviceType == PointerDeviceType.Mouse)
        {
            PointerPoint ptrPt = e.GetCurrentPoint(sender);
            if (ptrPt.Properties.IsXButton1Pressed)
            {
                return true;
            }
        }
        return false;
    }
}
