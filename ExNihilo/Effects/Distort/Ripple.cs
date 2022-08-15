using ExNihilo.Base;
using ExNihilo.Extensions.Processors;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ExNihilo.Effects;

/// <summary>
/// Defines effect that allow the application of ripple effect on an <see cref="Visual"/>
/// </summary>
public class Ripple : Effect
{
    /// <summary>
    /// Coordinates of effect center.
    /// </summary>
    public PointProperty Point { get; set; } = new();
    /// <summary>
    /// Radius of effect in pixels. Must be greater or equal to 1.
    /// </summary>
    public FloatProperty Radius { get; set; } = new(1, float.MaxValue, 1100f) { Min = 1f, Max = 100f };
    /// <summary>
    /// Wavelength of ripples, in pixels.Must be greater or equal to 1.
    /// </summary>
    public FloatProperty WaveLength { get; set; } = new FloatProperty(1, float.MaxValue, 10f) { Min = 1f, Max = 10f };
    /// <summary>
    /// Approximate width of wave train, in wavelengths. Must be greater or equal to 1.
    /// </summary>
    public FloatProperty TraintWidth { get; set; } = new(1, float.MaxValue, 2) { Min = 1f, Max = 10f };

    /// <summary>
    /// <inheritdoc cref="Ripple"/>
    /// </summary>
    public Ripple() { }

    /// <summary>
    /// <inheritdoc cref="Ripple"/>
    /// </summary>
    /// <param name="point"><inheritdoc cref="Point" path="/summary"/></param>
    /// <param name="radius"><inheritdoc cref="Radius" path="/summary"/></param>
    /// <param name="waveLength"><inheritdoc cref="WaveLength" path="/summary"/></param>
    /// <param name="traintWidth"><inheritdoc cref="TraintWidth" path="/summary"/></param>
    public Ripple(Point point, float radius, float waveLength, float traintWidth)
    {
        Point.WithValue(point);
        Radius.Value = radius;
        WaveLength.Value = waveLength;
        TraintWidth.Value = traintWidth;
    }

    /// <summary>
    /// Set Point value
    /// </summary>
    /// <param name="x">Define x coordinate of Point</param>
    /// <param name="y">Define y coordinate of Point</param>
    public Ripple WithPoint(int x, int y)
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
    public Ripple WithRandomizedPoint(int minX, int maxX, int minY, int maxY)
    {
        Point.WithRandomizedValue(minX, maxX, minY, maxY);
        return this;
    }

    /// <summary>
    /// Set Radius value
    /// </summary>
    /// <param name="value"><inheritdoc cref="Radius" path="/summary"/></param>
    public Ripple WithRadius(float value)
    {
        Radius.Value = value;
        return this;
    }
    /// <summary>
    /// Set Radius randomization parameters.
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="Radius" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="Radius" path="/summary"/></param>
    public Ripple WithRandomizedRadius(float min, float max)
    {
        Radius.Min = min;
        Radius.Max = max;
        return this;
    }

    /// <summary>
    /// Set Wave length value
    /// </summary>
    /// <param name="value"><inheritdoc cref="WaveLength" path="/summary"/></param>
    public Ripple WithWaveLength(float value)
    {
        WaveLength.Value = value;
        return this;
    }
    /// <summary>
    /// Set Wave length randomization parameters.
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="WaveLength" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="WaveLength" path="/summary"/></param>
    public Ripple WithRandomizedWaveLength(float min, float max)
    {
        WaveLength.Min = min;
        WaveLength.Max = max;
        return this;
    }

    /// <summary>
    /// Set Traint width value
    /// </summary>
    /// <param name="value"><inheritdoc cref="TraintWidth" path="/summary"/></param>
    public Ripple WithTraintWidth(float value)
    {
        TraintWidth.Value = value;
        return this;
    }
    /// <summary>
    /// Set Traing width randomization parameters.
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="TraintWidth" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="TraintWidth" path="/summary"/></param>
    public Ripple WithRandomizedTraintWidth(float min, float max)
    {
        TraintWidth.Min = min;
        TraintWidth.Max = max;
        return this;
    }

    public override void Render(Image image, GraphicsOptions graphicsOptions)
    {
        image.Mutate(x => x.Ripple(Point.X, Point.Y, Radius, WaveLength, TraintWidth));
    }
}
