using ExNihilo.Processors;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ExNihilo.Extensions.Processors;

/// <summary>
/// Defines extensions that apply bulge effect on an <see cref="Image"/>
/// using Mutate/Clone.
/// </summary>
public static class RippleProcessorExtension
{
    /// <summary>
    /// Applies ripple effect to the image.
    /// </summary>
    public static IImageProcessingContext Ripple(this IImageProcessingContext sourse)
    {
        return sourse.ApplyProcessor(new RippleProcessor());
    }
    /// <summary>
    /// Applies ripple effect to the image.
    /// </summary>
    public static IImageProcessingContext Ripple(this IImageProcessingContext sourse, int x, int y)
    {
        return sourse.ApplyProcessor(new RippleProcessor(x, y));
    }
    /// <summary>
    /// Applies ripple effect to the image.
    /// </summary>
    public static IImageProcessingContext Ripple(this IImageProcessingContext sourse, float radius, float waveLength)
    {
        return sourse.ApplyProcessor(new RippleProcessor(radius, waveLength));
    }
    /// <summary>
    /// Applies ripple effect to the image.
    /// </summary>
    public static IImageProcessingContext Ripple(this IImageProcessingContext sourse, float radius, float waveLength, float traintWidth)
    {
        return sourse.ApplyProcessor(new RippleProcessor(radius, waveLength, traintWidth));
    }

    /// <summary>
    /// Applies ripple effect to the image.
    /// </summary>
    public static IImageProcessingContext Ripple(this IImageProcessingContext sourse, int x, int y, float radius, float waveLength, float traintWidth)
    {
        return sourse.ApplyProcessor(new RippleProcessor(x, y, radius, waveLength, traintWidth));
    }

    /// <summary>
    /// Applies ripple effect to the image.
    /// </summary>
    public static IImageProcessingContext Ripple(this IImageProcessingContext sourse, Rectangle rectangle, int x, int y, float radius, float waveLength, float traintWidth)
    {
        return sourse.ApplyProcessor(new RippleProcessor(x, y, radius, waveLength, traintWidth) { Area = rectangle });
    }
}
