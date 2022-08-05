using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;

namespace TCG.Effects;

public class Scale : IEffect
{
    public FloatParameter XScale { get; set; } = new(0) { Min = 0, Max = 2 };
    public FloatParameter YScale { get; set; } = new(0) { Min = 0, Max = 2 };

    public Scale() { }

    public Scale(float xScale, float yScale)
    {
        XScale.Value = xScale;
        YScale.Value = yScale;
    }

    public Scale WithXScale(float value)
    {
        XScale.Value = value;
        return this;
    }

    public Scale WithRandomizedXScale(float min, float max)
    {
        XScale.Min = min;
        XScale.Max = max;
        return this;
    }

    public Scale WithYScale(float value)
    {
        YScale.Value = value;
        return this;
    }

    public Scale WithRandomizedYScale(float min, float max)
    {
        YScale.Min = min;
        YScale.Max = max;
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x =>
            x.Transform(new AffineTransformBuilder().AppendScale(new System.Numerics.Vector2(XScale, YScale))));
}
