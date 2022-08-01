using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Abstract;
using TCG.Base.Parameters;
using TCG.Base.Utils;

namespace TCG.Drawables;

public class DPolygon : BaseDrawable
{
    public PointFArrayParameter Points { get; } = new PointFArrayParameter(new PointF[0]);

    public DPolygon() { }

    public DPolygon(PointF[] points)
    {
        Points.Value = points;
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
