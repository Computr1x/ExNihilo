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
    public IntParameter Length { get; } = new IntParameter(0);

    public IntParameter X { get; } = new IntParameter(0);
    public IntParameter Y { get;  } = new IntParameter(0);

    public PointFArrayParameter(PointF[] defaultValue) : base(defaultValue)
    {
    }

    public PointFArrayParameter WithLength(int value)
    {
        Length.WithValue(value);
        return this;
    }

    public PointFArrayParameter WithRadnomizeLength(int min, int max)
    {
        Length.WithRandomizedValue(min, max);
        return this;
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
