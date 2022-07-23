using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Processors;

namespace TCG.Extensions.Processors;

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
