using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Processors;

namespace TCG.Extensions.Processors;

public static class BulgeProcessorExtension
{
    /// <summary>
    /// Applies HSB correction to the image.
    /// </summary>
    public static IImageProcessingContext Bulge(this IImageProcessingContext sourse, int x, int y, float radius, float strenght)
    {
        return sourse.ApplyProcessor(new BulgeProcessor(x, y, radius, strenght));
    }

    /// <summary>
    /// Applies HSB correction to the image.
    /// </summary>
    public static IImageProcessingContext Bulge(this IImageProcessingContext sourse, Rectangle rectangle, int x, int y, float radius, float strenght)
    {
        return sourse.ApplyProcessor(new BulgeProcessor(x, y, radius, strenght) { Area = rectangle });
    }
}
