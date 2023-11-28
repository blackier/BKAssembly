using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKAssembly.Extensions;

public static class ObjectExtensions
{
    public static bool IsNull(this object value)
    {
        return value == null;
    }

    public static bool IsNotNull(this object value)
    {
        return value != null;
    }
}