using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;

namespace TCG.Effects;

public class Glow : IEffect
{
    public FloatParameter Radius { get; set; } = new FloatParameter(15) { Min = 1, Max = 150 };

    public Glow() { }

    public Glow(float radius)
    {
        Radius.Value = radius;
    }

    public Glow WithRadius(float value)
    {
        Radius.Value = value;
        return this;
    }

    public Glow WithRandomizedRadius(float min, float max)
    {
        Radius.Min = min;
        Radius.Max = max;
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Glow(Radius));
}
