using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Processors;

namespace TCG.Extensions.Processors;

public static class HSBCorrectionProcessorExtenstion
{
    /// <summary>
    /// Applies HSB correction to the image.
    /// </summary>
    /// <param name="source">The image this method extends.</param>
    /// <param name="hue">The "attribute of a visual sensation according to which an area appears to be similar to one of the perceived colors: red, yellow, green, and blue, or to a combination of two of them"</param>
    /// <param name="saturation">The "colorfulness of a stimulus relative to its own brightness"</param>
    /// <param name="brightness">The "attribute of a visual sensation according to which an area appears to emit more or less light"</param>
    /// <returns>The <see cref="IImageProcessingContext"/> to allow chaining of operations.</returns>
    public static IImageProcessingContext HSBCorrection(this IImageProcessingContext sourse, sbyte hue = 0, sbyte saturation = 0, sbyte brightness = 0)
    {
        return sourse.ApplyProcessor(new HSBCorrectionProcessor(hue, saturation, brightness));
    }

    /// <summary>
    /// Applies HSB correction to the image.
    /// </summary>
    /// <param name="source">The image this method extends.</param>
    /// <param name="rectangle">
    /// The <see cref="Rectangle"/> structure that specifies the portion of the image object to alter.
    /// </param>
    /// <param name="hue">The "attribute of a visual sensation according to which an area appears to be similar to one of the perceived colors: red, yellow, green, and blue, or to a combination of two of them"</param>
    /// <param name="saturation">The "colorfulness of a stimulus relative to its own brightness"</param>
    /// <param name="brightness">The "attribute of a visual sensation according to which an area appears to emit more or less light"</param>
    /// <returns>The <see cref="IImageProcessingContext"/> to allow chaining of operations.</returns>
    public static IImageProcessingContext HSBCorrection(this IImageProcessingContext sourse, Rectangle rectangle, sbyte hue = 0, sbyte saturation = 0, sbyte brightness = 0)
    {
        return sourse.ApplyProcessor(new HSBCorrectionProcessor(hue, saturation, brightness) { Area = rectangle });
    }
}
