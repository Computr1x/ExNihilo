using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using ExNihilo.Base.Abstract;
using ExNihilo.Base.Interfaces;
using ExNihilo.Base.Properties;
using ExNihilo.Base.Utils;
using ExNihilo.Rnd;

namespace ExNihilo.Visuals;

/// <summary>
/// Define captcha visual object.
/// </summary>
public class CaptchaSymbols : Visual, ICaptcha
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
        get => TextSymbols.Content;
        set
        {
            TextSymbols.Content.WithValue(value);
        }
    }

    /// <summary>
    /// Captcha drawing position.
    /// </summary>
    public PointProperty Point { get; } = new PointProperty();
    /// <summary>
    /// Text parameters of symbols.
    /// </summary>
    public TextSymbolsProperty TextSymbols { get; } = new TextSymbolsProperty();

    /// <summary>
    /// Text DPI.
    /// </summary>
    public int Dpi { get; set; } = 72;
    /// <summary>
    /// Text aligment.
    /// </summary>
    public TextAlignment TextAlignment { get; set; } = TextAlignment.Center;

    /// <summary>
    /// <inheritdoc cref="CaptchaSymbols"/>
    /// </summary>
    public CaptchaSymbols()
    {
        TextSymbols.Brush.Type.Value = BrushType.Solid;
    }

    /// <summary>
    /// <inheritdoc cref="CaptchaSymbols"/>
    /// </summary>
    /// <param name="fontFamily"><inheritdoc cref="TextSymbols.FontFamily" path="/summary"/></param>
    public CaptchaSymbols(FontFamily fontFamily) : this()
    {
        TextSymbols.FontFamily.Value = fontFamily;
    }

    /// <summary>
    /// <inheritdoc cref="CaptchaSymbols"/>
    /// </summary>
    /// <param name="fontFamily"><inheritdoc cref="TextSymbols.FontFamily" path="/summary"/></param>
    /// <param name="text"><inheritdoc cref="TextSymbols.Content" path="/summary"/></param>
    public CaptchaSymbols(FontFamily fontFamily, string text) : this(fontFamily)
    {
        TextSymbols.Content.Value = text;
        TextSymbols.Content.Length.Value = text.Length;
    }

    /// <summary>
    /// Set brush value.
    /// </summary>
    public CaptchaSymbols WithBrush(Color color)
    {
        TextSymbols.Brush.WithValue(BrushType.Solid, color);
        return this;
    }
    /// <summary>
    /// Set brush randomization parameters.
    /// </summary>
    public CaptchaSymbols WithRandomizedBrush(Color[] palette)
    {
        TextSymbols.Brush.WithRandomizedColor(palette);
        return this;
    }
    /// <summary>
    /// Set brush randomization parameters.
    /// </summary>
    public CaptchaSymbols WithRandomizedBrush(int colorsCount)
    {
        TextSymbols.Brush.WithRandomizedColor(colorsCount);
        return this;
    }
    /// <summary>
    /// Set pen value.
    /// </summary>
    public CaptchaSymbols WithPen(PenType penType, int width, Color color)
    {
        TextSymbols.Pen.WithValue(penType, width, color);
        return this;
    }
    /// <summary>
    /// Set pen value.
    /// </summary>
    public CaptchaSymbols WithPen(Action<PenProperty> actionPen)
    {
        actionPen(TextSymbols.Pen);
        return this;
    }
    /// <summary>
    /// Set visual type value.
    /// </summary>
    public CaptchaSymbols WithType(VisualType value)
    {
        TextSymbols.Type.Value = value;
        return this;
    }
    /// <summary>
    /// Set content value.
    /// </summary>
    public CaptchaSymbols WithContent(string value)
    {
        TextSymbols.Content.Value = value;
        return this;
    }
    /// <summary>
    /// Set content randomization parameters.
    /// </summary>
    public CaptchaSymbols WithRandomizedContent(Action<StringProperty> stringPropertySetter)
    {
        stringPropertySetter(TextSymbols.Content);
        return this;
    }
    /// <summary>
    /// Set point value.
    /// </summary>
    public CaptchaSymbols WithPoint(Point p)
    {
        Point.WithValue(p);
        return this;
    }
    /// <summary>
    /// Set point randomization parameters.
    /// </summary>
    public CaptchaSymbols WithRandomizedPoint(int minX, int maxX, int minY, int maxY)
    {
        Point.WithRandomizedValue(minX, maxX, minY, maxY);
        return this;
    }
    /// <summary>
    /// Set font family value.
    /// </summary>
    public CaptchaSymbols WithFontFamily(FontFamily value)
    {
        TextSymbols.FontFamily.Value = value;
        return this;
    }

    /// <summary>
    /// Set font faimily randomization parameters.
    /// </summary>
    public CaptchaSymbols WithRandomizedFontFamily(IEnumerable<FontFamily> value)
    {
        TextSymbols.FontFamily.Collection.Clear();
        TextSymbols.FontFamily.Collection.AddRange(value);
        return this;
    }
    /// <summary>
    /// Set font size value.
    /// </summary>
    public CaptchaSymbols WithFontSize(int size)
    {
        TextSymbols.FontSize.Value = size;
        return this;
    }
    /// <summary>
    /// Set font size randomization parameters.
    /// </summary>
    public CaptchaSymbols WithRandomizedFontSize(int min, int max)
    {
        TextSymbols.FontSize.Min = min;
        TextSymbols.FontSize.Max = max;
        return this;
    }

    /// <summary>
    /// Set font style value.
    /// </summary>
    public CaptchaSymbols WithStyle(FontStyle value)
    {
        TextSymbols.Style.Value = value;
        return this;
    }

    /// <summary>
    /// Set text DPI value.
    /// </summary>
    public CaptchaSymbols WithDpi(int value)
    {
        Dpi = value;
        return this;
    }
    /// <summary>
    /// Set text aligment value.
    /// </summary>
    public CaptchaSymbols WithTextAligment(TextAlignment value)
    {
        TextAlignment = value;
        return this;
    }
    /// <summary>
    /// Sets the effect that will be applied to each captcha character.
    /// </summary>
    public CaptchaSymbols WithSymbolsEffect(Effect effect)
    {
        TextSymbols.Effects.Add(effect);
        return this;
    }

    public override void Render(Image image, GraphicsOptions graphicsOptions)
    {
        if (string.IsNullOrEmpty(TextSymbols.Content) || TextSymbols.FontFamily.Value is null)
            return;

        // create text options
        TextOptions opt = new((TextSymbols.FontFamily.Value ?? TextSymbols.FontFamily.DefaultValue).
                    CreateFont(TextSymbols.FontSize, TextSymbols.Style))
        {
            Dpi = Dpi,
            TextAlignment = TextAlignment.Start,
            HorizontalAlignment = HorizontalAlignment.Left,
            VerticalAlignment = VerticalAlignment.Top,
            Origin = new System.Numerics.Vector2(0, 0)
        };

        // calculate rendered text width
        FontRectangle rect = TextMeasurer.Measure(TextSymbols.Content, opt);

        DrawingOptions dopt = new() { GraphicsOptions = graphicsOptions };

        // set text origin point
        Point origin = Point;
        if (TextAlignment == TextAlignment.Start)
            origin = Point;
        else if (TextAlignment == TextAlignment.Center)
            origin = new Point((int) (origin.X - rect.Width / 2f), (int) (origin.Y - rect.Height / 2f));
        else
            origin = new Point((int) (origin.X - rect.Width), (int) (origin.Y - rect.Height));
        Image<Rgba32>? tempImg = null;
        image.Mutate((x) =>
        {
            // draw text by symbols
            foreach (var symbolsParams in TextSymbols.RandomizedTextProperties)
            {
                // create temp image
                rect = TextMeasurer.Measure(symbolsParams.Content.Value, opt);

                // skip symbol drawing if he outside of image
                if (origin.X + rect.Width < 0 || origin.X + rect.Width >= image.Width ||
                    origin.Y + rect.Height < 0 || origin.Y + rect.Height >= image.Height)
                {
                    origin = new Point(origin.X + (int) rect.Width, origin.Y);
                    continue;
                }

                tempImg = new((int) rect.Width + 1, (int) rect.Height + 1);

                // draw cymbol
                tempImg.Mutate(y =>
                {
                        // depend on drawing type choose drawing method
                    if (((VisualType) symbolsParams.Type).HasFlag(VisualType.FillWithOutline))
                        y.DrawText(dopt, opt, symbolsParams.Content,
                            symbolsParams.Brush.Value,
                            symbolsParams.Pen.Value);
                    else if (((VisualType) symbolsParams.Type).HasFlag(VisualType.Filled))
                        y.DrawText(dopt, opt, symbolsParams.Content,
                            symbolsParams.Brush.Value, null);
                    else if (((VisualType) symbolsParams.Type).HasFlag(VisualType.Outlined))
                        y.DrawText(dopt, opt, symbolsParams.Content,
                            null, symbolsParams.Pen.Value);
                });

                // render effects for symbol
                foreach (var effect in symbolsParams.Effects)
                    effect.Render(tempImg, graphicsOptions);

                // draw symbol on main image
                x.DrawImage(tempImg, origin, graphicsOptions);

                // shift origin on symbol width
                origin = new Point(origin.X + (int) rect.Width, origin.Y);
            }
        });
        // dispose unmanaged resourse
        tempImg?.Dispose();
    }
}


