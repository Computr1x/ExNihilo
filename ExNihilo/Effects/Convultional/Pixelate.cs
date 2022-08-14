using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using ExNihilo.Base.Interfaces;
using ExNihilo.Base.Parameters;

namespace ExNihilo.Effects;

/// <summary>
/// Defines effect that allow the application of pixelatation on an <see cref="IDrawable"/>
/// </summary>
public class Pixelate : IEffect
{
    /// <summary>
    /// The size of the pixels. Must be greater then 1
    /// </summary>
    public IntParameter PixelSize { get; set; } = new(1, int.MaxValue, 4) { Min = 2, Max = 32 };

    /// <summary>
    /// <inheritdoc cref="Pixelate"/>
    /// </summary>
    public Pixelate() { }

    /// <summary>
    /// <inheritdoc cref="Pixelate"/>
    /// </summary>
    /// <param name="pixelSize"><inheritdoc cref="PixelSize" path="/summary"/></param>
    public Pixelate(int pixelSize)
    {
        PixelSize.Value = pixelSize;
    }

    /// <summary>
    /// Set PixelSize value
    /// </summary>
    /// <param name="value"><inheritdoc cref="PixelSize" path="/summary"/></param>
    public Pixelate WithPixelSize(int value)
    {
        PixelSize.Value = value;
        return this;
    }
    /// <summary>
    /// Set Pixel size randomization parameters.
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="PixelSize" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="PixelSize" path="/summary"/></param>
    public Pixelate WithRandomizedPixelSize(int min, int max)
    {
        PixelSize.Min = min;
        PixelSize.Max = max;
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Pixelate(PixelSize));
}
