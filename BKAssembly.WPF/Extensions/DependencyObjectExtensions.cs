using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace BKAssembly.WPF.Extensions;

public static class DependencyObjectExtensions
{
    public static void UpdateBindingSources(this DependencyObject obj, params DependencyProperty[] properties)
    {
        foreach (DependencyProperty depProperty in properties)
        {
            //check whether the submitted object provides a bound property
            //that matches the property parameters
            BindingExpression be = BindingOperations.GetBindingExpression(obj, depProperty);
            if (be != null)
                be.UpdateSource();
        }

        int count = VisualTreeHelper.GetChildrenCount(obj);
        for (int i = 0; i < count; i++)
        {
            //process child items recursively
            DependencyObject childObject = VisualTreeHelper.GetChild(obj, i);
            UpdateBindingSources(childObject, properties);
        }
    }

    public static DependencyObject GetParentObject(this DependencyObject child)
    {
        if (child == null)
            return null;
        ContentElement contentElement = child as ContentElement;

        if (contentElement != null)
        {
            DependencyObject parent = ContentOperations.GetParent(contentElement);
            if (parent != null)
                return parent;

            FrameworkContentElement fce = contentElement as FrameworkContentElement;
            return fce != null ? fce.Parent : null;
        }

        //if it's not a ContentElement, rely on VisualTreeHelper
        return VisualTreeHelper.GetParent(child);
    }

    public static T FindParent<T>(this DependencyObject child)
        where T : DependencyObject
    {
        //get parent item
        DependencyObject parentObject = GetParentObject(child);

        //we've reached the end of the tree
        if (parentObject == null)
            return null;

        //check if the parent matches the type we're looking for
        T parent = parentObject as T;
        if (parent != null)
        {
            return parent;
        }
        else
        {
            //use recursion to proceed with next level
            return FindParent<T>(parentObject);
        }
    }

    public static T FindChild<T>(this DependencyObject parent, string childName)
        where T : DependencyObject
    {
        // Confirm parent and childName are valid.
        if (parent == null)
            return null;

        T foundChild = null;

        int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
        for (int i = 0; i < childrenCount; i++)
        {
            var child = VisualTreeHelper.GetChild(parent, i);
            // If the child is not of the request child type child
            T childType = child as T;
            if (childType == null)
            {
                // recursively drill down the tree
                foundChild = FindChild<T>(child, childName);

                // If the child is found, break so we do not overwrite the found child.
                if (foundChild != null)
                    break;
            }
            else if (!string.IsNullOrEmpty(childName))
            {
                var frameworkElement = child as FrameworkElement;
                // If the child's name is set for search
                if (frameworkElement != null && frameworkElement.Name == childName)
                {
                    // if the child's name is of the request name
                    foundChild = (T)child;
                    break;
                }
            }
            else
            {
                // child element found.
                foundChild = (T)child;
                break;
            }
        }

        return foundChild;
    }

    public static T FindChild<T>(this DependencyObject parent)
        where T : DependencyObject
    {
        // Confirm parent and childName are valid.
        if (parent == null)
            return null;

        T foundChild = null;

        int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
        for (int i = 0; i < childrenCount; i++)
        {
            var child = VisualTreeHelper.GetChild(parent, i);
            // If the child is not of the request child type child
            T childType = child as T;
            if (childType == null)
            {
                // recursively drill down the tree
                foundChild = FindChild<T>(child);

                // If the child is found, break so we do not overwrite the found child.
                if (foundChild != null)
                    break;
            }
            else
            {
                // child element found.
                foundChild = (T)child;
                break;
            }
        }

        return foundChild;
    }
}
