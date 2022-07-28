using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;
using TCG.Rnd.Randomizers.Parameters;

namespace TCG.Effects;

public class PerlinNoise : IEffect
{
    public IntParameter Seed { get; set; } = new(0);
    public IntParameter Octaves { get; set; } = new (10) { Value = 5 };
    public FloatParameter Persistence { get; set; } = new (1f) { Value = 0.5f };
    public BoolParameter Monochrome { get; set; } = new() { Value = false };

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.PerlinNoise(Seed.Value, Octaves.Value, Persistence.Value, Monochrome.Value));
}
