using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;
using TCG.Base.Parameters;

namespace TCG.Effects;

public class Ripple : IEffect
{
    public PointParameter Point { get; set; } = new();
    // radius of effect in pixels
    public FloatParameter Radius { get; set; } = new(100f) { Min = 1f, Max = 100f };
    //  wavelength of ripples, in pixels
    public FloatParameter WaveLength { get; set; } = new FloatParameter(10f) { Min = 1f, Max = 10f };
    // approximate width of wave train, in wavelengths
    public FloatParameter TraintWidth { get; set; } = new(2) { Min = 1f, Max = 10f };

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
