using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Abstract;
using TCG.Base.Parameters;
using TCG.Base.Utils;

namespace TCG.Drawables;

public class DPolygon : BaseDrawableWithBrushAndPen
{
    public PointFArrayParameter Points { get; } = new PointFArrayParameter(new PointF[0]);

    public DPolygon() { }

    public DPolygon(PointF[] points)
    {
        Points.Value = points;
    }

    public DPolygon WithPoints(PointF[] points)
    {
        Points.Value = points;
        return this;
    }

    public DPolygon WithRandomizedPoints(int minCount, int maxCount, int minX, int maxX, int minY, int maxY)
    {
        Points.Length.Min = minCount;
        Points.Length.Max = maxCount;
        Points.X.Min = minX;
        Points.X.Max = maxX;
        Points.Y.Min = minY;
        Points.Y.Max = maxY;
        return this;
    }

    public DPolygon WithRandomizedPoints(int count, int minX, int maxX, int minY, int maxY)
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
                x.FillPolygon(dopt, Brush.Value ?? Brush.DefaultValue, Points);
            if (((DrawableType)Type).HasFlag(DrawableType.Outlined))
                x.DrawPolygon(dopt, Pen.Value ?? Pen.DefaultValue, Points);
        });
    }
}
