using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Rnd.Randomizers.Parameters;

namespace TCG.Effects;

public class GaussianSharpen : IEffect
{
    public FloatParameter Sigma { get; set; } = new(0.5f) { Min = 0, Max = 1 };

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.GaussianSharpen(Sigma));
}
