using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCG.Rnd.Range;

public class BasicRange<T> where T : struct
{
    public T Max { get; }

    public T Min { get; }

    public BasicRange(T min, T max)
    {
        Min = min;
        Max = max;
    }

    public BasicRange(T length)
    {
        Min = length;
        Max = length;
    }
}
