using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using ExNihilo.Base.Interfaces;
using ExNihilo.Base.Properties;
using ExNihilo.Base.Utils;

namespace ExNihilo.Visuals;

/// <summary>
/// Define captcha visual object.
/// </summary>
public class Captcha : Text, ICaptcha
{
    /// <summary>
    /// Index of captcha. Needed to assign a captcha value.
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// Captcha text value.
    /// </summary>
    public string Text
    {
        get => Content;
        set
        {
            Content.WithValue(value);
        }
    }

    /// <summary>
    /// <inheritdoc cref="Captcha"/>
    /// </summary>
    public Captcha()
    {
        Brush.Type.Value = BrushType.Solid;
    }

    /// <summary>
    /// <inheritdoc cref="Captcha"/>
    /// </summary>
    /// <param name="fontFamily"><inheritdoc cref="Text.FontFamily" path="/summary"/></param>
    public Captcha(FontFamily fontFamily) : this()
    {
        FontFamily.Value = fontFamily;
    }

    /// <summary>
    /// <inheritdoc cref="Captcha"/>
    /// </summary>
    /// <param name="fontFamily"><inheritdoc cref="Text.FontFamily" path="/summary"/></param>
    /// <param name="text"><inheritdoc cref="Text.Content" path="/summary"/></param>
    public Captcha(FontFamily fontFamily, string text) : this(fontFamily)
    {
        Content.Value = text;
        Content.Length.Value = text.Length;
    }

    /// <summary>
    /// Set captcha Index value.
    /// </summary>
    /// <param name="index"><inheritdoc cref="Index" path="/summary"/></param>
    public Captcha WithIndex(int index)
    {
        Index = index;
        return this;
    }

    /// <summary>
    /// Set Brush value.
    /// </summary>
    public new Captcha WithBrush(Color color)
    {
        Brush.WithValue(BrushType.Solid, color);
        return this;
    }

    /// <summary>
    /// Set brush randomization parameters.
    /// </summary>
    /// <param name="palette">Color palette for brush</param>
    public new Captcha WithRandomizedBrush(Color[] palette)
    {
        Brush.WithRandomizedColor(palette);
        return this;
    }

    /// <summary>
    /// Set brush randomization parameters.
    /// </summary>
    /// <param name="colorsCount">Generate color palette with given colorsCount</param>
    public new Captcha WithRandomizedBrush(int colorsCount)
    {
        Brush.WithRandomizedColor(colorsCount);
        return this;
    }

    // Disabled via error in ImageSharp library
    //public new Captcha WithBrush(BrushType brushType, Color color)
    //{
    //    Brush.WithValue(brushType, color);
    //    return this;
    //}


    //public new Captcha WithBrush(Action<BrushProperty> actionBrush)
    //{
    //    actionBrush(Brush);
    //    return this;
    //}

    /// <summary>
    /// Set pen value.
    /// </summary>
    public new Captcha WithPen(PenType type, int width, Color color)
    {
        Pen.WithValue(type, width, color);
        return this;
    }

    /// <summary>
    /// Set pen value.
    /// </summary>
    public new Captcha WithPen(Action<PenProperty> actionPen)
    {
        actionPen(Pen);
        return this;
    }

    /// <summary>
    /// Set type value.
    /// </summary>
    public new Captcha WithType(VisualType value)
    {
        Type.Value = value;
        return this;
    }

    /// <summary>
    /// Set content value.
    /// </summary>
    public new Captcha WithContent(string value)
    {
        Content.Value = value;
        return this;
    }

    /// <summary>
    /// Set content randomization parameters.
    /// </summary>
    /// <param name="stringPropertySetter">Content set actions</param>
    public new Captcha WithRandomizedContent(Action<StringProperty> stringPropertySetter)
    {
        stringPropertySetter(Content);
        return this;
    }

    /// <summary>
    /// Set point value.
    /// </summary>
    public new Captcha WithPoint(Point p)
    {
        Point.WithValue(p);
        return this;
    }

    /// <summary>
    /// Set point randomization parameters.
    /// </summary>
    public new Captcha WithRandomizedPoint(int minX, int maxX, int minY, int maxY)
    {
        Point.WithRandomizedValue(minX, maxX, minY, maxY);
        return this;
    }

    /// <summary>
    /// Set fontfamily value.
    /// </summary>
    public new Captcha WithFontFamily(FontFamily value)
    {
        FontFamily.Value = value;
        return this;
    }

    /// <summary>
    /// Set font family randomization parameters.
    /// </summary>
    /// <param name="value">IEnumerable of font families</param>
    public new Captcha WithRandomizedFontFamily(IEnumerable<FontFamily> value)
    {
        FontFamily.Collection.Clear();
        FontFamily.Collection.AddRange(value);
        return this;
    }

    /// <summary>
    /// Set font size value.
    /// </summary>
    public new Captcha WithFontSize(int size)
    {
        FontSize.Value = size;
        return this;
    }

    /// <summary>
    /// Set FontSize randomization parameters.
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="Text.FontSize" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="Text.FontSize" path="/summary"/></param>
    public new Captcha WithRandomizedFontSize(int min, int max)
    {
        FontSize.Min = min;
        FontSize.Max = max;
        return this;
    }

    /// <summary>
    /// Set font style value.
    /// </summary>
    public new Captcha WithStyle(FontStyle value)
    {
        Style.Value = value;
        return this;
    }

    /// <summary>
    /// Set font dpi value.
    /// </summary>
    public new Captcha WithDpi(int value)
    {
        Dpi = value;
        return this;
    }

    /// <summary>
    /// Set text aligment value.
    /// </summary>
    public new Captcha WithTextAligment(TextAlignment value)
    {
        TextAlignment = value;
        return this;
    }
    /// <summary>
    /// Set text horizontal aligment value.
    /// </summary>
    public new Captcha WithHorizontalAligment(HorizontalAlignment value)
    {
        HorizontalAlignment = value;
        return this;
    }
    /// <summary>
    /// Set text vertical aligment value.
    /// </summary>
    public new Captcha WithVerticalAligment(VerticalAlignment value)
    {
        VerticalAlignment = value;
        return this;
    }
    /// <summary>
    /// Set text wrapping length value.
    /// </summary>
    public new Captcha WithWrappingLength(float value)
    {
        WrappingLength = value;
        return this;
    }
    /// <summary>
    /// Set text line spacing value.
    /// </summary>
    public new Captcha WithLineSpacing(float value)
    {
        LineSpacing = value;
        return this;
    }
}
