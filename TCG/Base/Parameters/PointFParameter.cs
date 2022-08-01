using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCG.Base.Abstract;
using TCG.Base.Parameters;

namespace TCG.Base.Parameters;

public class PointFParameter : GenericStructParameter<PointF>
{
    public PointFParameter(PointF defaultValue) : base(defaultValue)
    {
    }

    public FloatParameter X { get; init; } = new FloatParameter(0);
    public FloatParameter Y { get; init; } = new FloatParameter(0);

    protected override void RandomizeImplementation(Random r)
    {
        X.Randomize(r);
        Y.Randomize(r);
        
        Value = new(X, Y);
    }

    public static implicit operator Point(PointFParameter pointParameter)
    {
        PointF p = pointParameter;
        return new Point((int)p.X, (int)p.Y);
    }
}