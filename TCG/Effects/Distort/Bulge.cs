using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;
using TCG.Base.Parameters;

namespace TCG.Effects;

/// <summary>
/// Defines effect that allow the application of bulge effect on an <see cref="IDrawable"/>
/// </summary>
public class Bulge : IEffect
{
    /// <summary>
    /// Center of bulge effect
    /// </summary>
    public PointParameter Point { get; set; } = new();
    /// <summary>
    /// Radius of effect
    /// </summary>
    public FloatParameter Radius { get; set; } = new(1, int.MaxValue, 50f) { Min = 1, Max = 150};
    /// <summary>
    /// Amount of bulge (0.0-1.0)
    /// </summary>
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
        Point.WithValue(new SixLabors.ImageSharp.Point(x, y));
        return this;
    }

    public Bulge WithRandomizedPoint(int minX, int maxX, int minY, int maxY)
    {
        Point.WithRandomizedValue(minX, maxX, minY, maxY);
        return this;
    }

    public Bulge WithRadius(float value)
    {
        Radius.Value = value;
        return this;
    }

    public Bulge WithRandomizedRadius(float min, float max)
    {
        Radius.WithRandomizedValue(min, max);
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
