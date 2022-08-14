using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Abstract;
using TCG.Base.Parameters;
using TCG.Base.Utils;

namespace TCG.Drawables;

/// <summary>
/// Define polygon drawable object.
/// </summary>
public class Polygon : BaseDrawableWithBrushAndPen
{
    /// <summary>
    /// Defines a set of polygon points.
    /// </summary>
    public PointFArrayParameter Points { get; } = new PointFArrayParameter(new PointF[0]);

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
    public Polygon WithBrush(Action<BrushParameter> actionBrush)
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
    public Polygon WithPen(Action<PenParameter> actionPen)
    {
        actionPen(Pen);
        return this;
    }
    /// <summary>
    /// Set drawable type value.
    /// </summary>
    public Polygon WithType(DrawableType value)
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
            if (((DrawableType)Type).HasFlag(DrawableType.Filled))
                x.FillPolygon(dopt, Brush.Value, Points);
            if (((DrawableType)Type).HasFlag(DrawableType.Outlined))
                x.DrawPolygon(dopt, Pen.Value, Points);
        });
    }
}
