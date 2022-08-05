using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;

namespace TCG.Effects;

public class Opacity : IEffect
{
    // beteween 0 - 1
    public FloatParameter Amount { get; set; } = new(0.5f) { Min = 0, Max = 1 };

    public Opacity() { }

    public Opacity(float amount)
    {
        Amount.Value = amount;
    }

    public Opacity WithAmount(float value)
    {
        Amount.Value = value;
        return this;
    }

    public Opacity WithRandomizedAmount(float min, float max)
    {
        Amount.Min = min;
        Amount.Max = max;
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Opacity(Amount));
}
