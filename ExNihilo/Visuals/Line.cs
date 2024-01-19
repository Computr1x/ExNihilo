using ExNihilo.Base;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;

namespace ExNihilo.Visuals;

/// <summary>
/// Define line visual object.
/// </summary>
public class Line : Visual
{
    /// <summary>
    /// Represents the pen with which to outlined an object.
    /// </summary>
    public PenProperty Pen { get; } = new PenProperty();
    /// <summary>
    /// Specifies if line type is bezier curve.
    /// </summary>
    public bool IsBeziers { get; set; } = false;
    /// <summary>
    /// Defines a set of line points. For bezier curves, the length of the array must be greater than or equal to four.
    /// </summary>
    public PointFArrayProperty Points { get;  } = new PointFArrayProperty(Array.Empty<PointF>()) {
        Length = {
            DefaultValue = 4
        }
    };

    /// <summary>
    /// <inheritdoc cref="Line"/>
    /// </summary>
    public Line() { }

    /// <summary>
    /// <inheritdoc cref="Line"/>
    /// </summary>
    /// <param name="points"><inheritdoc cref="Points" path="/summary"/></param>
    public Line(PointF[] points) 
    { 
        Points.Value = points;
    }
    /// <summary>
    /// Set pen value.
    /// </summary>
    public Line WithPen(PenType penType, int width, Color color)
    {
        Pen.WithValue(penType, width, color);
        return this;
    }
    /// <summary>
    /// Set pen value.
    /// </summary>
    public Line WithPen(Action<PenProperty> setPen)
    {
        setPen(Pen);
        return this;
    }
    /// <summary>
    /// Set line is bezeir value.
    /// </summary>
    public Line IsBezier(bool value)
    {
        IsBeziers = value;
        return this;
    }
    /// <summary>
    /// Set line point values.
    /// </summary>
    public Line WithPoints(PointF[] points)
    {
        Points.Value = points;
        return this;
    }

    /// <summary>
    /// Set points randomization parameters.
    /// </summary>
    public Line WithRandomizedPoints(int minCount, int maxCount, int minX, int maxX, int minY, int maxY)
    {
        Points.Length.WithRandomizedValue(minCount, maxCount);
        Points.X.WithRandomizedValue(minX, maxX);
        Points.Y.WithRandomizedValue(minY, maxY);
        return this;
    }

    /// <summary>
    /// Set points randomization parameters.
    /// </summary>
    public Line WithRandomizedPoints(int count, int minX, int maxX, int minY, int maxY)
    {
        return WithRandomizedPoints(count, count, minX, maxX, minY, maxY);
    }

    public override void Render(Image image, GraphicsOptions graphicsOptions)
    {
        if ((Points.Value ?? Points.DefaultValue).Length < 2)
            return;

        DrawingOptions dopt = new() { GraphicsOptions = graphicsOptions };
        
        image.Mutate(x =>
        {
            if (IsBeziers)
                x.DrawBeziers(dopt, Pen.Value, Points);
            else
                x.DrawLine(dopt, Pen.Value, Points);
        });
    }
}