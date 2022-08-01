using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;

namespace TCG.Effects;

public class GaussianBlur : IEffect
{
    public FloatParameter Sigma { get; set; } = new(0.5f) { Min = 0, Max = 1};

public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.GaussianBlur(Sigma));
}
