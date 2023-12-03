using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKAssembly.WPF.Extensions;

public static class PointExtensions
{
    public static System.Drawing.Point ToDrawingPoint(this System.Windows.Point point)
    {
        return new System.Drawing.Point((int)point.X, (int)point.Y);
    }

    public static System.Windows.Point ToWindowsPoint(this System.Drawing.Point point)
    {
        return new System.Windows.Point(point.X, point.Y);
    }
}
