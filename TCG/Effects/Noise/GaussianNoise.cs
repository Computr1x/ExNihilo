using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;
using TCG.Extensions.Processors;

namespace TCG.Effects;

/// <summary>
/// Defines effect that allow the application of gaussian noise on an <see cref="IDrawable"/>
/// </summary>
public class GaussianNoise : IEffect
{
    /// <summary>
    /// Seed for noise randomizer.
    /// </summary>
    public IntParameter Seed { get; set; } = new(0) { Min = 0, Max = 10 };
    /// <summary>
    /// Amount of noise (0-255).
    /// </summary>
    public ByteParameter Amount { get; set; } = new(0, byte.MaxValue, byte.MaxValue) { Min = 0, Max = byte.MaxValue };
    /// <summary>
    /// Define is noise monochrome or not.
    /// </summary>
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
