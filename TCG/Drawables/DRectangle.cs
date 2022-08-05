using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Abstract;
using TCG.Base.Utils;
using TCG.Base.Parameters;

namespace TCG.Drawables;

public class DRectangle : BaseDrawableWithBrushAndPen
{
    public RectangleParameter Rectangle { get; } = new RectangleParameter();

    public DRectangle() { }

    public DRectangle(Rectangle rectangle) 
    {
        Rectangle.Value = rectangle;
    }

    public DRectangle(int x, int y, int width, int height) : this(new SixLabors.ImageSharp.Rectangle(x, y, width, height))
    {
        
    }

    public DRectangle WithPoint(Point p)
    {
        Rectangle.Point.Value = p;
        return this;
    }

    public DRectangle WithRandomizedPoint(int minX, int maxX, int minY, int maxY)
    {
        Rectangle.Point.X.Min = minX;
        Rectangle.Point.X.Max = maxX;
        Rectangle.Point.Y.Min = minY;
        Rectangle.Point.Y.Max = maxY;
        return this;
    }

    public DRectangle WithSize(Size size)
    {
        Rectangle.Size.Value = size;
        return this;
    }

    public DRectangle WithRandomizedSize(int minWidth, int maxWidth, int minHeight, int maxHeight)
    {
        Rectangle.Size.Width.Min = minWidth;
        Rectangle.Size.Width.Max = maxWidth;
        Rectangle.Size.Height.Min = minHeight;
        Rectangle.Size.Height.Max = maxHeight;
        return this;
    }

    public override void Render(Image image, GraphicsOptions graphicsOptions)
    {
        SixLabors.ImageSharp.Rectangle rect = Rectangle;

        if (rect.Width <= 0 || rect.Height <= 0)
            return;

        IPath path = new RectangularPolygon(rect);
        DrawingOptions dopt = new() { GraphicsOptions = graphicsOptions };
        image.Mutate((x) =>
        {
            if (((DrawableType)Type).HasFlag(DrawableType.Filled))
                x.Fill(dopt, Brush.Value ?? Brush.DefaultValue, path);
            if (((DrawableType)Type).HasFlag(DrawableType.Outlined))
                x.Draw(dopt, Pen.Value ?? Pen.DefaultValue, path);
        });
    }
}
