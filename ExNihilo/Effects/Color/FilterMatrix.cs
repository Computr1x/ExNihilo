using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using ExNihilo.Base.Interfaces;

namespace ExNihilo.Effects;

/// <summary>
/// Defines effect that allow filter <see cref="Drawable"/> by the give color matrix
/// </summary>
public class FilterMatrix : Effect
{
    /// <summary>
    /// The filter color matrix
    /// </summary>
    public ColorMatrix Matrix { get; set; } = new ColorMatrix();

    /// <summary>
    /// <inheritdoc cref="FilterMatrix"/>
    /// </summary>
    public FilterMatrix() { }

    /// <summary>
    /// <inheritdoc cref="FilterMatrix"/>
    /// </summary>
    /// <param name="colorMatrix"><inheritdoc cref="Matrix" path="/summary"/></param>
    public FilterMatrix(ColorMatrix colorMatrix)
    {
        Matrix = colorMatrix;
    }

    /// <summary>
    /// Set Matrix value
    /// </summary>
    /// <param name="value"><inheritdoc cref="Matrix" path="/summary"/></param>
    public FilterMatrix WithColorMatrix(ColorMatrix value)
    {
        Matrix = value;
        return this;
    }

    public override void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Filter(Matrix));
}