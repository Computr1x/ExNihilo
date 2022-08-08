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

    public Crop() { }

    public Crop(Point point, Size size)
    {
        Area.Point.Value = point;
        Area.Size.Value = size;
    }

    public Crop WithPoint(Point p)
    {
        Area.Point.Value = p;
        return this;
    }

    public Crop WithRandomizedPoint(int minX, int maxX, int minY, int maxY)
    {
        Area.WithRandomizedPoint(minX, maxX, minY, maxY);
        return this;
    }

    public Crop WithSize(Size size)
    {
        Area.WithSize(size);
        return this;
    }

    public Crop WithRandomizedSize(int minWidth, int maxWidth, int minHeight, int maxHeight)
    {
        Area.WithRandomizedSize(minWidth, maxWidth, minHeight, maxHeight);
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Crop(Area));
}