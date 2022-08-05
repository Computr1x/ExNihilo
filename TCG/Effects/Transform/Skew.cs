using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;

namespace TCG.Effects;

public class Skew : IEffect
{
    public FloatParameter XDegree { get; set; } = new(0) { Min = 0, Max = 360 };
    public FloatParameter YDegree { get; set; } = new(0) { Min = 0, Max = 360 };

    public Skew() { }

    public Skew(float xDegree, float yDegree)
    {
        XDegree.Value = xDegree;
        YDegree.Value = yDegree;
    }

    public Skew WithXDegree(float value)
    {
        XDegree.Value = value;
        return this;
    }

    public Skew WithRandomizedXDegree(float min, float max)
    {
        XDegree.Min = min;
        XDegree.Max = max;
        return this;
    }

    public Skew WithYDegree(float value)
    {
        YDegree.Value = value;
        return this;
    }

    public Skew WithRandomizedYDegree(float min, float max)
    {
        YDegree.Min = min;
        YDegree.Max = max;
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x =>
            x.Transform(new AffineTransformBuilder().AppendSkewDegrees(XDegree, YDegree)));
}
