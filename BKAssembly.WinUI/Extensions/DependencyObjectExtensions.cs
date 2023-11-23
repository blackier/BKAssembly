using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKAssembly.WinUI.Extensions;

public static class DependencyObjectExtensions
{
    public static IEnumerable<T> FindVisualChildren<T>(this DependencyObject depObj) where T : DependencyObject
    {
        if (depObj != null)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                if (child != null && child is T)
                {
                    yield return (T)child;
                }

                foreach (T childOfChild in FindVisualChildren<T>(child))
                {
                    yield return childOfChild;
                }
            }
        }
    }

    public static T FindVisualChildren<T>(this DependencyObject depObj, string childrenName) where T : FrameworkElement
    {
        if (depObj != null)
        {
            foreach (T child in FindVisualChildren<T>(depObj))
            {
                if ((child as FrameworkElement).Name == childrenName)
                    return child;
            }
        }
        return null;
    }

    public static T FindParent<T>(this DependencyObject dependencyObject) where T : DependencyObject
    {
        var parentObject = VisualTreeHelper.GetParent(dependencyObject);

        if (parentObject == null) return null;

        var parent = parentObject as T;
        return parent ?? FindParent<T>(parentObject);
    }
}
