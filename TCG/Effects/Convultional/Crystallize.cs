using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;
using TCG.Base.Parameters;

namespace TCG.Effects;

/// <summary>
/// Defines effect that allow the application of crystallization on an <see cref="IDrawable"/>
/// </summary>
public class Crystallize : IEffect
{
    /// <summary>
    /// The number of crystals into which the image will be divided. Must be gretear then 1
    /// </summary>
    public IntParameter CrystalsCount { get; set; } = new(1, int.MaxValue, 64) { Min = 16, Max = 128 };
    /// <summary>
    /// Randomiztion seed value
    /// </summary>
    public IntParameter Seed { get; set; } = new(0) { Min = 0, Max = int.MaxValue };

    public Crystallize() { }

    public Crystallize(int crystalsCount)
    {
        CrystalsCount.Value = crystalsCount;
    }

    public Crystallize WithCrystalsCount(int value)
    {
        CrystalsCount.Value = value;
        return this;
    }

    public Crystallize WithRandomizedCrystalsCount(int min, int max)
    {
        CrystalsCount.Min = min;
        CrystalsCount.Max = max;
        return this;
    }

    public Crystallize WithSeed(int value)
    {
        Seed.Value = value;
        return this;
    }

    public Crystallize WithRandomizedSeed(int min, int max)
    {
        Seed.Min = min;
        Seed.Max = max;
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Crystallize(Seed, CrystalsCount));
}
