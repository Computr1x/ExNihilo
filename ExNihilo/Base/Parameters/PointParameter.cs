using SixLabors.ImageSharp;
using System.Numerics;
using ExNihilo.Base.Abstract;

namespace ExNihilo.Base.Properties;

public class PointProperty : ComplexProperty
{
    public IntProperty X { get; init; } = new IntProperty(0);
    public IntProperty Y { get; init; } = new IntProperty(0);

    public PointProperty WithValue(Point value)
    {
        X.WithValue(value.X);
        Y.WithValue(value.Y);
        return this;
    }

    public PointProperty WithRandomizedValue(int minX, int maxX, int minY, int maxY)
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

    public static implicit operator PointF(PointProperty pointProperty)
    {
        return new PointF(pointProperty.X, pointProperty.Y);
    }

    public static implicit operator Point(PointProperty pointProperty)
    {
        return new Point(pointProperty.X, pointProperty.Y);
    }
}