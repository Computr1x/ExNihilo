using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;
using TCG.Rnd.Randomizers.Parameters;

namespace TCG.Effects;

public class Crystallize : IEffect
{
    public IntParameter CrystalsCount { get; set; } = new(100) { Value = 64 };
    public IntParameter Seed { get; set; } = new(0);

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Crystallize(Seed.Value, CrystalsCount.Value));
}
