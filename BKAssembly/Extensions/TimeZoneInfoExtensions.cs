using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKAssembly.Extensions;

public static class TimeZoneInfoExtensions
{
    public static DateTime ToUtcTime(this TimeZoneInfo value, TimeSpan offset)
    {
        return TimeZoneInfo
            .ConvertTime(new DateTime(1970, 1, 1, TimeZoneInfo.Local.BaseUtcOffset.Hours, 0, 0), value)
            .Add(offset);
    }
}
