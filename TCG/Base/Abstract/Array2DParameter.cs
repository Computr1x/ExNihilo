using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCG.Base.Interfaces;
using TCG.Rnd.Randomizers.Parameters;

namespace TCG.Base.Abstract;

public abstract class Array2DParameter<T> : GenericParameter<T[,]>
{
    protected Array2DParameter(T[,] defaultValue) : base(defaultValue)
    {
    }

    public IntParameter Width { get; set; } = new IntParameter(0);
    public IntParameter Height { get; set; } = new IntParameter(0);

    public T Min { get; set; }
    public T Max { get; set; }
}