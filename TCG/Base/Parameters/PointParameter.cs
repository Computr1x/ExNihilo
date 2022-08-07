using SixLabors.ImageSharp;
using System.Numerics;
using TCG.Base.Abstract;

namespace TCG.Base.Parameters;

public class PointParameter : GenericStructParameter<Point>
{
    public IntParameter X { get; init; } = new IntParameter(0);
    public IntParameter Y { get; init; } = new IntParameter(0);

    public PointParameter() : base(new Point())
    {
    }

    public PointParameter(Point defaultValue) : base(defaultValue)
    {
    }

    public override PointParameter WithValue(Point value)
    {
        X.WithValue(value.X);
        Y.WithValue(value.Y);
        return this;
    }

    protected override void RandomizeImplementation(Random r)
    {
        X.Randomize(r);
        Y.Randomize(r);
        
        Value = new(X, Y);
    }

    public static implicit operator PointF(PointParameter pointParameter)
    {
        Point p = pointParameter;
        return new PointF(p.X, p.Y);
    }
}