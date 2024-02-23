using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKAssembly.Extensions;

public static class IntPtrExtensions
{
    public static int Get_X_Param(this IntPtr value)
    {
        return (short)(value.ToInt32() & 0xFFFF);
    }

    public static int Get_Y_Param(this IntPtr value)
    {
        return (short)(value.ToInt32() >> 16);
    }
}
