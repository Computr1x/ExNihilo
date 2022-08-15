using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using ExNihilo.Base.Abstract;
using ExNihilo.Base.Utils;
using ExNihilo.Base.Parameters;

namespace ExNihilo.Drawables;

/// <summary>
/// Define rectangle drawable object.
/// </summary>
public class Rectangle : BaseDrawableWithBrushAndPen
{
    /// <summary>
    /// Represent rectangular area where object will be drawn.
    /// </summary>
    public RectangleParameter Area { get; } = new RectangleParameter();

    /// <summary>
    /// <inheritdoc cref="Rectangle"/>
    /// </summary>
    public Rectangle() { }

    /// <summary>
    /// <inheritdoc cref="Rectangle"/>
    /// </summary>
    /// <param name="rectangle"><inheritdoc cref="Area" path="/summary"/></param>
    public Rectangle(SixLabors.ImageSharp.Rectangle rectangle) 
    {
        Area.WithValue(rectangle);
    }

    /// <summary>
    /// <inheritdoc cref="Rectangle"/>
    /// </summary>
    /// <param name="x">Set x coordinate of Area</param>
    /// <param name="y">Set y coordinate of Area</param>
    /// <param name="width">Set width of Area</param>
    /// <param name="height">Set height of Area</param>
    public Rectangle(int x, int y, int width, int height) : this(new SixLabors.ImageSharp.Rectangle(x, y, width, height))
    {
        
    }
    /// <summary>
    /// Set brush value.
    /// </summary>
    public Rectangle WithBrush(BrushType brushType, Color color)
    {
        Brush.WithValue(brushType, color);
        return this;
    }
    /// <summary>
    /// Set brush value.
    /// </summary>
    public Rectangle WithBrush(Action<BrushParameter> actionBrush)
    {
        actionBrush(Brush);
        return this;
    }
    /// <summary>
    /// Set pen value.
    /// </summary>
    public Rectangle WithPen(PenType penType, int width, Color color)
    {
        Pen.WithValue(penType, width, color);
        return this;
    }
    /// <summary>
    /// Set pen value.
    /// </summary>
    public Rectangle WithPen(Action<PenParameter> actionPen)
    {
        actionPen(Pen);
        return this;
    }
    /// <summary>
    /// Set drawable type value.
    /// </summary>
    public Rectangle WithType(DrawableType value)
    {
        Type.Value = value;
        return this;
    }
    /// <summary>
    /// Set area point value.
    /// </summary>
    public Rectangle WithPoint(Point p)
    {
        Area.WithPoint(p);
        return this;
    }
    /// <summary>
    /// Set points randomization parameters.
    /// </summary>
    public Rectangle WithRandomizedPoint(int minX, int maxX, int minY, int maxY)
    {
        Area.WithRandomizedPoint(minX, maxX, minY, maxY);
        return this;
    }
    /// <summary>
    /// Set area size value.
    /// </summary>
    public Rectangle WithSize(Size size)
    {
        Area.WithSize(size);
        return this;
    }
    /// <summary>
    /// Set size randomization parameters.
    /// </summary>
    public Rectangle WithRandomizedSize(int minWidth, int maxWidth, int minHeight, int maxHeight)
    {
        Area.WithRandomizedSize(minWidth, maxWidth, minHeight, maxHeight);
        return this;
    }
    /// <summary>
    /// Set size randomization parameters.
    /// </summary>
    public Rectangle WithRandomizedSize(int min, int max)
    {
        Area.WithRandomizedSize(min, max, min, max);
        return this;
    }

    public override void Render(Image image, GraphicsOptions graphicsOptions)
    {
        SixLabors.ImageSharp.Rectangle rect = Area;

        if (rect.Width <= 0 || rect.Height <= 0)
            return;

        IPath path = new RectangularPolygon(rect);
        DrawingOptions dopt = new() { GraphicsOptions = graphicsOptions };
        image.Mutate((x) =>
        {
            if (((DrawableType) Type).HasFlag(DrawableType.Filled))
                x.Fill(dopt, Brush.Value, path);
            if (((DrawableType) Type).HasFlag(DrawableType.Outlined))
                x.Draw(dopt, Pen.Value, path);
        });
    }
}
