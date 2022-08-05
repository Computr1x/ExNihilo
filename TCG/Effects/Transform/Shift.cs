using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;

namespace TCG.Effects;

public class Shift : IEffect
{
    public FloatParameter XShift { get; set; } = new(0) { Min = 0, Max = 50 };
    public FloatParameter YShift { get; set; } = new(0) { Min = 0, Max = 50 };

    public Shift() { }

    public Shift(float xShift, float yShift)
    {
        XShift.Value = xShift;
        YShift.Value = yShift;
    }

    public Shift WithXShift(float value)
    {
        XShift.Value = value;
        return this;
    }

    public Shift WithRandomizedXShift(float min, float max)
    {
        XShift.Min = min;
        XShift.Max = max;
        return this;
    }

    public Shift WithYShift(float value)
    {
        YShift.Value = value;
        return this;
    }

    public Shift WithRandomizedYShift(float min, float max)
    {
        YShift.Min = min;
        YShift.Max = max;
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Transform(new AffineTransformBuilder().AppendTranslation(new System.Numerics.Vector2(XShift, YShift))));
}
