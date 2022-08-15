using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using ExNihilo.Base.Abstract;
using ExNihilo.Base.Properties;
using ExNihilo.Base.Utils;

namespace ExNihilo.Visuals;

/// <summary>
/// Define polygon visual object.
/// </summary>
public class Polygon : VisualWithBrushAndPen
{
    /// <summary>
    /// Defines a set of polygon points.
    /// </summary>
    public PointFArrayProperty Points { get; } = new PointFArrayProperty(Array.Empty<PointF>());

    /// <summary>
    /// <inheritdoc cref="Polygon"/>
    /// </summary>
    public Polygon() { }

    /// <summary>
    /// <inheritdoc cref="Polygon"/>
    /// </summary>
    /// <param name="points"><inheritdoc cref="Points" path="/summary"/></param>
    public Polygon(PointF[] points)
    {
        Points.Value = points;
    }

    /// <summary>
    /// Set brush value.
    /// </summary>
    public Polygon WithBrush(BrushType brushType, Color color)
    {
        Brush.WithValue(brushType, color);
        return this;
    }
    /// <summary>
    /// Set brush value.
    /// </summary>
    public Polygon WithBrush(Action<BrushProperty> actionBrush)
    {
        actionBrush(Brush);
        return this;
    }
    /// <summary>
    /// Set pen value.
    /// </summary>
    public Polygon WithPen(PenType penType, int width, Color color)
    {
        Pen.WithValue(penType, width, color);
        return this;
    }
    /// <summary>
    /// Set pen value.
    /// </summary>
    public Polygon WithPen(Action<PenProperty> actionPen)
    {
        actionPen(Pen);
        return this;
    }
    /// <summary>
    /// Set visual type value.
    /// </summary>
    public Polygon WithType(VisualType value)
    {
        Type.Value = value;
        return this;
    }
    /// <summary>
    /// Set points value.
    /// </summary>
    public Polygon WithPoints(PointF[] points)
    {
        Points.Value = points;
        return this;
    }

    /// <summary>
    /// Set points randomization parameters.
    /// </summary>
    public Polygon WithRandomizedPoints(int minCount, int maxCount, int minX, int maxX, int minY, int maxY)
    {
        Points.Length.WithRandomizedValue(minCount, maxCount);
        Points.X.WithRandomizedValue(minX, maxX);
        Points.Y.WithRandomizedValue(minY, maxY);
        return this;
    }

    /// <summary>
    /// Set points randomization parameters.
    /// </summary>
    public Polygon WithRandomizedPoints(int count, int minX, int maxX, int minY, int maxY)
    {
        return WithRandomizedPoints(count, count, minX, maxX, minY, maxY);
    }

    public override void Render(Image image, GraphicsOptions graphicsOptions)
    {
        if ((Points.Value ?? Points.DefaultValue).Length <= 2)
            return;

        DrawingOptions dopt = new() { GraphicsOptions = graphicsOptions };

        image.Mutate((x) =>
        {
            if (((VisualType) Type).HasFlag(VisualType.Filled))
                x.FillPolygon(dopt, Brush.Value, Points);
            if (((VisualType) Type).HasFlag(VisualType.Outlined))
                x.DrawPolygon(dopt, Pen.Value, Points);
        });
    }
}