public class TextSymbolsProperty : Property
{
    /// <summary>
    /// Specifies the text to be displayed
    /// </summary>
    public StringProperty Content { get; } = new StringProperty
    {
        DefaultValue = "TEST"
    };

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
    public FloatProperty FontSize { get; } = new FloatProperty(64) { Min = 32, Max = 128 };
    /// <summary>
    /// Specifies font style. By default it's regular.
    /// </summary>
    public EnumProperty<FontStyle> Style { get; } = new EnumProperty<FontStyle>(FontStyle.Regular);

    /// <summary>
    /// Represents the pen with which to stroke an object.
    /// </summary>
    public BrushProperty Brush { get; } = new();
    /// <summary>
    /// Represents the pen with which to outlined an object.
    /// </summary>
    public PenProperty Pen { get; } = new();
    /// <summary>
    /// Specifies the rendering type of an object
    /// </summary>
    public EnumProperty<VisualType> Type { get; } = new(VisualType.Filled);

    /// <summary>
    /// Specify collection of effect that will be applied to all characters.
    /// </summary>
    public List<Effect> Effects { get; set; } = new List<Effect>();
    /// <summary>
    /// Specify collection of randomized text parameters for every character.
    /// </summary>
    public List<TextSymbolsProperty> RandomizedTextProperties { get; } = new List<TextSymbolsProperty>();

    public override void Randomize(Random r, bool force = false)
    {
        Content.Randomize(r, force);
        RandomizedTextProperties.Clear();

        foreach (var symbol in (string) Content)
        {
            FontFamily.Randomize(r, force);
            FontSize.Randomize(r, force);
            Style.Randomize(r, force);
            Brush.Randomize(r, force);
            Pen.Randomize(r, force);
            Type.Randomize(r, force);

            foreach (var effect in Effects)
                RandomManager.RandomizeProperties(effect, force);

            var symbolParams = new TextSymbolsProperty()
            {
                Content = { Value = symbol.ToString() },
                FontFamily = { Value = FontFamily.Value },
                FontSize = { Value = FontSize.Value },
                Style = { Value = Style.Value },
                Type = { Value = Type.Value },
                Effects = Effects.Select(x => x.Copy()).ToList()
            };

            symbolParams.Brush.WithValue(Brush.Type, Brush.Color);
            symbolParams.Pen.WithValue(Pen.Type, Pen.Width, Pen.Color);

            RandomizedTextProperties.Add(symbolParams);
        }
    }
}
