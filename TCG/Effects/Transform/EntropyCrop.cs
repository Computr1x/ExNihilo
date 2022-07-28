using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Rnd.Randomizers.Parameters;

namespace TCG.Effects;

public class EntropyCrop : IEffect
{
    public FloatParameter Threshold { get; set; } = new(0, 1f) { Value = 0.5f };

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.EntropyCrop(Threshold.Value));
}