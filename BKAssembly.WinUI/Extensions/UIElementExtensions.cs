using Microsoft.UI.Input;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BKAssembly.WinUI.Extensions;

public static class UIElementExtensions
{
    public static double RasterizationScale(this UIElement element)
    {
        if (element.XamlRoot != null)
        {
            return element.XamlRoot.RasterizationScale;
        }
        return 1.0;
    }

    public static void ChangeCursor(this UIElement element, InputSystemCursorShape cursorShape)
    {
        var cursor = InputSystemCursor.Create(cursorShape);
        typeof(UIElement).InvokeMember(
            "ProtectedCursor",
            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.SetProperty | BindingFlags.Instance,
            null,
            element,
            new object[] { cursor });
    }
}
