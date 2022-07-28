using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCG.Base.Abstract;

namespace TCG.Base.Parameters;

public class PointFArrayParameter : ArrayParameter<PointF>
{
    public PointFArrayParameter(PointF[] defaultValue) : base(defaultValue)
    {
    }

    protected override void RandomizeImplementation(Random r)
    {
        Length.Randomize(r);
        
        Value = new PointF[Length];
        for (int i = 0; i < Length; i++)
        {
            Value[i] = r.NextSingle() * (Max - Min) + Min;
        }
        
    }
}
