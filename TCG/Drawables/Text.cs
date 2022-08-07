using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Abstract;
using TCG.Base.Parameters;
using TCG.Base.Utils;

namespace TCG.Drawables;

public class Text : BaseDrawableWithBrushAndPen
{
    public StringParameter Content { get; } = new StringParameter() { DefaultValue = "TEST" };
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

    public Text() {
        Brush.Type.Value = BrushType.Solid;
    }

    public Text(FontFamily fontFamily) : this()
    {
        FontFamily.Value = fontFamily;
    }

    public Text(FontFamily fontFamily, string text) : this(fontFamily)
    {
        Content.Value = text;
        Content.Length.Value = text.Length;
    }

    public Text WithBrush(Color color)
    {
        Brush.Value = Brushes.Solid(color);
        return this;
    }

    public Text WithRandomizedBrush(Color[] palette)
    {
        Brush.WithRandomizedColor(palette);
        return this;
    }

    public Text WithRandomizedBrush(int colorsCount)
    {
        Brush.WithRandomizedColor(colorsCount);
        return this;
    }

    // Disabled via error in ImageSharp library
    //public Text WithBrush(IBrush brush)
    //{
    //    Brush.Value = brush;
    //    return this;
    //}


    //public Text WithBrush(Action<BrushParameter> actionBrush)
    //{
    //    actionBrush(Brush);
    //    return this;
    //}

    public Text WithPen(IPen pen)
    {
        Pen.Value = pen;
        return this;
    }

    public Text WithPen(Action<PenParameter> actionPen)
    {
        actionPen(Pen);
        return this;
    }

    public Text WithType(DrawableType value)
    {
        Type.Value = value;
        return this;
    }

    public Text WithContent(string value)
    {
        Content.Value = value;
        return this;
    }

    public Text WithRandomizedContent(Action<StringParameter> stringParameterSetter)
    {
        stringParameterSetter(Content);
        return this;
    }

    public Text WithPoint(Point p)
    {
        Point.Value = p;
        return this;
    }

    public Text WithRandomizedPoint(int minX, int maxX, int minY, int maxY)
    {
        Point.X.Min = minX;
        Point.X.Max = maxX;
        Point.Y.Min = minY;
        Point.Y.Min = maxY;
        return this;
    }

    public Text WithFontFamily(FontFamily value)
    {
        FontFamily.Value = value;
        return this;
    }

    public Text WithRandomizedFontFamily(IEnumerable<FontFamily> value)
    {
        FontFamily.Collection.Clear();
        FontFamily.Collection.AddRange(value);
        return this;
    }

    public Text WithFontSize(int size)
    {
        FontSize.Value = size;
        return this;
    }

    public Text WithRandomizedFontSize(int min, int max)
    {
        FontSize.Min = min;
        FontSize.Max = max;
        return this;
    }

    public Text WithStyle(FontStyle value)
    {
        Style.Value = value;
        return this;
    }

    public Text WithDpi(int value)
    {
        Dpi = value;
        return this;
    }

    public Text WithTextAligment(TextAlignment value)
    {
        this.TextAlignment = value;
        return this;
    }

    public Text WithHorizontalAligment(HorizontalAlignment value)
    {
        HorizontalAlignment = value;
        return this;
    }

    public Text WithVerticalAligment(VerticalAlignment value)
    {
        VerticalAlignment = value;
        return this;
    }

    public Text WithWrappingLength(float value)
    {
        WrappingLength = value;
        return this;
    }

    public Text WithLineSpacing(float value)
    {
        LineSpacing = value;
        return this;
    }

    public override void Render(Image image, GraphicsOptions graphicsOptions)
    {
        if (string.IsNullOrEmpty(Content.Value ?? Content.DefaultValue) || FontFamily.Value is null)
            return;
        
        DrawingOptions dopt = new() { GraphicsOptions = graphicsOptions };

        image.Mutate((x) =>
        {
            if(((DrawableType)Type).HasFlag(DrawableType.FillWithOutline))
                x.DrawText(dopt, TextOptions, Content, Brush.Value ?? Brush.DefaultValue, Pen.Value ?? Pen.DefaultValue);
            else if (((DrawableType)Type).HasFlag(DrawableType.Filled))
                x.DrawText(dopt, TextOptions, Content, Brush.Value ?? Brush.DefaultValue, null);
            else if (((DrawableType)Type).HasFlag(DrawableType.Outlined))
                x.DrawText(dopt, TextOptions, Content, null, Pen.Value ?? Pen.DefaultValue);
        });
    }
}
