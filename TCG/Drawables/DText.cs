using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Abstract;
using TCG.Base.Parameters;

namespace TCG.Drawables;

public class DText : BaseDrawable
{
    public StringParameter Text { get; } = new StringParameter() { DefaultValue = "TEST" };
    public TextOptions Options { get; set; }

    public DText(Font font) : base()
    {
        Options = new TextOptions(font);
    }

    public DText(Font font, string text) : this(font)
    {
        Text.Value = text;
        Text.Length.Value = text.Length;
    }

    public override void Render(Image image, GraphicsOptions graphicsOptions)
    {
        if (string.IsNullOrEmpty(Text.Value ?? Text.DefaultValue))
            return;

        DrawingOptions dopt = new() { GraphicsOptions = graphicsOptions };


        image.Mutate((x) =>
        {
            x.DrawText(dopt, Options, Text, Brush, Pen);
        });
    }
}
