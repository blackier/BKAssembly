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

    public static bool IsNotNullOrEmpty(this string str)
    {
        return !string.IsNullOrEmpty(str);
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
