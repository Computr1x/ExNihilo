using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;

namespace TCG.Effects;

/// <summary>
/// Defines effect that allow the application of quantinization on an <see cref="IDrawable"/>
/// </summary>
public class GaussianSharpen : IEffect
{
    /// <summary>
    /// The 'sigma' (0.0-1.0) value representing the weight of the blur.
    /// </summary>
    public FloatParameter Sigma { get; set; } = new(0, 1, 0.5f) { Min = 0, Max = 1 };

    public GaussianSharpen() { }

    public GaussianSharpen(float sigma)
    {
        Sigma.Value = sigma;
    }

    public GaussianSharpen WithSigma(float value)
    {
        Sigma.Value = value;
        return this;
    }

    public GaussianSharpen WithRandomizedAmount(float min, float max)
    {
        Sigma.Min = min;
        Sigma.Max = max;
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.GaussianSharpen(Sigma));
}
