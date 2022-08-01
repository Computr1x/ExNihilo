using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCG.Base.Abstract;
using TCG.Base.Parameters;

namespace TCG.Base.Parameters;

public class PointFArrayParameter : GenericParameter<PointF[]>
{
    public IntParameter Length { get; set; } = new IntParameter(0);

    public IntParameter X { get; set; } = new IntParameter(0);
    public IntParameter Y { get; set; } = new IntParameter(0);

    public PointFArrayParameter(PointF[] defaultValue) : base(defaultValue)
    {
    }

    protected override void RandomizeImplementation(Random r)
    {
        Length.Randomize(r);
        
        Value = new PointF[Length];
        for (int i = 0; i < Length; i++)
        {
            X.Randomize(r);
            Y.Randomize(r);
            Value[i] = new PointF(X, Y);
        }
    }
}
