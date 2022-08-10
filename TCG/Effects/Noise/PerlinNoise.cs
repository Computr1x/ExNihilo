using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;
using TCG.Base.Parameters;

namespace TCG.Effects;

/// <summary>
/// Defines effect that allow the application of perlin noise on an <see cref="IDrawable"/>
/// </summary>
public class PerlinNoise : IEffect
{
    /// <summary>
    /// Seed for noise randomizer.
    /// </summary>
    public IntParameter Seed { get; set; } = new(0) { Min = 0, Max = int.MaxValue };
    /// <summary>
    /// The number of octaves control the amount of detail of Perlin noise. 
    /// Adding more octaves increases the detail of Perlin noise, with the added drawback of increasing the calculation time.
    /// Must be equal or greater then 1.
    /// </summary>
    public IntParameter Octaves { get; set; } = new (1, int.MaxValue, 10) { Value = 5 };
    /// <summary>
    /// A multiplier (0-1) that determines how quickly the amplitudes diminish for each successive octave in a Perlin-noise function.
    /// </summary>
    public FloatParameter Persistence { get; set; } = new (0, 1, 1f) { Value = 0.5f };
    /// <summary>
    /// Define is noise monochrome or not.
    /// </summary>
    public BoolParameter Monochrome { get; set; } = new();

    /// <summary>
    /// <inheritdoc cref="PerlinNoise"/>
    /// </summary>
    public PerlinNoise() { }

    /// <summary>
    /// <inheritdoc cref="PerlinNoise"/>
    /// </summary>
    /// <param name="octaves"><inheritdoc cref="Octaves" path="/summary"/></param>
    /// <param name="persistence"><inheritdoc cref="Persistence" path="/summary"/></param>
    /// <param name="isMonochrome"><inheritdoc cref="Monochrome" path="/summary"/></param>
    public PerlinNoise(int octaves, float persistence, bool isMonochrome) 
    { 
        Octaves.Value = octaves;
        Persistence.Value = persistence;
        Monochrome.Value = isMonochrome;
    }

    /// <summary>
    /// Set Seed value
    /// </summary>
    /// <param name="value"><inheritdoc cref="Seed" path="/summary"/></param>
    public PerlinNoise WithSeed(int value)
    {
        Seed.Value = value;
        return this;
    }
    /// <summary>
    /// Set Seed randomization parameters.
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="Seed" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="Seed" path="/summary"/></param>
    public PerlinNoise WithRandomizedSeed(int min, int max)
    {
        Seed.WithRandomizedValue(min, max);
        return this;
    }

    /// <summary>
    /// Set noise Octaves value
    /// </summary>
    /// <param name="value"><inheritdoc cref="Octaves" path="/summary"/></param>
    public PerlinNoise WithOctaves(int value)
    {
        Octaves.Value = value;
        return this;
    }
    /// <summary>
    /// Set noise Ocataves randomization parameters.
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="Octaves" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="Octaves" path="/summary"/></param>
    public PerlinNoise WithRandomizedOctaves(int min, int max)
    {
        Octaves.WithRandomizedValue(min, max);
        return this;
    }

    /// <summary>
    /// Set Persistence value
    /// </summary>
    /// <param name="value"><inheritdoc cref="Persistence" path="/summary"/></param>
    public PerlinNoise WithPersistence(float value)
    {
        Persistence.Value = value;
        return this;
    }
    /// <summary>
    /// Set noise Persistense randomization parameters.
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="Persistence" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="Persistence" path="/summary"/></param>
    public PerlinNoise WithRandomizedPersistence(float min, float max)
    {
        Persistence.WithRandomizedValue(min, max);
        return this;
    }

    /// <summary>
    /// Set Monochrome value
    /// </summary>
    /// <param name="value"><inheritdoc cref="Monochrome" path="/summary"/></param>
    public PerlinNoise IsMonochrome(bool value)
    {
        Monochrome.Value = value;
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.PerlinNoise(Seed, Octaves, Persistence, Monochrome));
}
