using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;
using TCG.Base.Parameters;

namespace TCG.Drawables;

public class DLine : IDrawable
{
    public IList<IEffect> Effects { get; } =  new List<IEffect>();

    public PenParameter Pen { get; } = new PenParameter(Pens.Solid(Color.Black, 1));
    public BoolParameter IsBeziers { get;  } = new BoolParameter(false);

    public PointFArrayParameter Points { get;  } = new PointFArrayParameter(new PointF[0]) { Length = { DefaultValue = 4}};

    public DLine() { }

    public DLine(PointF[] points) 
    { 
        Points.Value = points;
    }

    public DLine(PointF[] points, Color color) : this(points)
    {
        Pen.Color.Value = color;
        Pen.Color.Value = color;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions)
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