using ExNihilo.Base;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;

namespace ExNihilo.Visuals;

/// <summary>
/// Define ellipse visual object.
/// </summary>
public class Ellipse : VisualWithBrushAndPen
{
    /// <summary>
    /// Represent rectangular area where ellipse will be drawn.
    /// </summary>
    public RectangleProperty Area { get; } = new RectangleProperty();

    /// <summary>
    /// <inheritdoc cref="Ellipse"/>
    /// </summary>
    public Ellipse() { }

    /// <summary>
    /// <inheritdoc cref="Ellipse"/>
    /// </summary>
    /// <param name="rectangle"><inheritdoc cref="Area" path="/summary"/></param>
    public Ellipse(SixLabors.ImageSharp.Rectangle rectangle)
    {
        Area.WithValue(rectangle);
    }

    /// <summary>
    /// <inheritdoc cref="Ellipse"/>
    /// </summary>
    /// <param name="x">Set x coordinate of Area</param>
    /// <param name="y">Set y coordinate of Area</param>
    /// <param name="width">Set width of Area</param>
    /// <param name="height">Set height of Area</param>
    public Ellipse(int x, int y, int width, int height) : this(new SixLabors.ImageSharp.Rectangle(x, y, width, height)) { }
    

    /// <summary>
    /// Set brush value.
    /// </summary>
    public Ellipse WithBrush(BrushType brushType, Color color)
    {
        Brush.WithValue(brushType, color);
        return this;
    }

    /// <summary>
    /// Set brush value.
    /// </summary>
    public Ellipse WithBrush(Action<BrushProperty> actionBrush)
    {
        actionBrush(Brush);
        return this;
    }
    /// <summary>
    /// Set pen value.
    /// </summary>
    public Ellipse WithPen(PenType penType, int width, Color color)
    {
        Pen.WithValue(penType, width, color);
        return this;
    }

    /// <summary>
    /// Set pen value.
    /// </summary>
    public Ellipse WithPen(Action<PenProperty> actionPen)
    {
        actionPen(Pen);
        return this;
    }
    /// <summary>
    /// Set visual type value.
    /// </summary>
    public Ellipse WithType(VisualType value)
    {
        Type.Value = value;
        return this;
    }
    /// <summary>
    /// Set visual point value.
    /// </summary>
    public Ellipse WithPoint(Point p)
    {
        Area.WithPoint(p);
        return this;
    }
    /// <summary>
    /// Set point randomization parameters.
    /// </summary>
    public Ellipse WithRandomizedPoint(int minX, int maxX, int minY, int maxY)
    {
        Area.WithRandomizedPoint(minX, maxX, minY, maxY);
        return this;
    }

    /// <summary>
    /// Set visual size value.
    /// </summary>
    public Ellipse WithSize(Size size)
    {
        Area.WithSize(size);
        return this;
    }

    /// <summary>
    /// Set size randomization parameters.
    /// </summary>
    public Ellipse WithRandomizedSize(int minWidth, int maxWidth, int minHeight, int maxHeight)
    {
        Area.WithRandomizedSize(minWidth, maxWidth, minHeight, maxHeight);
        return this;
    }
    /// <summary>
    /// Set area Size randomization parameters.
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="Size" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="Size" path="/summary"/></param>
    public Ellipse WithRandomizedSize(int min, int max)
    {
        Area.WithRandomizedSize(min, max, min, max);
        return this;
    }

    public override void Render(Image image, GraphicsOptions graphicsOptions)
    {
        SixLabors.ImageSharp.Rectangle rect = Area; 

        if (rect.Width <= 0 || rect.Height <= 0)
            return;

        IPath path = new EllipsePolygon(new PointF(rect.X, rect.Y), rect.Size);
        
        DrawingOptions drawingOptions = new() {
            GraphicsOptions = graphicsOptions
        };

        image.Mutate(x =>
        {
            if (((VisualType) Type).HasFlag(VisualType.Filled))
                x.Fill(drawingOptions, (Brush.Value), path);
            
            if (((VisualType) Type).HasFlag(VisualType.Outlined))
                x.Draw(drawingOptions, (Pen.Value), path);
        });

        base.Render(image, graphicsOptions);
    }
}
