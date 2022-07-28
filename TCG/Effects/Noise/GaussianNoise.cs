using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;
using TCG.Rnd.Randomizers.Parameters;

namespace TCG.Effects;

public class GaussianNoise : IEffect
{
    public IntParameter Seed { get; set; } = new(0) { Min = 0, Max = 10 };
    public ByteParameter Amount { get; set; } = new (byte.MaxValue) { Min = 0, Max = byte.MaxValue };
    public BoolParameter Monochrome { get; set; } = new();

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.GaussianNoise(Seed, Amount, Monochrome));
}
