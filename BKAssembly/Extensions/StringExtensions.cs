using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKAssembly.Extensions;

public static class StringExtensions
{
    public static bool IsNullOrEmpty(this string str)
    {
        return string.IsNullOrEmpty(str);
    }

    public static bool IsImage(this string str)
    {
        if (str.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase))
            return true;
        if (str.EndsWith(".png", StringComparison.OrdinalIgnoreCase))
            return true;
        if (str.EndsWith(".jepg", StringComparison.OrdinalIgnoreCase))
            return true;
        if (str.EndsWith(".gif", StringComparison.OrdinalIgnoreCase))
            return true;
        if (str.EndsWith(".webp", StringComparison.OrdinalIgnoreCase))
            return true;
        if (str.EndsWith(".bmp", StringComparison.OrdinalIgnoreCase))
            return true;
        if (str.EndsWith(".svg", StringComparison.OrdinalIgnoreCase))
            return true;
        if (str.EndsWith(".tiff", StringComparison.OrdinalIgnoreCase))
            return true;
        if (str.EndsWith(".ico", StringComparison.OrdinalIgnoreCase))
            return true;
        return false;
    }

    public static int ToInt(this string str)
    {
        int result = 0;
        int.TryParse(str, out result);
        return result;
    }

    public static float ToFloat(this string str)
    {
        float result = 0;
        float.TryParse(str, out result);
        return result;
    }

    public static T ToEnum<T>(this string str, T defaultValue)
        where T : Enum
    {
        return EnumExtensions.TryParse(str, defaultValue);
    }
}
