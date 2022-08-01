using SixLabors.ImageSharp;
using TCG.Base.Abstract;

namespace TCG.Base.Parameters;

public class RectangleParameter : GenericStructParameter<Rectangle>
{
    public PointParameter Point { get; init; } = new PointParameter(default);
    public SizeParameter Size { get; init; } = new SizeParameter(default);

    public RectangleParameter(Rectangle defaultValue = default) : base(defaultValue)
    {
    }

    protected override void RandomizeImplementation(Random r)
    {
        Point.Randomize(r);
        Size.Randomize(r);
        
        Value = new(Point, Size);
    }

    public static implicit operator RectangleF(RectangleParameter rectParamater)
    {
        Rectangle rect = rectParamater;
        return new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);
    }
}
