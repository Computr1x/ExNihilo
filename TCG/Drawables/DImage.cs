using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;

namespace TCG.Drawables;

public class DImage : IDrawable
{
    public PointParameter Location { get; } = new PointParameter(new Point());
    public IList<IEffect> Effects { get; }

    private Image? image = null;
    private string path = "";

    public DImage(string path)
    {
        this.path = path;
        Effects = new List<IEffect>();
    }

    public DImage(Image image)
    {
        this.image = image;
        Effects = new List<IEffect>();
    }


    public void Render(Image image, GraphicsOptions graphicsOptions)
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
            x.DrawImage(this.image, Location, graphicsOptions);
        });
    }
}
