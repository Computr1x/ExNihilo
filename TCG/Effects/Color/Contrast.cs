using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;

namespace TCG.Effects;

public class Contrast : IEffect
{
    public FloatParameter Amount { get; set; } = new FloatParameter(3) { Min = 0, Max = 3};

    public Contrast() { }

    public Contrast(float amount)
    {
        Amount.Value = amount;
    }

    public Contrast WithAmount(float value)
    {
        Amount.Value = value;
        return this;
    }

    public Contrast WithRandomizedAmount(float min, float max)
    {
        Amount.Min = min;
        Amount.Max = max;
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Contrast(Amount));
}