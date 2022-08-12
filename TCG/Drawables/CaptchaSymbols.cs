using SixLabors.Fonts;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Abstract;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;
using TCG.Base.Utils;

namespace TCG.Drawables;

/// <summary>
/// Define captcha drawable object.
/// </summary>
public class CaptchaSymbols : BaseDrawable, ICaptcha
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
    public PointParameter Point { get; } = new PointParameter();
    /// <summary>
    /// Text parameters of symbols.
    /// </summary>
    public TextSymbolsParameter TextSymbols { get; } = new TextSymbolsParameter();

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
        TextSymbols.Brush.Value = Brushes.Solid(color);
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
    public CaptchaSymbols WithPen(IPen pen)
    {
        TextSymbols.Pen.Value = pen;
        return this;
    }
    /// <summary>
    /// Set pen value.
    /// </summary>
    public CaptchaSymbols WithPen(Action<PenParameter> actionPen)
    {
        actionPen(TextSymbols.Pen);
        return this;
    }
    /// <summary>
    /// Set drawable type value.
    /// </summary>
    public CaptchaSymbols WithType(DrawableType value)
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
    public CaptchaSymbols WithRandomizedContent(Action<StringParameter> stringParameterSetter)
    {
        stringParameterSetter(TextSymbols.Content);
        return this;
    }
    /// <summary>
    /// Set point value.
    /// </summary>
    public CaptchaSymbols WithPoint(Point p)
    {
        Point.Value = p;
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
        this.TextAlignment = value;
        return this;
    }
    /// <summary>
    /// Sets the effect that will be applied to each captcha character.
    /// </summary>
    public CaptchaSymbols WithSymbolsEffect(IEffect effect)
    {
        TextSymbols.Effects.Add(effect);
        return this;
    }

    public override void Render(Image image, GraphicsOptions graphicsOptions)
    {
        if (string.IsNullOrEmpty(TextSymbols.Content.Value ?? TextSymbols.Content.DefaultValue) || TextSymbols.FontFamily.Value is null)
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
        Point origin;
        if (TextAlignment == TextAlignment.Start)
            origin = Point;
        else if (TextAlignment == TextAlignment.Center)
            origin = new Point((int)(Point.X - rect.Width / 2f), (int)(Point.Y - rect.Height / 2f));
        else
            origin = new Point((int)(Point.X - rect.Width), (int)(Point.Y - rect.Height));

        Image<Rgba32>? tempImg = null;
        image.Mutate((x) =>
        {
            // draw text by symbols
            foreach (var symbolsParams in TextSymbols.RandomizedTextParameters)
            {
                // create temp image
                rect = TextMeasurer.Measure(symbolsParams.Content.Value, opt);
                tempImg = new((int)rect.Width + 1, (int)rect.Height + 1);

                // draw cymbol
                tempImg.Mutate(y =>
                {
                    // depend on drawing type choose drawing method
                    if (((DrawableType)symbolsParams.Type).HasFlag(DrawableType.FillWithOutline))
                        y.DrawText(dopt, opt, symbolsParams.Content,
                            symbolsParams.Brush.Value ?? symbolsParams.Brush.DefaultValue,
                            symbolsParams.Pen.Value ?? symbolsParams.Pen.DefaultValue);
                    else if (((DrawableType)symbolsParams.Type).HasFlag(DrawableType.Filled))
                        y.DrawText(dopt, opt, symbolsParams.Content,
                            symbolsParams.Brush.Value ?? symbolsParams.Brush.DefaultValue, null);
                    else if (((DrawableType)symbolsParams.Type).HasFlag(DrawableType.Outlined))
                        y.DrawText(dopt, opt, symbolsParams.Content,
                            null, symbolsParams.Pen.Value ?? symbolsParams.Pen.DefaultValue);
                });

                // render effects for symbol
                foreach (var effect in symbolsParams.Effects)
                    effect.Render(tempImg, graphicsOptions);

                // draw symbol on main image
                x.DrawImage(tempImg, origin, graphicsOptions);

                // shift origin on symbol width
                origin = new Point(origin.X + (int)rect.Width, origin.Y);
            }
        });
        // dispose unmanaged resourse
        tempImg?.Dispose();
    }
}


public class TextSymbolsParameter : IRandomizableParameter
{
    public StringParameter Content { get; } = new StringParameter() { DefaultValue = "TEST" };
    public FontFamilyParameter FontFamily { get; } = new FontFamilyParameter();
    public FloatParameter FontSize { get; } = new FloatParameter(64) { Min = 32, Max = 128 };
    public EnumParameter<FontStyle> Style { get; } = new EnumParameter<FontStyle>(FontStyle.Regular);

    public BrushParameter Brush { get; } = new() { DefaultValue = Brushes.Solid(Color.Black) };
    public PenParameter Pen { get; } = new(Pens.Solid(Color.White, 1));
    public EnumParameter<DrawableType> Type { get; } = new(DrawableType.Filled);

    public List<IEffect> Effects { get; set; } = new List<IEffect>();

    public List<TextSymbolsParameter> RandomizedTextParameters { get; } = new List<TextSymbolsParameter>();


    public void Randomize(Random r, bool force = false)
    {
        Content.Randomize(r, force);
        RandomizedTextParameters.Clear();

        foreach (var symbol in Content.Value)
        {
            FontFamily.Randomize(r, force);
            FontSize.Randomize(r, force);
            Style.Randomize(r, force);
            Brush.Randomize(r, force);
            Pen.Randomize(r, force);
            Type.Randomize(r, force);

            foreach (var effect in Effects)
                RandomizeProperties(r, effect, force);

            RandomizedTextParameters.Add(new TextSymbolsParameter()
            {
                Content = { Value = symbol.ToString() },
                FontFamily = { Value = FontFamily.Value },
                FontSize = { Value = FontSize.Value },
                Style = { Value = Style.Value },
                Brush = { Value = Brush.Value },
                Pen = { Value = Pen.Value },
                Type = { Value = Type.Value },
                Effects = Effects.Select(x => x.Copy()).ToList()
            });
        }
    }

    public void RandomizeProperties(Random rnd, IRenderable renderable, bool force = false)
    {
        foreach (var property in renderable.GetType().GetProperties())
        {
            if (property.PropertyType.GetInterfaces().Contains(typeof(IRandomizableParameter)))
            {
                //Console.WriteLine(renderable.GetType().ToString() + " " + property.Name);
                object? propValue = property.GetValue(renderable);
                if (propValue != null)
                    (propValue as IRandomizableParameter)!.Randomize(rnd, force);
            }
        }
    }
}
