using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Abstract;
using TCG.Base.Parameters;
using TCG.Rnd.Randomizers.Parameters;

namespace TCG.Drawables;

public class DText : BaseDrawable
{
    public StringParameter Text { get; } = new StringParameter() { DefaultValue = "TEST" };
    public PointParameter Origin { get; } = new PointParameter();

    public Font Font { get; set; }
    public int Dpi { get; set; } = 72;
    public TextAlignment TextAlignment { get; set; } = TextAlignment.Center;
    public HorizontalAlignment HorizontalAlignment { get; set; } = HorizontalAlignment.Center;
    public VerticalAlignment VerticalAlignment { get; set; } = VerticalAlignment.Center;
    public float WrappingLength { get; set; } = -1;
    public float LineSpacing { get; set; } = 1;

    public TextOptions TextOptions
    {
        get
        {
            Point origin = Origin;

            return new(Font)
            {
                Dpi = Dpi,
                HorizontalAlignment = HorizontalAlignment,
                VerticalAlignment = VerticalAlignment,
                WrappingLength = WrappingLength,
                LineSpacing = LineSpacing,
                Origin = new System.Numerics.Vector2(origin.X, origin.Y)
            };
        }
    }

    public DText(Font font) : base()
    {
        Font = font;
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
            x.DrawText(dopt, TextOptions, Text, Brush, Pen);
        });
    }
}
