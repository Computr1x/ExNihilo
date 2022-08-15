using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using ExNihilo.Base.Abstract;
using ExNihilo.Base.Interfaces;
using ExNihilo.Base.Parameters;
using ExNihilo.Base.Utils;

namespace ExNihilo.Drawables;

/// <summary>
/// Define line drawable object.
/// </summary>
public class Line : BaseDrawable
{
    /// <summary>
    /// Represents the pen with which to outlined an object.
    /// </summary>
    public PenParameter Pen { get; } = new PenParameter();
    /// <summary>
    /// Specifies if line type is bezier curve.
    /// </summary>
    public bool IsBeziers { get; set; } = false;
    /// <summary>
    /// Defines a set of line points. For bezier curves, the length of the array must be greater than or equal to four.
    /// </summary>
    public PointFArrayParameter Points { get;  } = new PointFArrayParameter(Array.Empty<PointF>()) {
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
    public Line WithPen(Action<PenParameter> setPen)
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
        
        image.Mutate((x) =>
        {
            if (IsBeziers)
                x.DrawBeziers(dopt, Pen.Value, Points);
            else
                x.DrawLines(dopt, Pen.Value, Points);
        });
    }
}