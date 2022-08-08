using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Processors;

namespace TCG.Extensions.Processors;

/// <summary>
/// Defines extensions that apply perlin noise on an <see cref="Image"/>
/// using Mutate/Clone.
/// </summary>
public static class PerlinNoiseProcessorExtension
{
    /// <summary>
    /// Applies Perlin noise to the image.
    /// </summary>
    public static IImageProcessingContext PerlinNoise(this IImageProcessingContext sourse, int seed, int octaves, float persistence, bool monochrome)
    {
        return sourse.ApplyProcessor(new PerlinNoiseProcessor(seed) { Monochrome = monochrome, Octaves = octaves, Persistence = persistence });
    }

    /// <summary>
    /// Applies Perlin noise to the image.
    /// </summary>
    public static IImageProcessingContext PerlinNoise(this IImageProcessingContext sourse, Rectangle rectangle, int seed, int octaves, float persistence, bool monochrome)
    {
        return sourse.ApplyProcessor(new PerlinNoiseProcessor(seed) { Monochrome = monochrome, Octaves = octaves, Persistence = persistence, Area = rectangle });
    }
}
