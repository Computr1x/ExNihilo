using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Abstract;

namespace TCG.Drawables;

public class DLine : BaseDrawable
{
    public PointF[] Points { get; set; }

    public bool IsBeziers { get; set; } = false;

    public DLine(PointF[] points) : base()
    {
        Points = points;
    }

    public override void Render(Image image, GraphicsOptions graphicsOptions)
    {
        DrawingOptions dopt = new () { GraphicsOptions = graphicsOptions };

        image.Mutate((x) =>
        {
            if (IsBeziers)
                x.DrawBeziers(dopt, Pen, Points);
            else
                x.DrawLines(dopt, Pen, Points);
        });
    }
}