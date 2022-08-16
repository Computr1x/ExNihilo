using ExNihilo.Processors;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ExNihilo.Extensions.Processors;

/// <summary>
/// Defines extensions that apply shift of red, green and blue channel on an <see cref="Image"/>
/// using Mutate/Clone.
/// </summary>
public static class RGBShiftProcessorExtensions
{
    /// <summary>
    /// Applies rgb shift to the image.
    /// </summary>
    /// <param name="source">The image this method extends.</param>
    /// <returns>The <see cref="IImageProcessingContext"/> to allow chaining of operations.</returns>
    public static IImageProcessingContext RGBShift(this IImageProcessingContext source)
    {
        return source.ApplyProcessor(new RGBShiftProcessor(3));
    }

    /// <summary>
    /// Applies rgb shift to the image.
    /// </summary>
    /// <param name="source">The image this method extends.</param>
    /// <param name="offset">The 'offset' value representing the amount of channels shift value in pixels.</param>
    /// <returns>The <see cref="IImageProcessingContext"/> to allow chaining of operations.</returns>
    public static IImageProcessingContext RGBShift(this IImageProcessingContext source, int offset)
    {
        return source.ApplyProcessor(new RGBShiftProcessor(offset));
    }

    /// <summary>
    /// Applies rgb shift to the image.
    /// </summary>
    /// <param name="source">The image this method extends.</param>
    /// <param name="offset">The 'offset' value representing the amount of channels shift value in pixels.</param>
    /// <param name="rectangle">
    /// The <see cref="Rectangle"/> structure that specifies the portion of the image object to alter.
    /// </param>
    /// <returns>The <see cref="IImageProcessingContext"/> to allow chaining of operations.</returns>
    public static IImageProcessingContext RGBShift(
        this IImageProcessingContext source,
        int offset,
        Rectangle rectangle
    ) {
        return source.ApplyProcessor(new RGBShiftProcessor(offset) { Area = rectangle });
    }

    /// <summary>
    /// Applies rgb shift to the image by channels.
    /// </summary>
    /// <param name="source">The image this method extends.</param>
    /// <param name="redXOffset">The 'redXOffset' value representing the amount of channels shift of red channel by x coordinate in pixels.</param>
    /// <param name="redYOffset">The 'redYOffset' value representing the amount of channels shift of red channel by y coordinate in pixels.</param>
    /// <param name="blueXOffset">The 'blueXOffset' value representing the amount of channels shift of blue channel by x coordinate in pixels.</param>
    /// <param name="blueYOffset">The 'blueYOffset' value representing the amount of channels shift of blue channel by y coordinate in pixels.</param>
    /// <param name="greenXOffset">The 'greenXOffset' value representing the amount of channels shift of green channel by x coordinate in pixels.</param>
    /// <param name="greenYOffset">The 'greenYOffset' value representing the amount of channels shift of green channel by y coordinate in pixels.</param>
    /// <returns>The <see cref="IImageProcessingContext"/> to allow chaining of operations.</returns>
    public static IImageProcessingContext RgbShift(
        this IImageProcessingContext source,
        int redXOffset,
        int redYOffset,
        int blueXOffset,
        int blueYOffset,
        int greenXOffset,
        int greenYOffset
    ) {
        return source.ApplyProcessor(new RGBShiftProcessor(blueXOffset, blueYOffset, greenXOffset, greenYOffset, redXOffset, redYOffset));
    }

    /// <summary>
    /// Applies rgb shift to the image by channels.
    /// </summary>
    /// <param name="source">The image this method extends.</param>
    /// <param name="redXOffset">The 'redXOffset' value representing the amount of channels shift of red channel by x coordinate in pixels.</param>
    /// <param name="redYOffset">The 'redYOffset' value representing the amount of channels shift of red channel by y coordinate in pixels.</param>
    /// <param name="blueXOffset">The 'blueXOffset' value representing the amount of channels shift of blue channel by x coordinate in pixels.</param>
    /// <param name="blueYOffset">The 'blueYOffset' value representing the amount of channels shift of blue channel by y coordinate in pixels.</param>
    /// <param name="greenXOffset">The 'greenXOffset' value representing the amount of channels shift of green channel by x coordinate in pixels.</param>
    /// <param name="greenYOffset">The 'greenYOffset' value representing the amount of channels shift of green channel by y coordinate in pixels.</param>
    /// <param name="rectangle">The <see cref="Rectangle"/> structure that specifies the portion of the image object to alter.</param>
    /// <returns>The <see cref="IImageProcessingContext"/> to allow chaining of operations.</returns>
    public static IImageProcessingContext RGBShift(
        this IImageProcessingContext source,
        Rectangle rectangle,
        int redXOffset,
        int redYOffset,
        int blueXOffset,
        int blueYOffset,
        int greenXOffset,
        int greenYOffset
    ) {
        return source.ApplyProcessor(new RGBShiftProcessor(blueXOffset, blueYOffset, greenXOffset, greenYOffset, redXOffset, redYOffset) { Area = rectangle });
    }
}
