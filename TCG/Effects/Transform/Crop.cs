using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;

namespace TCG.Effects;

public class Crop : IEffect
{
    public RectangleParameter Rectangle { get; set; } = new();

    public Crop() { }

    public Crop(Point point, Size size)
    {
        Rectangle.Point.Value = point;
        Rectangle.Size.Value = size;
    }

    public Crop WithPoint(Point p)
    {
        Rectangle.Point.Value = p;
        return this;
    }

    public Crop WithRandomizedPoint(int minX, int maxX, int minY, int maxY)
    {
        Rectangle.Point.X.Min = minX;
        Rectangle.Point.X.Max = maxX;
        Rectangle.Point.Y.Min = minY;
        Rectangle.Point.Y.Min = maxY;
        return this;
    }

    public Crop WithSize(Size size)
    {
        Rectangle.Size.Value = size;
        return this;
    }

    public Crop WithRandomizedSize(int minWidth, int maxWidth, int minHeight, int maxHeight)
    {
        Rectangle.Size.Width.Min = minWidth;
        Rectangle.Size.Width.Max = maxWidth;
        Rectangle.Size.Height.Min = minHeight;
        Rectangle.Size.Height.Max = maxHeight;
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Crop(Rectangle));
}