using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;

namespace TCG.Effects;

/// <summary>
/// Defines effect that alter contrast component of the <see cref="IDrawable"/>
/// </summary>
public class Contrast : IEffect
{
    /// <summary>
    /// The proportion of the conversion. Must be greater than or equal to 0.
    /// </summary>
    public FloatParameter Amount { get; set; } = new FloatParameter(0, float.MaxValue, 3) { Min = 0, Max = 3};

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