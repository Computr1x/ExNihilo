using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Abstract;

namespace TCG.Drawables;

public class DText : BaseDrawable
{
    public string Text { get; set; }
    public TextOptions TextOptions { get; set; }

    public DText(Font font, string text) : base()
    {
        Text = text;
        TextOptions = new TextOptions(font);
    }


    public override void Render(Image image, GraphicsOptions graphicsOptions)
    {
        if (string.IsNullOrEmpty(Text))
            return;

        DrawingOptions dopt = new() { GraphicsOptions = graphicsOptions };


        image.Mutate((x) =>
        {
            x.DrawText(dopt, TextOptions, Text, Brush, Pen);
        });
    }
}
