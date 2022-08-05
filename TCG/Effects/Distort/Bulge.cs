using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;
using TCG.Base.Parameters;

namespace TCG.Effects;

public class Bulge : IEffect
{
    public PointParameter Point { get; set; } = new();
    public FloatParameter Radius { get; set; } = new(50f) { Min = 1, Max = 150};
    public FloatParameter Strenght { get; set; } = new(0.5f) { Min = 0, Max = 2f };

    public Bulge() { }

    public Bulge(Point point, float radius, float strenght)
    {
        Point.Value = point;
        Radius.Value = radius;
        Strenght.Value = strenght;
    }

    public Bulge WithPoint(Point value)
    {
        Point.Value = value;
        return this;
    }

    public Bulge WithPoint(int x, int y)
    {
        Point.Value = new Point(x,y);
        return this;
    }

    public Bulge WithRandomizedPoint(int minX, int maxX, int minY, int maxY)
    {
        Point.X.Min = minX;
        Point.X.Max = maxX;
        Point.Y.Min = minY;
        Point.Y.Max = maxY;
        return this;
    }

    public Bulge WithRadius(float value)
    {
        Radius.Value = value;
        return this;
    }

    public Bulge WithRandomizedRadius(float min, float max)
    {
        Radius.Min = min;
        Radius.Max = max;
        return this;
    }

    public Bulge WithStrenght(float value)
    {
        Strenght.Value = value;
        return this;
    }

    public Bulge WithRandomizedStrenght(float min, float max)
    {
        Strenght.Min = min;
        Strenght.Max = max;
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Bulge(Point.X, Point.Y, Radius, Strenght));
}
