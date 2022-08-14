using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using ExNihilo.Processors;

namespace ExNihilo.Extensions.Processors;

/// <summary>
/// Defines extensions that translate Euclidean coordinates to Polar coordinats and vise versa on an <see cref="Image"/>
/// using Mutate/Clone.
/// </summary>
public static class PolarCoordinatesProcessorExtension
{
    /// <summary>
    /// Transform image coordinates from cartesian to polar.
    /// </summary>
    public static IImageProcessingContext PolarCoordinates(this IImageProcessingContext sourse)
    {
        return sourse.ApplyProcessor(new PolarCoordinatesProcessor());
    }

    /// <summary>
    /// Transform image coordinates from cartesian to polar and vice versa.
    /// </summary>
    public static IImageProcessingContext PolarCoordinates(this IImageProcessingContext sourse, PolarConversionType conversionType)
    {
        return sourse.ApplyProcessor(new PolarCoordinatesProcessor() { PolarType = conversionType });
    }

    /// <summary>
    /// Transform area coordinates from cartesian to polar and vice versa.
    /// </summary>
    public static IImageProcessingContext PolarCoordinates(this IImageProcessingContext sourse, Rectangle rectangle, PolarConversionType conversionType)
    {
        return sourse.ApplyProcessor(new PolarCoordinatesProcessor() { Area = rectangle, PolarType = conversionType });
    }
}
