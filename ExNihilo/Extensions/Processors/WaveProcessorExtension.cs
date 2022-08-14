using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using ExNihilo.Processors;

namespace ExNihilo.Extensions.Processors;

/// <summary>
/// Defines extensions that apply wave effect on an <see cref="Image"/>
/// using Mutate/Clone.
/// </summary>
public static class WaveProcessorExtension
{
    /// <summary>
    /// Applies wave effect to the image.
    /// </summary>
    public static IImageProcessingContext Wave(this IImageProcessingContext sourse, float waveLength, float amplitude)
    {
        return sourse.ApplyProcessor(new WaveProcessor(waveLength, amplitude));
    }
    /// <summary>
    /// Applies wave effect to the image.
    /// </summary>
    public static IImageProcessingContext Wave(this IImageProcessingContext sourse, float waveLength, float amplitude, WaveType waveType)
    {
        return sourse.ApplyProcessor(new WaveProcessor(waveLength, amplitude, waveType));
    }

    /// <summary>
    /// Applies wave effect to the image.
    /// </summary>
    public static IImageProcessingContext Wave(this IImageProcessingContext sourse, Rectangle rectangle, float waveLength, float amplitude)
    {
        return sourse.ApplyProcessor(new WaveProcessor(waveLength, amplitude) { Area = rectangle });
    }

    /// <summary>
    /// Applies wave effect to the image.
    /// </summary>
    public static IImageProcessingContext Wave(this IImageProcessingContext sourse, Rectangle rectangle, float waveLength, float amplitude, WaveType waveType)
    {
        return sourse.ApplyProcessor(new WaveProcessor(waveLength, amplitude, waveType) { Area = rectangle });
    }
}
