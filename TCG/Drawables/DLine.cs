using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;
using TCG.Rnd.Randomizers.Parameters;

namespace TCG.Drawables;

public class DLine : IDrawable
{
    public IList<IEffect> Effects { get; }

    public PenParameter Pen { get; set; } = new PenParameter(Pens.Solid(Color.Black, 1));
    public BoolParameter IsBeziers { get; set; } = new BoolParameter(false);

    public PointFArrayParameter Points { get; set; } = new PointFArrayParameter(new PointF[0]);


    public DLine(PointFParameter[] points) : base()
    {
        Points = points;
        Effects = new List<IEffect>();
    }

    public void Render(Image image, GraphicsOptions graphicsOptions)
    {
        DrawingOptions dopt = new() { GraphicsOptions = graphicsOptions };
        
        image.Mutate((x) =>
        {
            var pointsArray = Points.Select(x => (PointF)x).ToArray();
            if (IsBeziers)
                x.DrawBeziers(dopt, Pen.Value, pointsArray);
            else
                x.DrawLines(dopt, Pen.Value, pointsArray);
        });
    }
}