using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using ExNihilo.Base.Interfaces;
using ExNihilo.Extensions.Processors;
using ExNihilo.Base.Properties;

namespace ExNihilo.Effects;

/// <summary>
/// Defines effect that allow the application of bulge effect on an <see cref="Drawable"/>
/// </summary>
public class Bulge : Effect
{
    /// <summary>
    /// Center of bulge effect
    /// </summary>
    public PointProperty Point { get; set; } = new();
    /// <summary>
    /// Radius of effect
    /// </summary>
    public FloatProperty Radius { get; set; } = new(1, int.MaxValue, 50f) { Min = 1, Max = 150};
    /// <summary>
    /// Amount of bulge (0.0-1.0)
    /// </summary>
    public FloatProperty Strenght { get; set; } = new(0.5f) { Min = 0, Max = 2f };

    /// <summary>
    /// <inheritdoc cref="Bulge"/>
    /// </summary>
    public Bulge() { }

    /// <summary>
    /// <inheritdoc cref="Bulge"/>
    /// </summary>
    /// <param name="point"><inheritdoc cref="Point" path="/summary"/></param>
    /// <param name="radius"><inheritdoc cref="Radius" path="/summary"/></param>
    /// <param name="strenght"><inheritdoc cref="Strenght" path="/summary"/></param>
    public Bulge(Point point, float radius, float strenght)
    {
        Point.WithValue(point);
        Radius.Value = radius;
        Strenght.Value = strenght;
    }

    /// <summary>
    /// Set Point value
    /// </summary>
    /// <param name="value"><inheritdoc cref="Point" path="/summary"/></param>
    public Bulge WithPoint(Point value)
    {
        Point.WithValue(value);
        return this;
    }

    /// <summary>
    /// Set Point value
    /// </summary>
    /// <param name="x">Define x coordinate of Point</param>
    /// <param name="y">Define y coordinate of Point</param>
    public Bulge WithPoint(int x, int y)
    {
        Point.WithValue(new Point(x, y));
        return this;
    }
    /// <summary>
    /// Set Point randomization parameters.
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="Point" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="Point" path="/summary"/></param>
    public Bulge WithRandomizedPoint(int minX, int maxX, int minY, int maxY)
    {
        Point.WithRandomizedValue(minX, maxX, minY, maxY);
        return this;
    }

    /// <summary>
    /// Set Radius value
    /// </summary>
    /// <param name="value"><inheritdoc cref="Radius" path="/summary"/></param>
    public Bulge WithRadius(float value)
    {
        Radius.Value = value;
        return this;
    }
    /// <summary>
    /// Set Radius randomization parameters.
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="Radius" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="Radius" path="/summary"/></param>
    public Bulge WithRandomizedRadius(float min, float max)
    {
        Radius.WithRandomizedValue(min, max);
        return this;
    }

    /// <summary>
    /// Set Strenght value
    /// </summary>
    /// <param name="value"><inheritdoc cref="Strenght" path="/summary"/></param>
    public Bulge WithStrenght(float value)
    {
        Strenght.Value = value;
        return this;
    }
    /// <summary>
    /// Set Strenght randomization parameters.
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="Strenght" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="Strenght" path="/summary"/></param>
    public Bulge WithRandomizedStrenght(float min, float max)
    {
        Strenght.Min = min;
        Strenght.Max = max;
        return this;
    }

    public override void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Bulge(Point.X, Point.Y, Radius, Strenght));
}
