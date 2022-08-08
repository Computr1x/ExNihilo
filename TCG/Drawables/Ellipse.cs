using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Abstract;
using TCG.Base.Utils;
using TCG.Base.Parameters;

namespace TCG.Drawables;

/// <summary>
/// Define ellipse drawable object.
/// </summary>
public class Ellipse : BaseDrawableWithBrushAndPen
{
    /// <summary>
    /// Represent rectangular area where ellipse will be drawn.
    /// </summary>
    public RectangleParameter Area { get; } = new RectangleParameter();

    public Ellipse() { }

    public Ellipse(SixLabors.ImageSharp.Rectangle rectangle)
    {
        Area.Value = rectangle;
    }

    public Ellipse(int x, int y, int width, int height) : this(new SixLabors.ImageSharp.Rectangle(x, y, width, height)) { }


    public Ellipse WithBrush(IBrush brush)
    {
        Brush.Value = brush;
        return this;
    }
    public Ellipse WithBrush(Action<BrushParameter> actionBrush)
    {
        actionBrush(Brush);
        return this;
    }

    public Ellipse WithPen(IPen pen)
    {
        Pen.Value = pen;
        return this;
    }

    public Ellipse WithPen(Action<PenParameter> actionPen)
    {
        actionPen(Pen);
        return this;
    }

    public Ellipse WithType(DrawableType value)
    {
        Type.Value = value;
        return this;
    }

    public Ellipse WithPoint(Point p)
    {
        Area.WithPoint(p);
        return this;
    }

    public Ellipse WithRandomizedPoint(int minX, int maxX, int minY, int maxY)
    {
        Area.WithRandomizedPoint(minX, maxX, minY, maxY);
        return this;
    }

    public Ellipse WithSize(Size size)
    {
        Area.WithSize(size);
        return this;
    }

    public Ellipse WithRandomizedSize(int minWidth, int maxWidth, int minHeight, int maxHeight)
    {
        Area.WithRandomizedSize(minWidth, maxWidth, minHeight, maxHeight);
        return this;
    }

    public Ellipse WithRandomizedSize(int min, int max)
    {
        Area.WithRandomizedSize(min, max, min, max);
        return this;
    }

    public override void Render(Image image, GraphicsOptions graphicsOptions)
    {
        SixLabors.ImageSharp.Rectangle rect = Area.Value ?? new SixLabors.ImageSharp.Rectangle(Area.Point, Area.Size); 

        if (rect.Width <= 0 || rect.Height <= 0)
            return;

        IPath path = new EllipsePolygon(new PointF(rect.X, rect.Y), rect.Size);
        DrawingOptions dopt = new () { GraphicsOptions = graphicsOptions };

        image.Mutate((x) =>
        {
            if (((DrawableType)Type).HasFlag(DrawableType.Filled))
                x.Fill(dopt, (Brush.Value ?? Brush.DefaultValue), path);
            if (((DrawableType)Type).HasFlag(DrawableType.Outlined))
                x.Draw(dopt, (Pen.Value ?? Pen.DefaultValue), path);
        });
    }
}
