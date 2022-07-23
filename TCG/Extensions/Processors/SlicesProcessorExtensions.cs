using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Processors;

namespace TCG.Extensions.Processors;

public static class SlicesProcessorExtensions
{
    /// <summary>
    /// Applies slices effect to the image.
    /// </summary>
    public static IImageProcessingContext Slices(this IImageProcessingContext sourse, int seed, int countSlices, int sliceHeight, int minOffset, int maxOffset)
    {
        return sourse.ApplyProcessor(new SlicesProcessor(seed) { Count = countSlices, SliceHeight = sliceHeight, MaxOffset = maxOffset, MinOffset = minOffset });
    }

    /// <summary>
    /// Applies slices effect to the image.
    /// </summary>
    public static IImageProcessingContext Slices(this IImageProcessingContext sourse, Rectangle rectangle, int seed, int countSlices, int sliceHeight, int minOffset, int maxOffset)
    {
        return sourse.ApplyProcessor(new SlicesProcessor(seed) { Area = rectangle, Count = countSlices, SliceHeight = sliceHeight, MaxOffset = maxOffset, MinOffset = minOffset });
    }
}
