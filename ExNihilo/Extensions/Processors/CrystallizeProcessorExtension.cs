using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using ExNihilo.Processors;

namespace ExNihilo.Extensions.Processors;

/// <summary>
/// Defines extensions that apply crystallization effect on an <see cref="Image"/>
/// using Mutate/Clone.
/// </summary>
public static class CrystallizeProcessorExtension
{
    /// <summary>
    /// Applies crystallize effect to the image.
    /// </summary>
    public static IImageProcessingContext Crystallize(this IImageProcessingContext sourse, int seed, int crystallsCount)
    {
        return sourse.ApplyProcessor(new CrystallizeProcessor(seed) { CrystalsCount = crystallsCount });
    }

    /// <summary>
    /// Applies crystallize effect to the image.
    /// </summary>
    public static IImageProcessingContext Crystallize(this IImageProcessingContext sourse, Rectangle rectangle, int seed, int crystallsCount)
    {
        return sourse.ApplyProcessor(new CrystallizeProcessor(seed) { Area = rectangle, CrystalsCount = crystallsCount });
    }
}
