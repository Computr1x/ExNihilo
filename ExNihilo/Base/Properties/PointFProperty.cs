using SixLabors.ImageSharp;

namespace ExNihilo.Base;

public class PointFProperty : Property
{
    public FloatProperty X { get; init; } = new FloatProperty(0);
    public FloatProperty Y { get; init; } = new FloatProperty(0);

    public override void Randomize(Random r, bool force = false)
    {
        X.Randomize(r);
        Y.Randomize(r);
    }

    public PointFProperty WithValue(PointF value)
    {
        X.WithValue(value.X);
        Y.WithValue(value.Y);
        return this;
    }

    public PointFProperty WithRandomizedValue(int minX, int maxX, int minY, int maxY)
    {
        X.WithRandomizedValue(minX, maxX);
        Y.WithRandomizedValue(minY, maxY);
        return this;
    }

    public static implicit operator PointF(PointFProperty pointProperty)
    {
        return new PointF((int) pointProperty.X, (int) pointProperty.Y);
    }
}