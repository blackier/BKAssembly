using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BKAssembly.WPF.Extensions;

public static class UIElementExtensions
{
    public static T FindElementFromPoint<T>(this UIElement reference, Point point)
        where T : DependencyObject
    {
        DependencyObject element = reference.InputHitTest(point) as DependencyObject;
        if (element == null)
            return null;
        else if (element is T)
            return (T)element;
        else
            return element.FindParent<T>();
    }

    public static bool IsMouseOver(this UIElement element, IntPtr lParam)
    {
        if (lParam == IntPtr.Zero)
            return false;

        var mousePosScreen = new Point(lParam.Get_X_Param(), lParam.Get_Y_Param());
        var bounds = new Rect(new Point(), element.RenderSize);
        var mousePosRelative = element.PointFromScreen(mousePosScreen);
        return bounds.Contains(mousePosRelative);
    }
}
