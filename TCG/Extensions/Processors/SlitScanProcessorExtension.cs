using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Processors;

namespace TCG.Extensions.Processors;

public static class SlitScanProcessorExtension
{
    /// <summary>
    /// Applies slitscan effect to the image.
    /// </summary>
    public static IImageProcessingContext SlitScan(this IImageProcessingContext sourse)
    {
        return sourse.ApplyProcessor(new SlitScanProcessor());
    }

    /// <summary>
    /// Applies slitscan effect to the image.
    /// </summary>
    public static IImageProcessingContext SlitScan(this IImageProcessingContext sourse, float time)
    {
        return sourse.ApplyProcessor(new SlitScanProcessor(time));
    }

    /// <summary>
    /// Applies slitscan effect to the image.
    /// </summary>
    public static IImageProcessingContext SlitScan(this IImageProcessingContext sourse, Rectangle rectangle, float time)
    {
        return sourse.ApplyProcessor(new SlitScanProcessor(time) { Area = rectangle });
    }
}
