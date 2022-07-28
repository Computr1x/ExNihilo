using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;
using TCG.Rnd.Randomizers.Parameters;

namespace TCG.Effects;

public class GaussianNoise : IEffect
{
    public IntParameter Seed { get; set; } = new(0);
    public ByteParameter Amount { get; set; } = new (byte.MaxValue) { Value = byte.MaxValue };
    public BoolParameter Monochrome { get; set; } = new() { Value = false };

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.GaussianNoise(Seed.Value, Amount.Value, Monochrome.Value));
}
