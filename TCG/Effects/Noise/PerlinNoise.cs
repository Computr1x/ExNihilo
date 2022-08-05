using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;
using TCG.Base.Parameters;

namespace TCG.Effects;

public class PerlinNoise : IEffect
{
    public IntParameter Seed { get; set; } = new(0) { Min = 0, Max = int.MaxValue };
    public IntParameter Octaves { get; set; } = new (10) { Value = 5 };
    public FloatParameter Persistence { get; set; } = new (1f) { Value = 0.5f };
    public BoolParameter Monochrome { get; set; } = new();

    public PerlinNoise() { }

    public PerlinNoise(int octaves, float persistence, bool isMonochrome) 
    { 
        Octaves.Value = octaves;
        Persistence.Value = persistence;
        Monochrome.Value = isMonochrome;
    }

    public PerlinNoise WithSeed(int value)
    {
        Seed.Value = value;
        return this;
    }

    public PerlinNoise WithRandomizedSeed(int min, int max)
    {
        Seed.Min = min;
        Seed.Max = max;
        return this;
    }

    public PerlinNoise WithOctaves(int value)
    {
        Octaves.Value = value;
        return this;
    }

    public PerlinNoise WithRandomizedOctaves(int min, int max)
    {
        Octaves.Min = min;
        Octaves.Max = max;
        return this;
    }

    public PerlinNoise WithPersistence(float value)
    {
        Persistence.Value = value;
        return this;
    }

    public PerlinNoise WithRandomizedPersistence(float min, float max)
    {
        Persistence.Min = min;
        Persistence.Max = max;
        return this;
    }

    public PerlinNoise IsMonochrome(bool value)
    {
        Monochrome.Value = value;
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.PerlinNoise(Seed, Octaves, Persistence, Monochrome));
}
