using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKAssembly.Extensions;

public static class NumberExtensions
{
    public static string ToFileSize(this long value)
    {
        double unit = 1024;
        if (value < unit)
            return $"{value} B";

        unit *= 1024;
        if (value < unit)
            return (value * 1024 / unit).ToString("0.00 KB");

        unit *= 1024;
        if (value < unit)
            return (value * 1024 / unit).ToString("0.00 MB");

        unit *= 1024;
        if (value < unit)
            return (value * 1024 / unit).ToString("0.00 GB");

        unit *= 1024;
        if (value < unit)
            return (value * 1024 / unit).ToString("0.00 TB");

        unit *= 1024;
        if (value < unit)
            return (value * 1024 / unit).ToString("0.00 PB");

        unit *= 1024;
        if (value < unit)
            return (value * 1024 / unit).ToString("0.00 EB");

        unit *= 1024;
        if (value < unit)
            return (value * 1024 / unit).ToString("0.00 ZB");

        unit *= 1024;
        return (value * 1024 / unit).ToString("0.00 YB");
    }
}
