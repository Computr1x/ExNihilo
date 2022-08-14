using SixLabors.ImageSharp;
using System.Numerics;
using ExNihilo.Base.Abstract;

namespace ExNihilo.Base.Parameters;

public class PointParameter : ComplexParameter
{
    public IntParameter X { get; init; } = new IntParameter(0);
    public IntParameter Y { get; init; } = new IntParameter(0);

    public PointParameter WithValue(Point value)
    {
        X.WithValue(value.X);
        Y.WithValue(value.Y);
        return this;
    }

    public PointParameter WithRandomizedValue(int minX, int maxX, int minY, int maxY)
    {
        X.WithRandomizedValue(minX, maxX);
        Y.WithRandomizedValue(minY, maxY);
        return this;
    }

    protected override void RandomizeImplementation(Random r)
    {
        X.Randomize(r);
        Y.Randomize(r);
    }

    public static implicit operator SixLabors.ImageSharp.PointF(PointParameter pointParameter)
    {
        return new PointF(pointParameter.X, pointParameter.Y);
    }

    public static implicit operator SixLabors.ImageSharp.Point(PointParameter pointParameter)
    {
        return new SixLabors.ImageSharp.Point(pointParameter.X, pointParameter.Y);
    }
}