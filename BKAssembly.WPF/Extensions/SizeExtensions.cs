using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKAssembly.WPF.Extensions;

public static class SizeExtensions
{
    public static System.Drawing.Size ToDrawingSize(this System.Windows.Size size)
    {
        return new System.Drawing.Size((int)size.Width, (int)size.Height);
    }

    public static System.Windows.Size ToWindowsSize(this System.Drawing.Size size)
    {
        return new System.Windows.Size(size.Width, size.Height);
    }

}
