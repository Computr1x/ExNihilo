using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using ExNihilo.Base.Interfaces;
using ExNihilo.Extensions.Processors;
using ExNihilo.Base.Properties;

namespace ExNihilo.Effects;

/// <summary>
/// Defines effect that allow the application of swirl effect on an <see cref="Drawable"/>
/// </summary>
public class Swirl : Effect
{
    /// <summary>
    /// Coordinates of effect center.
    /// </summary>
    public PointProperty Point { get; set; } = new();
    /// <summary>
    /// Radius of effect. Must be greater or equal to 0.
    /// </summary>
    public FloatProperty Radius { get; set; } = new(0, float.MaxValue, 100f) { Min = 1f, Max = 150f };
    /// <summary>
    /// Swirl angle in degrees. Must be greater or equal to 0.
    /// </summary>
    public FloatProperty Degree { get; set; } = new(0, float.MaxValue, 10f) { Min = 0f, Max = 360f };
    /// <summary>
    /// Swirl twists count. Must be greater or equal to 0.
    /// </summary>
    public FloatProperty Twists { get; set; } = new(0, float.MaxValue, 0.5f) { Min = 0f, Max = 3f };

    /// <summary>
    /// <inheritdoc cref="Swirl"/>
    /// </summary>
    public Swirl() { }

    /// <summary>
    /// <inheritdoc cref="Swirl"/>
    /// </summary>
    /// <param name="point"><inheritdoc cref="Point" path="/summary"/></param>
    /// <param name="radius"><inheritdoc cref="Radius" path="/summary"/></param>
    /// <param name="degree"><inheritdoc cref="Degree" path="/summary"/></param>
    /// <param name="twists"><inheritdoc cref="Twists" path="/summary"/></param>
    public Swirl(Point point, float radius, float degree, float twists)
    {
        Point.WithValue(point);
        Radius.WithValue(radius);
        Degree.WithValue(degree);
        Twists.WithValue(twists);
    }

    /// <summary>
    /// Set Point value
    /// </summary>
    /// <param name="x">Define x coordinate of Point</param>
    /// <param name="y">Define y coordinate of Point</param>
    public Swirl WithPoint(int x, int y)
    {
        Point.WithValue(new Point(x, y));
        return this;
    }

    /// <summary>
    /// Set Point randomization parameters.
    /// </summary>
    /// <param name="minX">Minimal randomization value of x asix.</param>
    /// <param name="maxX">Maximum randomization value of x asix.</param>
    /// <param name="minY">Minimal randomization value of y asix.</param>
    /// <param name="maxY">Maximum randomization value of y asix.</param>
    public Swirl WithRandomizedPoint(int minX, int maxX, int minY, int maxY)
    {
        Point.WithRandomizedValue(minX, maxX, minY, maxY);
        return this;
    }

    /// <summary>
    /// Set Radius value
    /// </summary>
    /// <param name="value"><inheritdoc cref="Radius" path="/summary"/></param>
    public Swirl WithRadius(float value)
    {
        Radius.Value = value;
        return this;
    }

    /// <summary>
    /// Set Radius randomization parameters.
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="Radius" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="Radius" path="/summary"/></param>
    public Swirl WithRandomizedRadius(float min, float max)
    {
        Radius.WithRandomizedValue(min, max);
        return this;
    }

    /// <summary>
    /// Set Degree value
    /// </summary>
    /// <param name="value"><inheritdoc cref="Degree" path="/summary"/></param>
    public Swirl WithDegree(float value)
    {
        Degree.Value = value;
        return this;
    }

    /// <summary>
    /// Set Degree randomization parameters.
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="Degree" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="Degree" path="/summary"/></param>
    public Swirl WithRandomizedDegree(float min, float max)
    {
        Degree.WithRandomizedValue(min, max);
        return this;
    }

    /// <summary>
    /// Set Twists value
    /// </summary>
    /// <param name="value"><inheritdoc cref="Twists" path="/summary"/></param>
    public Swirl WithTwists(float value)
    {
        Twists.Value = value;
        return this;
    }
    /// <summary>
    /// Set Twists randomization parameters.
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="Twists" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="Twists" path="/summary"/></param>
    public Swirl WithRandomizedTwists(float min, float max)
    {
        Twists.WithRandomizedValue(min, max);
        return this;
    }

    public override void Render(Image image, GraphicsOptions graphicsOptions)
    {
        image.Mutate(x => x.Swirl(Point.X, Point.Y, Radius, Degree, Twists));
    }
}
