using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Abstract;
using TCG.Base.Utils;
using TCG.Base.Parameters;

namespace TCG.Drawables;

/// <summary>
/// Define rectangle drawable object.
/// </summary>
public class Rectangle : BaseDrawableWithBrushAndPen
{
    /// <summary>
    /// Represent rectangular area where object will be drawn.
    /// </summary>
    public RectangleParameter Area { get; } = new RectangleParameter();

    public Rectangle() { }

    public Rectangle(SixLabors.ImageSharp.Rectangle rectangle) 
    {
        Area.Value = rectangle;
    }

    public Rectangle(int x, int y, int width, int height) : this(new SixLabors.ImageSharp.Rectangle(x, y, width, height))
    {
        
    }

    public Rectangle WithBrush(IBrush brush)
    {
        Brush.Value = brush;
        return this;
    }
    public Rectangle WithBrush(Action<BrushParameter> actionBrush)
    {
        actionBrush(Brush);
        return this;
    }

    public Rectangle WithPen(IPen pen)
    {
        Pen.Value = pen;
        return this;
    }

    public Rectangle WithPen(Action<PenParameter> actionPen)
    {
        actionPen(Pen);
        return this;
    }

    public Rectangle WithType(DrawableType value)
    {
        Type.Value = value;
        return this;
    }

    public Rectangle WithPoint(Point p)
    {
        Area.WithPoint(p);
        return this;
    }

    public Rectangle WithRandomizedPoint(int minX, int maxX, int minY, int maxY)
    {
        Area.WithRandomizedPoint(minX, maxX, minY, maxY);
        return this;
    }

    public Rectangle WithSize(Size size)
    {
        Area.WithSize(size);
        return this;
    }

    public Rectangle WithRandomizedSize(int minWidth, int maxWidth, int minHeight, int maxHeight)
    {
        Area.WithRandomizedSize(minWidth, maxWidth, minHeight, maxHeight);
        return this;
    }

    public Rectangle WithRandomizedSize(int min, int max)
    {
        Area.WithRandomizedSize(min, max, min, max);
        return this;
    }

    public override void Render(Image image, GraphicsOptions graphicsOptions)
    {
        SixLabors.ImageSharp.Rectangle rect = Area.Value ?? new SixLabors.ImageSharp.Rectangle(Area.Point, Area.Size);

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
