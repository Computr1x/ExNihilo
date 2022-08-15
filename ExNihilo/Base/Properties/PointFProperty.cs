using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExNihilo.Base.Abstract;
using ExNihilo.Base.Properties;

namespace ExNihilo.Base.Properties;

public class PointFProperty : ComplexProperty
{
    public FloatProperty X { get; init; } = new FloatProperty(0);
    public FloatProperty Y { get; init; } = new FloatProperty(0);

    protected override void RandomizeImplementation(Random r)
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