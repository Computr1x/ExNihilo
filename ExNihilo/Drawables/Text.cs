using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using ExNihilo.Base.Abstract;
using ExNihilo.Base.Properties;
using ExNihilo.Base.Utils;

namespace ExNihilo.Drawables;

/// <summary>
/// Define text drawable object.
/// </summary>
public class Text : BaseDrawableWithBrushAndPen
{
    /// <summary>
    /// Specifies the text to be displayed
    /// </summary>
    public StringProperty Content { get; } = new StringProperty() { DefaultValue = "TEST" };
    /// <summary>
    /// <inheritdoc cref="TextOptions.Origin"/>
    /// </summary>
    public PointProperty Point { get; } = new PointProperty();
    /// <summary>
    /// Specifies text font family.
    /// </summary>
    public FontFamilyProperty FontFamily { get; } = new FontFamilyProperty();
    /// <summary>
    /// Specifies font size. Default value is 64;
    /// </summary>
    public FloatProperty FontSize { get; } = new FloatProperty(64) { Min = 32, Max = 128};
    /// <summary>
    /// Specifies font style. By default it's regular.
    /// </summary>
    public EnumProperty<FontStyle> Style { get; } = new EnumProperty<FontStyle>(FontStyle.Regular);

    /// <summary>
    /// <inheritdoc cref="TextOptions.Dpi"/>
    /// </summary>
    public int Dpi { get; set; } = 72;
    /// <summary>
    /// <inheritdoc cref="TextOptions.TextAlignment"/>
    /// </summary>
    public TextAlignment TextAlignment { get; set; } = TextAlignment.Center;
    /// <summary>
    /// <inheritdoc cref="TextOptions.HorizontalAlignment"/>
    /// </summary>
    public HorizontalAlignment HorizontalAlignment { get; set; } = HorizontalAlignment.Center;
    /// <summary>
    /// <inheritdoc cref="TextOptions.VerticalAlignment"/>
    /// </summary>
    public VerticalAlignment VerticalAlignment { get; set; } = VerticalAlignment.Center;
    /// <summary>
    /// <inheritdoc cref="TextOptions.WrappingLength"/>
    /// </summary>
    public float WrappingLength { get; set; } = -1;
    /// <summary>
    /// <inheritdoc cref="TextOptions.LineSpacing"/>
    /// </summary>
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

    /// <summary>
    /// <inheritdoc cref="Text"/>
    /// </summary>
    public Text() {
        Brush.Type.Value = BrushType.Solid;
    }

    /// <summary>
    /// <inheritdoc cref="Text"/>
    /// </summary>
    /// <param name="fontFamily"><inheritdoc cref="FontFamily" path="/summary"/></param>
    public Text(FontFamily fontFamily) : this()
    {
        FontFamily.Value = fontFamily;
    }

    /// <summary>
    /// <inheritdoc cref="Text"/>
    /// </summary>
    /// <param name="fontFamily"><inheritdoc cref="FontFamily" path="/summary"/></param>
    /// <param name="text"><inheritdoc cref="Content" path="/summary"/></param>
    public Text(FontFamily fontFamily, string text) : this(fontFamily)
    {
        Content.Value = text;
        Content.Length.Value = text.Length;
    }
    /// <summary>
    /// Set brush value.
    /// </summary>
    public Text WithBrush(Color color)
    {
        Brush.WithValue(BrushType.Solid, color);
        return this;
    }
    /// <summary>
    /// Set brush randomization parameters.
    /// </summary>
    public Text WithRandomizedBrush(Color[] palette)
    {
        Brush.WithRandomizedColor(palette);
        return this;
    }
    /// <summary>
    /// Set brush randomization parameters.
    /// </summary>
    public Text WithRandomizedBrush(int colorsCount)
    {
        Brush.WithRandomizedColor(colorsCount);
        return this;
    }

    // Disabled via error in ImageSharp library
    //public Text WithBrush(BrushType brushType, Color color)
    //{
    //    Brush.WithValue(brushType, color);
    //    return this;
    //}


