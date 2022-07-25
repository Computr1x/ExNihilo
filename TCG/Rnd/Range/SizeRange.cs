using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCG.Rnd.Range;

public class SizeRange<T> where T : struct
{
    public BasicRange<T> widthRange { get; }
    public BasicRange<T> heightRange { get; }

    public SizeRange(T width, T height)
    {
        widthRange = new(width);
        heightRange = new(height);
    }

    public SizeRange(BasicRange<T> widthRange, BasicRange<T> heightRange)
    {
        this.widthRange = widthRange;
        this.heightRange = heightRange;
    }
}
