using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;

namespace TCG.Effects;

/// <summary>
/// Defines effect that allow to alter brightness component of the <see cref="IDrawable"/>
/// </summary>
public class Lightness : IEffect
{
    /// <summary>
    /// The proportion of the conversion. Must be greater than or equal to 0.
    /// </summary>
    public FloatParameter Amount { get; set; } = new(1) { Min = 0, Max = 3};

    public Lightness() { }

    public Lightness(float amount)
    {
        Amount.Value = amount;
    }

    public Lightness WithAmount(float value)
    {
        Amount.Value = value;
        return this;
    }

    public Lightness WithRandomizedAmount(float min, float max)
    {
        Amount.Min = min;
        Amount.Max = max;
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Lightness(Amount));
}