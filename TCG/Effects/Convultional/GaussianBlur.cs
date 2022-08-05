using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;

namespace TCG.Effects;

public class GaussianBlur : IEffect
{
    public FloatParameter Sigma { get; set; } = new(0.5f) { Min = 0, Max = 1};

    public GaussianBlur() { }

    public GaussianBlur(float sigma)
    {
        Sigma.Value = sigma;
    }

    public GaussianBlur WithSigma(float value)
    {
        Sigma.Value = value;
        return this;
    }

    public GaussianBlur WithRandomizedAmount(float min, float max)
    {
        Sigma.Min = min;
        Sigma.Max = max;
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
            image.Mutate(x => x.GaussianBlur(Sigma));
}
