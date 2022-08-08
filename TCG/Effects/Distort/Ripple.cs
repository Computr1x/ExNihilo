using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;
using TCG.Base.Parameters;

namespace TCG.Effects;

/// <summary>
/// Defines effect that allow the application of ripple effect on an <see cref="IDrawable"/>
/// </summary>
public class Ripple : IEffect
{
    /// <summary>
    /// Coordinates of effect center.
    /// </summary>
    public PointParameter Point { get; set; } = new();
    /// <summary>
    /// Radius of effect in pixels. Must be greater or equal to 1.
    /// </summary>
    public FloatParameter Radius { get; set; } = new(1, float.MaxValue, 1100f) { Min = 1f, Max = 100f };
    /// <summary>
    /// Wavelength of ripples, in pixels.Must be greater or equal to 1.
    /// </summary>
    public FloatParameter WaveLength { get; set; } = new FloatParameter(1, float.MaxValue, 10f) { Min = 1f, Max = 10f };
    /// <summary>
    /// Approximate width of wave train, in wavelengths. Must be greater or equal to 1.
    /// </summary>
    public FloatParameter TraintWidth { get; set; } = new(1, float.MaxValue, 2) { Min = 1f, Max = 10f };

    public Ripple() { }

    public Ripple(Point point, float radius, float waveLength, float traintWidth)
    {
        Point.Value = point;
        Radius.Value = radius;
        WaveLength.Value = waveLength;
        TraintWidth.Value = traintWidth;
    }

    public Ripple WithPoint(int x, int y)
    {
        Point.Value = new Point(x, y);
        return this;
    }

    public Ripple WithRandomizedPoint(int minX, int maxX, int minY, int maxY)
    {
        Point.X.Min = minX;
        Point.X.Max = maxX;
        Point.Y.Min = minY;
        Point.Y.Max = maxY;
        return this;
    }

    public Ripple WithRadius(float value)
    {
        Radius.Value = value;
        return this;
    }

    public Ripple WithRandomizedRadius(float min, float max)
    {
        Radius.Min = min;
        Radius.Max = max;
        return this;
    }

    public Ripple WithWaveLength(float value)
    {
        WaveLength.Value = value;
        return this;
    }

    public Ripple WithRandomizedWaveLength(float min, float max)
    {
        WaveLength.Min = min;
        WaveLength.Max = max;
        return this;
    }

    public Ripple WithTraintWidth(float value)
    {
        TraintWidth.Value = value;
        return this;
    }

    public Ripple WithRandomizedTraintWidth(float min, float max)
    {
        TraintWidth.Min = min;
        TraintWidth.Max = max;
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions)
    {
        image.Mutate(x => x.Ripple(Point.X, Point.Y, Radius, WaveLength, TraintWidth));
    }
}
