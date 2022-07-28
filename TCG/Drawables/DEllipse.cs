using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Abstract;
using TCG.Rnd.Randomizers.Parameters;

namespace TCG.Drawables;

public class DEllipse : BaseDrawable
{
    public RectangleParameter Rect { get; set; } = new RectangleParameter(new Rectangle());

    public DEllipse(Rectangle defaultValue) : base()
    {
        Rect.DefaultValue = defaultValue;
    }

    public DEllipse(int x, int y, int width, int height)
    : this(new Rectangle(x, y, width, height))
    { }

    public override void Render(Image image, GraphicsOptions graphicsOptions)
    {
        IPath path = new EllipsePolygon(Rect.Point, Rect.Size);
        DrawingOptions dopt = new () { GraphicsOptions = graphicsOptions };

        image.Mutate((x) =>
        {
            if (Type.HasFlag(DrawableType.Filled))
                x.Fill(dopt, Brush, path);
            if (Type.HasFlag(DrawableType.Outlined))
                x.Draw(dopt, Pen, path);
        });
    }
}
