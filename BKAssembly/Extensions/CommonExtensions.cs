using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BKAssembly.Extensions;

public static class CommonExtensions
{
    public static T? CloneWithJson<T>(this T theObject)
    {
        string jsonData = BKMisc.JsonSerialize(theObject);
        return BKMisc.JsonDeserialize<T>(jsonData);
    }
}
