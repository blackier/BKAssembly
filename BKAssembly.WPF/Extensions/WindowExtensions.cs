using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;

namespace BKAssembly.WPF.Extensions;

public static class WindowExtensions
{
    public static double ScreenScaling()
    {
        Matrix m = PresentationSource.FromVisual(Application.Current.MainWindow).CompositionTarget.TransformToDevice;
        //double dx = m.M11;
        //double dy = m.M22;
        return m.M11;
    }

    public static IntPtr Handle(this Window value)
    {
        WindowInteropHelper wih = new(value);
        return wih.EnsureHandle();
    }
}
