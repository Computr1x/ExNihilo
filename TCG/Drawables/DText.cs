using SixLabors.Fonts;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Abstract;
using TCG.Base.Interfaces;

namespace TCG.Drawables;

public class DText : BaseDrawable
{
    public string Text { get; set; }
    public TextOptions TextOptions { get; set; } = new TextOptions() { }

    public DText(string text) : base()
    {
        Text = text;
    }


    public override void Render(Image image, GraphicsOptions graphicsOptions)
    {
        if (string.IsNullOrEmpty(Text))
            return;

        DrawingOptions dopt = new() { GraphicsOptions = graphicsOptions };

        image.Mutate((x) =>
        {
            x.DrawText()
        });
    }
}
