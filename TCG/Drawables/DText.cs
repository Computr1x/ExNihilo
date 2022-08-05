using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Abstract;
using TCG.Base.Parameters;

namespace TCG.Drawables;

public class DText : BaseDrawableWithBrushAndPen
{
    public StringParameter Text { get; } = new StringParameter() { DefaultValue = "TEST" };
    public PointParameter Point { get; } = new PointParameter();
    public FontFamilyParameter FontFamily { get; } = new FontFamilyParameter();
    public FloatParameter FontSize { get; } = new FloatParameter(64) { Min = 32, Max = 128};
    public EnumParameter<FontStyle> Style { get; } = new EnumParameter<FontStyle>(FontStyle.Regular);

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
            Point origin = Point;

            return new((FontFamily.Value ?? FontFamily.DefaultValue).CreateFont(FontSize, Style))
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

    public DText() { }

    public DText(FontFamily fontFamily) : base()
    {
        FontFamily.Value = fontFamily;
    }

    public DText(FontFamily fontFamily, string text) : this(fontFamily)
    {
        Text.Value = text;
        Text.Length.Value = text.Length;
    }

    public DText WithText(string value)
    {
        Text.Value = value;
        return this;
    }

    public DText WithText(Action<StringParameter> stringParameterSetter)
    {
        stringParameterSetter(Text);
        return this;
    }

    public DText WithPoint(Point p)
    {
        Point.Value = p;
        return this;
    }

    public DText WithRandomizedPoint(int minX, int maxX, int minY, int maxY)
    {
        Point.X.Min = minX;
        Point.X.Max = maxX;
        Point.Y.Min = minY;
        Point.Y.Min = maxY;
        return this;
    }

    public DText WithFontFamily(FontFamily value)
    {
        FontFamily.Value = value;
        return this;
    }

    public DText WithRandomizedFontFamily(IEnumerable<FontFamily> value)
    {
        FontFamily.Collection.Clear();
        FontFamily.Collection.AddRange(value);
        return this;
    }

    public DText WithFontSize(int size)
    {
        FontSize.Value = size;
        return this;
    }

    public DText WithRandomizedFontSize(int min, int max)
    {
        FontSize.Min = min;
        FontSize.Max = max;
        return this;
    }

    public DText WithStyle(FontStyle value)
    {
        Style.Value = value;
        return this;
    }

    public DText WithDpi(int value)
    {
        Dpi = value;
        return this;
    }

    public DText WithTextAligment(TextAlignment value)
    {
        this.TextAlignment = value;
        return this;
    }

    public DText WithHorizontalAligment(HorizontalAlignment value)
    {
        HorizontalAlignment = value;
        return this;
    }

    public DText WithVerticalAligment(VerticalAlignment value)
    {
        VerticalAlignment = value;
        return this;
    }

    public DText WithWrappingLength(float value)
    {
        WrappingLength = value;
        return this;
    }

    public DText WithLineSpacing(float value)
    {
        LineSpacing = value;
        return this;
    }

    public override void Render(Image image, GraphicsOptions graphicsOptions)
    {
        if (string.IsNullOrEmpty(Text.Value ?? Text.DefaultValue) || FontFamily.Value is null)
            return;
        
        DrawingOptions dopt = new() { GraphicsOptions = graphicsOptions };

        image.Mutate((x) =>
        {
            x.DrawText(dopt, TextOptions, Text, Brush.Value ?? Brush.DefaultValue, Pen.Value ?? Pen.DefaultValue);
        });
    }
}
