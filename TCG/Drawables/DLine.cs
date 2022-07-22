using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Abstract;
using TCG.Base.Interfaces;

namespace TCG.Drawables;

public class DLine : IDrawable
{
    public IPen Pen { get; set; } = Pens.Solid(Color.White, 1);
    public IList<IEffect> Effects { get; }

    public PointF[] Points { get; set; }

    public bool IsBeziers { get; set; } = false;

    public DLine(PointF[] points) : base()
    {
        Points = points;
        Effects = new List<IEffect>();
    }

    public void Render(Image image, GraphicsOptions graphicsOptions)
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