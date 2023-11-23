using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BKAssembly.Extensions;

public static class EnumExtensions
{
    public static IEnumerable<T> GetTypeList<T>() where T : Enum
    {
        foreach (object item in Enum.GetValues(typeof(T)))
        {
            yield return (T)item;
        }
    }
    public static IEnumerable<T> GetTypeList<T>(this T value) where T : Enum
    {
        foreach (object item in Enum.GetValues(typeof(T)))
        {
            yield return (T)item;
        }
    }

    public static int ToInt(this Enum value)
    {
        return value.GetHashCode();
    }
}
