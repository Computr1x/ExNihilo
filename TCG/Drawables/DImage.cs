using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Abstract;
using TCG.Base.Parameters;

namespace TCG.Drawables;

public class DImage : BaseDrawable
{
    public PointParameter Point { get; } = new PointParameter(new Point());

    private Image? image = null;
    private string path = "";

    public DImage(string path)
    {
        this.path = path;
    }

    public DImage(Image image)
    {
        this.image = image;
    }

    public DImage WithPoint(Point p)
    {
        Point.Value = p;
        return this;
    }

    public DImage WithRandomiPoint(Point p)
    {
        Point.Value = p;
        return this;
    }

    public override void Render(Image image, GraphicsOptions graphicsOptions)
    {
        if (string.IsNullOrWhiteSpace(path) == false)
        {
            try
            {
                this.image = Image.Load(path);
            }
            catch { }
        }

        if (this.image == null)
            return;

        DrawingOptions dopt = new() { GraphicsOptions = graphicsOptions };

        image.Mutate((x) =>
        {
            x.DrawImage(this.image, Point, graphicsOptions);
        });
    }
}
