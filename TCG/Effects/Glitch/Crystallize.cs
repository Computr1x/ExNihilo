using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;
using TCG.Rnd.Randomizers.Parameters;

namespace TCG.Effects;

public class Crystallize : IEffect
{
    public IntParameter CrystalsCount { get; set; } = new(64) { Min = 16, Max = 128 };
    public IntParameter Seed { get; set; } = new(0) { Min = 0, Max = int.MaxValue };

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Crystallize(Seed, CrystalsCount));
}
