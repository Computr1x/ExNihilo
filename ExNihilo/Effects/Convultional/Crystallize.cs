using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using ExNihilo.Base.Interfaces;
using ExNihilo.Extensions.Processors;
using ExNihilo.Base.Parameters;

namespace ExNihilo.Effects;

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

    /// <summary>
    /// <inheritdoc cref="Crystallize"/>
    /// </summary>
    public Crystallize() { }

    /// <summary>
    /// <inheritdoc cref="Crystallize"/>
    /// </summary>
    /// <param name="crystalsCount"><inheritdoc cref="CrystalsCount" path="/summary"/></param>
    public Crystallize(int crystalsCount)
    {
        CrystalsCount.Value = crystalsCount;
    }

    /// <summary>
    /// Set CrystalsCount value
    /// </summary>
    /// <param name="value"><inheritdoc cref="CrystalsCount" path="/summary"/></param>
    public Crystallize WithCrystalsCount(int value)
    {
        CrystalsCount.Value = value;
        return this;
    }
    /// <summary>
    /// Set Crystals count randomization parameters.
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="CrystalsCount" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="CrystalsCount" path="/summary"/></param>
    public Crystallize WithRandomizedCrystalsCount(int min, int max)
    {
        CrystalsCount.Min = min;
        CrystalsCount.Max = max;
        return this;
    }

    /// <summary>
    /// Set Seed value
    /// </summary>
    /// <param name="value"><inheritdoc cref="Seed" path="/summary"/></param>
    public Crystallize WithSeed(int value)
    {
        Seed.Value = value;
        return this;
    }
    /// <summary>
    /// Set Seed randomization parameters.
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="Seed" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="Seed" path="/summary"/></param>
    public Crystallize WithRandomizedSeed(int min, int max)
    {
        Seed.Min = min;
        Seed.Max = max;
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Crystallize(Seed, CrystalsCount));
}
