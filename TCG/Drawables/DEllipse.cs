using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Abstract;
using TCG.Base.Utils;
using TCG.Base.Parameters;

namespace TCG.Drawables;

public class DEllipse : DRectangle
{
    public DEllipse() { }

    public DEllipse(Rectangle rectangle) : base(rectangle) { }

    public DEllipse(int x, int y, int width, int height) : this(new Rectangle(x, y, width, height)) { }

    public override void Render(Image image, GraphicsOptions graphicsOptions)
    {
        Rectangle rect = Rectangle;

        if (rect.Width <= 0 || rect.Height <= 0)
            return;

        IPath path = new EllipsePolygon(new PointF(rect.X, rect.Y), rect.Size);
        DrawingOptions dopt = new () { GraphicsOptions = graphicsOptions };

        image.Mutate((x) =>
        {
            if (((DrawableType)Type).HasFlag(DrawableType.Filled))
                x.Fill(dopt, (Brush.Value ?? Brush.DefaultValue), path);
            if (((DrawableType)Type).HasFlag(DrawableType.Outlined))
                x.Draw(dopt, (Pen.Value ?? Pen.DefaultValue), path);
        });
    }
}
