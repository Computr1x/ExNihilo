using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Rnd.Randomizers.Parameters;

namespace TCG.Effects;

public class GaussianSharpen : IEffect
{
    public FloatParameter Sigma { get; set; } = new(5f) { Value = 0.5f };

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.GaussianSharpen(Sigma.Value));
}
