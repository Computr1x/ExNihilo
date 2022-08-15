using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExNihilo.Base.Abstract;
using ExNihilo.Base.Parameters;

namespace ExNihilo.Base.Parameters;

public class PointFParameter : ComplexParameter
{
    public FloatParameter X { get; init; } = new FloatParameter(0);
    public FloatParameter Y { get; init; } = new FloatParameter(0);

    protected override void RandomizeImplementation(Random r)
    {
        X.Randomize(r);
        Y.Randomize(r);
    }

    public PointFParameter WithValue(PointF value)
    {
        X.WithValue(value.X);
        Y.WithValue(value.Y);
        return this;
    }

    public PointFParameter WithRandomizedValue(int minX, int maxX, int minY, int maxY)
    {
        X.WithRandomizedValue(minX, maxX);
        Y.WithRandomizedValue(minY, maxY);
        return this;
    }

    public static implicit operator PointF(PointFParameter pointParameter)
    {
        return new PointF((int) pointParameter.X, (int) pointParameter.Y);
    }
}