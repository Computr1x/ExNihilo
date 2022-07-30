using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Abstract;
using TCG.Rnd.Randomizers.Parameters;

namespace TCG.Drawables;

public class DEllipse : BaseDrawable
{
    public RectangleParameter Rectangle { get; } = new RectangleParameter();

    public DEllipse() { }

    public DEllipse(Rectangle value) : base()
    {
        Rectangle.Value = value;
    }

    public DEllipse(int x, int y, int width, int height)
    : this(new Rectangle(x, y, width, height))
    { }

    public override void Render(Image image, GraphicsOptions graphicsOptions)
    {
        SixLabors.ImageSharp.Rectangle rect = Rectangle;

        if (rect.Width <= 0 || rect.Height <= 0)
            return;

        IPath path = new EllipsePolygon(new PointF(rect.X, rect.Y), rect.Size);
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
