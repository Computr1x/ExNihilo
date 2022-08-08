using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Abstract;
using TCG.Base.Parameters;
using TCG.Base.Utils;

namespace TCG.Drawables;

/// <summary>
/// Define svg path drawable object.
/// </summary>
public class SvgPath : BaseDrawableWithBrushAndPen
{
    /// <summary>
    /// Specifies the coordinate of the upper left corner of the svg path from which to start rendering.
    /// </summary>
    public PointParameter Point { get; } = new PointParameter();
    /// <summary>
    /// Defines a scale of the rendered object.
    /// </summary>
    public FloatParameter Scale { get; } = new FloatParameter(0, float.MaxValue, 1);

    private IPath svgPath;

    public SvgPath(string svgPathString) 
    {
        bool result = SixLabors.ImageSharp.Drawing.Path.TryParseSvgPath(svgPathString, out svgPath);
        if (result == false)
            throw new ArgumentException("Svg path is not falid or can't be parsed");
    }

    public SvgPath WithBrush(IBrush brush)
    {
        Brush.Value = brush;
        return this;
    }
    public SvgPath WithBrush(Action<BrushParameter> actionBrush)
    {
        actionBrush(Brush);
        return this;
    }

    public SvgPath WithPen(IPen pen)
    {
        Pen.Value = pen;
        return this;
    }

    public SvgPath WithPen(Action<PenParameter> actionPen)
    {
        actionPen(Pen);
        return this;
    }

    public SvgPath WithType(DrawableType value)
    {
        Type.Value = value;
        return this;
    }

    public SvgPath WithPoint(Point p)
    {
        Point.WithValue(p);
        return this;
    }

    public SvgPath WithRandomizedPoint(int minX, int maxX, int minY, int maxY)
    {
        Point.WithRandomizedValue(minX, maxX, minY, maxY);
        return this;
    }

    public SvgPath WithScale(float value)
    {
        Scale.Value = value;
        return this;
    }

    public SvgPath WithRandomizedSize(float min, float max)
    {
        Scale.WithRandomizedValue(min, max);
        return this;
    }

    public override void Render(Image image, GraphicsOptions graphicsOptions)
    {
        DrawingOptions dopt = new() { GraphicsOptions = graphicsOptions };

        svgPath = svgPath.Scale(Scale).Translate(Point);

        image.Mutate((x) =>
        {
            if (((DrawableType)Type).HasFlag(DrawableType.Filled))
                x.Fill(dopt, Brush.Value ?? Brush.DefaultValue, svgPath);
            if (((DrawableType)Type).HasFlag(DrawableType.Outlined))
                x.Draw(dopt, Pen.Value ?? Pen.DefaultValue, svgPath);
        });
    }
}