using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;

namespace TCG.Effects;

/// <summary>
/// Defines effect that allow filter <see cref="IDrawable"/> by the give color matrix
/// </summary>
public class FilterMatrix : IEffect
{
    /// <summary>
    /// The filter color matrix
    /// </summary>
    public ColorMatrix Matrix { get; set; } = new ColorMatrix();

    public FilterMatrix() { }

    public FilterMatrix(ColorMatrix colorMatrix)
    {
        Matrix = colorMatrix;
    }

    public FilterMatrix WithColorMatrix(ColorMatrix value)
    {
        Matrix = value;
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Filter(Matrix));
}