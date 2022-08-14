using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;

namespace TCG.Effects;

/// <summary>
/// Defines effect that allow the application of cropping operations on an <see cref="IDrawable"/>
/// </summary>
public class Crop : IEffect
{
    /// <summary>
    /// <see cref="Rectangle"/> structure that specifies the portion of the image object to retain.
    /// </summary>
    public RectangleParameter Area { get; set; } = new();

    /// <summary>
    /// <inheritdoc cref="Crop"/>
    /// </summary>
    public Crop() { }

    /// <summary>
    /// <inheritdoc cref="Crop"/>
    /// </summary>
    /// <param name="point"><inheritdoc cref="Point" path="/summary"/></param>
    /// <param name="size"><inheritdoc cref="Size" path="/summary"/></param>
    public Crop(Point point, Size size)
    {
        Area.Point.WithValue(point);
        Area.Size.WithValue(size);
    }

    /// <summary>
    /// Set Area point value
    /// </summary>
    /// <param name="value"><inheritdoc cref="Point" path="/summary"/></param>
    public Crop WithPoint(Point p)
    {
        Area.Point.WithValue(p);
        return this;
    }
    /// <summary>
    /// Set Crop randomization parameters.
    /// </summary>
    /// <param name="minX">Minimal randomization value of x asix.</param>
    /// <param name="maxX">Maximum randomization value of x asix.</param>
    /// <param name="minY">Minimal randomization value of y asix.</param>
    /// <param name="maxY">Maximum randomization value of y asix.</param>
    public Crop WithRandomizedPoint(int minX, int maxX, int minY, int maxY)
    {
        Area.WithRandomizedPoint(minX, maxX, minY, maxY);
        return this;
    }

    /// <summary>
    /// Set Area size value
    /// </summary>
    /// <param name="value"><inheritdoc cref="Size" path="/summary"/></param>
    public Crop WithSize(Size size)
    {
        Area.WithSize(size);
        return this;
    }

    /// <summary>
    /// Set Size randomization parameters.
    /// </summary>
    /// <param name="minWidth">Minimal randomization value of width.</param>
    /// <param name="maxWidth">Maximum randomization value of width.</param>
    /// <param name="minHeight">Minimal randomization value of height.</param>
    /// <param name="maxHeight">Maximum randomization value of height.</param>
    public Crop WithRandomizedSize(int minWidth, int maxWidth, int minHeight, int maxHeight)
    {
        Area.WithRandomizedSize(minWidth, maxWidth, minHeight, maxHeight);
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Crop(Area));
}