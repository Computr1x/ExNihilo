using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using ExNihilo.Processors;

namespace ExNihilo.Extensions.Processors;

/// <summary>
/// Defines extensions that apply bulge effect on an <see cref="Image"/>
/// using Mutate/Clone.
/// </summary>
public static class BulgeProcessorExtension
{
    /// <summary>
    /// Applies bulge effect to the image.
    /// </summary>
    public static IImageProcessingContext Bulge(this IImageProcessingContext sourse, int x, int y, float radius, float strenght)
    {
        return sourse.ApplyProcessor(new BulgeProcessor(x, y, radius, strenght));
    }

    /// <summary>
    /// Applies bulge effect to the image.
    /// </summary>
    public static IImageProcessingContext Bulge(this IImageProcessingContext sourse, Rectangle rectangle, int x, int y, float radius, float strenght)
    {
        return sourse.ApplyProcessor(new BulgeProcessor(x, y, radius, strenght) { Area = rectangle });
    }
}
