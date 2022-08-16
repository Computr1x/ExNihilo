using SixLabors.ImageSharp;

namespace ExNihilo.Base;

public class RectangleProperty : Property
{
    public PointProperty Point { get; } = new PointProperty();
    public SizeProperty Size { get; } = new SizeProperty();

    public RectangleProperty WithValue(Rectangle value)
    {
        Point.WithValue(new Point(value.X, value.Y));
        Size.WithValue(new Size(value.Width, value.Height));
        return this;
    }

    public RectangleProperty WithPoint(Point p)
    {
        Point.WithValue(p);
        return this;
    }

    public RectangleProperty WithRandomizedPoint(int minX, int maxX, int minY, int maxY)
    {
        Point.WithRandomizedValue(minX, maxX, minY, maxY);
        return this;
    }

    public RectangleProperty WithSize(Size size)
    {
        Size.WithValue(size);
        return this;
    }

    public RectangleProperty WithRandomizedSize(int minWidth, int maxWidth, int minHeight, int maxHeight)
    {
        Size.WithRandomizedValue(minWidth, maxWidth, minHeight, maxHeight);
        return this;
    }

    public override void Randomize(Random r, bool force = false)
    {
        Point.Randomize(r);
        Size.Randomize(r);
    }

    public static implicit operator RectangleF(RectangleProperty rectParamater)
    {
        return new RectangleF(rectParamater.Point, rectParamater.Size);
    }

    public static implicit operator Rectangle(RectangleProperty rectParamater)
    {
        return new Rectangle(rectParamater.Point, rectParamater.Size);
    }
}
