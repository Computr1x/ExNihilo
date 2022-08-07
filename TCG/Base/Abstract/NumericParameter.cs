using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCG.Base.Interfaces;

namespace TCG.Base.Abstract;
internal class NumericParameter
{
}


public abstract class NumericParameter<T> : GenericStructParameter<T>, IHasMinMax<T> where T : struct
{
    public T Max { get; set; }

    public T Min { get; set; }

    public NumericParameter(T defaultValue = default) : base(defaultValue) { }

    public NumericParameter<T> WithRandomizedValue(T min, T max)
    {
        Min = min;
        Max = max;
        return this;
    }
}
