using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using ExNihilo.Processors;

namespace ExNihilo.Extensions.Processors;

/// <summary>
/// Defines extensions that apply swirl effect on an <see cref="Image"/>
/// using Mutate/Clone.
/// </summary>
public static class SwirlProcessorExtension
{
    /// <summary>
    /// Applies swirl effect to the image.
    /// </summary>
    public static IImageProcessingContext Swirl(this IImageProcessingContext sourse, float radius)
    {
        return sourse.ApplyProcessor(new SwirlProcessor(radius));
    }
    /// <summary>
    /// Applies swirl effect to the image.
    /// </summary>
    public static IImageProcessingContext Swirl(this IImageProcessingContext sourse, float radius, float degree, float twists)
    {
        return sourse.ApplyProcessor(new SwirlProcessor(radius, degree, twists));
    }
    /// <summary>
    /// Applies swirl effect to the image.
    /// </summary>
    public static IImageProcessingContext Swirl(this IImageProcessingContext sourse, int x, int y, float radius, float degree, float twists)
    {
        return sourse.ApplyProcessor(new SwirlProcessor(x, y, radius, degree, twists));
    }

    /// <summary>
    /// Applies swirl effect to the image.
    /// </summary>
    public static IImageProcessingContext Swirl(this IImageProcessingContext sourse, Rectangle rectangle, int x, int y, float radius, float degree, float twists)
    {
        return sourse.ApplyProcessor(new SwirlProcessor(x, y, radius, degree, twists) { Area = rectangle });
    }

    /// <summary>
    /// Applies swirl effect to the image.
    /// </summary>
    public static IImageProcessingContext Swirl(this IImageProcessingContext sourse, Rectangle rectangle, float radius, float degree, float twists)
    {
        return sourse.ApplyProcessor(new SwirlProcessor(radius, degree, twists) { Area = rectangle });
    }
}
