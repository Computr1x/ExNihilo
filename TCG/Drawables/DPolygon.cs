using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Abstract;

namespace TCG.Drawables;

public class DPolygon : BaseDrawable
{
    public PointF[] Points { get; set; }

    public DPolygon(PointF[] points) : base()
    {
        Points = points;
    }

    public override void Render(Image image, GraphicsOptions graphicsOptions)
    {
        DrawingOptions dopt = new() { GraphicsOptions = graphicsOptions };
        
        image.Mutate((x) =>
        {
            if (Type.HasFlag(DrawableType.Filled))
                x.FillPolygon(dopt, Brush, Points);
            if (Type.HasFlag(DrawableType.Outlined))
                x.DrawPolygon(dopt, Pen, Points);
        });
    }
}
