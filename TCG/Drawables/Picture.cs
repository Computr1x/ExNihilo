using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Abstract;
using TCG.Base.Parameters;

namespace TCG.Drawables;

/// <summary>
/// Define picture drawable object.
/// </summary>
public class Picture : BaseDrawable
{
    /// <summary>
    /// Specifies the coordinate of the upper left corner of the image from which to start rendering
    /// </summary>
    public PointParameter Point { get; } = new PointParameter(new Point());

    private Image? image = null;
    private string path = "";

    public Picture(string path)
    {
        this.path = path;
    }

    public Picture(byte[] image)
    {
        this.image = Image.Load(image);
    }

    public Picture(Image image)
    {
        this.image = image;
    }

    public Picture WithPoint(Point p)
    {
        Point.Value = p;
        return this;
    }

    public Picture WithRandomiPoint(Point p)
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
