using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCG.Base.Abstract;
using TCG.Base.Interfaces;

namespace TCG.Base.Parameters;

public class FloatParameter : GenericStructParameter<float>, IHasMinMax<float>
{
    public float Max { get; set; }

    public float Min { get; set; }

    public FloatParameter(float defaultValue = default) : base(defaultValue) { }

    protected override void RandomizeImplementation(Random r)
    {
        Value = r.NextSingle() * (Max - Min) + Min;
    }
}
