using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Abstract;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;

namespace TCG.Drawables;

public class Line : BaseDrawable
{
    public PenParameter Pen { get; } = new PenParameter(Pens.Solid(Color.Black, 1));
    public BoolParameter IsBeziers { get;  } = new BoolParameter(false);

    public PointFArrayParameter Points { get;  } = new PointFArrayParameter(new PointF[0]) { Length = { DefaultValue = 4}};

    public Line() { }

    public Line(PointF[] points) 
    { 
        Points.Value = points;
    }

    public Line WithPen(IPen pen)
    {
        Pen.Value = pen;
        return this;
    }

    public Line WithPen(Action<PenParameter> setPen)
    {
        setPen(Pen);
        return this;
    }

    public Line IsBezier(bool value)
    {
        IsBeziers.Value = value;
        return this;
    }

    public Line WithPoints(PointF[] points)
    {
        Points.Value = points;
        return this;
    }

    public Line WithRandomizedPoints(int minCount, int maxCount, int minX, int maxX, int minY, int maxY)
    {
        Points.Length.Min = minCount;
        Points.Length.Max = maxCount;
        Points.X.Min = minX;
        Points.X.Max = maxX;
        Points.Y.Min = minY;
        Points.Y.Max = maxY;
        return this;
    }

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
                x.DrawBeziers(dopt, Pen.Value ?? Pen.DefaultValue, Points);
            else
                x.DrawLines(dopt, Pen.Value ?? Pen.DefaultValue, Points);
        });
    }
}