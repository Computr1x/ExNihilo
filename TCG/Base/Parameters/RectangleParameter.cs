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

    public Rectangle WithPoint(Point p)
    {
        Point.Value = p;
        return this;
    }

    public Rectangle WithRandomizedPoint(int minX, int maxX, int minY, int maxY)
    {
        Point.X.Min = minX;
        Point.X.Max = maxX;
        Point.Y.Min = minY;
        Point.Y.Max = maxY;
        return this;
    }

    public Rectangle WithSize(Size size)
    {
        Size.Value = size;
        return this;
    }

    public Rectangle WithRandomizedSize(int minWidth, int maxWidth, int minHeight, int maxHeight)
    {
        Size.Width.Min = minWidth;
        Size.Width.Max = maxWidth;
        Size.Height.Min = minHeight;
        Size.Height.Max = maxHeight;
        return this;
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
