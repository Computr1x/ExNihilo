using ExNihilo.Processors;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ExNihilo.Extensions.Processors;

/// <summary>
/// Defines extensions that apply gaussian noise on an <see cref="Image"/>
/// using Mutate/Clone.
/// </summary>
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
