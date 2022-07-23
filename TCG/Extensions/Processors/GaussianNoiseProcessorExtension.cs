using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Processors;

namespace TCG.Extensions.Processors;

public static class GaussianNoiseProcessorExtension
{
    /// <summary>
    /// Applies Gaussian noise to the image.
    /// </summary>
    public static IImageProcessingContext GaussianNoise(this IImageProcessingContext sourse, int seed, byte amount, bool monochrome)
    {
        return sourse.ApplyProcessor(new GaussianNoiseProcessor(seed, amount) { Monochrome = monochrome });
    }

    /// <summary>
    /// Applies Gaussian noise to the image.
    /// </summary>
    public static IImageProcessingContext GaussianNoise(this IImageProcessingContext sourse, Rectangle rectangle, int seed, byte amount, bool monochrome)
    {
        return sourse.ApplyProcessor(new GaussianNoiseProcessor(seed, amount) { Area = rectangle, Monochrome = monochrome });
    }
}