    //public Text WithBrush(Action<BrushProperty> actionBrush)
    //{
    //    actionBrush(Brush);
    //    return this;
    //}
    /// <summary>
    /// Set pen value.
    /// </summary>
    public Text WithPen(PenType penType, int width, Color color)
    {
        Pen.WithValue(penType, width, color);
        return this;
    }
    /// <summary>
    /// Set pen value.
    /// </summary>
    public Text WithPen(Action<PenProperty> actionPen)
    {
        actionPen(Pen);
        return this;
    }
    /// <summary>
    /// Set drawable type value.
    /// </summary>
    public Text WithType(DrawableType value)
    {
        Type.Value = value;
        return this;
    }
    /// <summary>
    /// Set content value.
    /// </summary>
    public Text WithContent(string value)
    {
        Content.Value = value;
        return this;
    }
    /// <summary>
    /// Set content randomization parameters.
    /// </summary>
    public Text WithRandomizedContent(Action<StringProperty> stringPropertySetter)
    {
        stringPropertySetter(Content);
        return this;
    }
    /// <summary>
    /// Set point value.
    /// </summary>
    public Text WithPoint(Point p)
    {
        Point.WithValue(p);
        return this;
    }
    /// <summary>
    /// Set point randomization parameters.
    /// </summary>
    public Text WithRandomizedPoint(int minX, int maxX, int minY, int maxY)
    {
        Point.WithRandomizedValue(minX, maxX, minY, maxY);
        return this;
    }
    /// <summary>
    /// Set font family value.
    /// </summary>
    public Text WithFontFamily(FontFamily value)
    {
        FontFamily.Value = value;
        return this;
    }

    /// <summary>
    /// Set font faimily randomization parameters.
    /// </summary>
    public Text WithRandomizedFontFamily(IEnumerable<FontFamily> value)
    {
        FontFamily.Collection.Clear();
        FontFamily.Collection.AddRange(value);
        return this;
    }
    /// <summary>
    /// Set font size value.
    /// </summary>
    public Text WithFontSize(int size)
    {
        FontSize.Value = size;
        return this;
    }
    /// <summary>
    /// Set font size randomization parameters.
    /// </summary>
    public Text WithRandomizedFontSize(int min, int max)
    {
        FontSize.Min = min;
        FontSize.Max = max;
        return this;
    }

    /// <summary>
    /// Set font style value.
    /// </summary>
    public Text WithStyle(FontStyle value)
    {
        Style.Value = value;
        return this;
    }

    /// <summary>
    /// Set text DPI value.
    /// </summary>
    public Text WithDpi(int value)
    {
        Dpi = value;
        return this;
    }
    /// <summary>
    /// Set text aligment value.
    /// </summary>
    public Text WithTextAligment(TextAlignment value)
    {
        TextAlignment = value;
        return this;
    }
    /// <summary>
    /// Set horizontal aligment value.
    /// </summary>
    public Text WithHorizontalAligment(HorizontalAlignment value)
    {
        HorizontalAlignment = value;
        return this;
    }
    /// <summary>
    /// Set vertical aligment value.
    /// </summary>
    public Text WithVerticalAligment(VerticalAlignment value)
    {
        VerticalAlignment = value;
        return this;
    }
    /// <summary>
    /// Set wrapping lenght value.
    /// </summary>
    public Text WithWrappingLength(float value)
    {
        WrappingLength = value;
        return this;
    }
    /// <summary>
    /// Set line spacing value.
    /// </summary>
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
            if(((DrawableType) Type).HasFlag(DrawableType.FillWithOutline))
                x.DrawText(dopt, TextOptions, Content, Brush.Value, Pen.Value);
            else if (((DrawableType) Type).HasFlag(DrawableType.Filled))
                x.DrawText(dopt, TextOptions, Content, Brush.Value, null);
            else if (((DrawableType) Type).HasFlag(DrawableType.Outlined))
                x.DrawText(dopt, TextOptions, Content, null, Pen.Value);
        });
    }
}
