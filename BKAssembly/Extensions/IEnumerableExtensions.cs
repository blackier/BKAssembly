using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKAssembly.Extensions;

public static class IEnumerableExtensions
{
    public static bool IsNullOrEmpty<TSource>(this IEnumerable<TSource> source)
    {
        return source == null || source.Count() == 0;
    }

    public static bool IsNotNullOrEmpty<TSource>(this IEnumerable<TSource> source)
    {
        return source != null && source.Count() > 0;
    }

    public static bool TryGet<TSource>(this IEnumerable<TSource> source, int index, [MaybeNullWhen(false)] out TSource value)
    {
        if (index >= 0 && index < source.Count())
        {
            value = source.ElementAt(index);
            return true;
        }
        value = default;
        return false;
    }
}
