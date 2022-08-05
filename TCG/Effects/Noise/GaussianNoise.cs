using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;
using TCG.Extensions.Processors;

namespace TCG.Effects;

public class GaussianNoise : IEffect
{
    public IntParameter Seed { get; set; } = new(0) { Min = 0, Max = 10 };
    public ByteParameter Amount { get; set; } = new(byte.MaxValue) { Min = 0, Max = byte.MaxValue };
    public BoolParameter Monochrome { get; set; } = new();

    public GaussianNoise() { }
    public GaussianNoise(byte amount, bool isMonochome)
    {
        Amount.Value = amount;
        Monochrome.Value = isMonochome;
    }

    public GaussianNoise WithSeed(int value)
    {
        Seed.Value = value;
        return this;
    }

    public GaussianNoise WithRandomizedSeed(int min, int max)
    {
        Seed.Min = min;
        Seed.Max = max;
        return this;
    }

    public GaussianNoise WithAmount(byte value)
    {
        Amount.Value = value;
        return this;
    }

    public GaussianNoise WithRandomizedAmount(byte min, byte max)
    {
        Amount.Min = min;
        Amount.Max = max;
        return this;
    }

    public GaussianNoise IsMonochrome(bool value)
    {
        Monochrome.Value = value;
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions)
    {
        image.Mutate(x => { x.GaussianNoise(Seed, Amount, Monochrome); });
    }
}
