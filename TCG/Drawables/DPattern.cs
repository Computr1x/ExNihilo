using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Abstract;
using TCG.Base.Parameters;

namespace TCG.Drawables;

public class DPattern : BaseDrawable
{
    public RectangleParameter Rectangle { get; } = new RectangleParameter();
    public Bool2DArrayParameter Pattern { get; } = new Bool2DArrayParameter(new bool[,] { { true, false }, { false, true } });
    public ColorParameter Background { get; } = new ColorParameter(SixLabors.ImageSharp.Color.Transparent);
    public ColorParameter Foreground { get; } = new ColorParameter(SixLabors.ImageSharp.Color.Black, 5);

    public DPattern() { }

    public DPattern(Rectangle rectangle, bool[,] pattern)
    {
        Rectangle.Value = rectangle;
        Pattern.Value = pattern;
    }

    public DPattern(Rectangle rectangle, bool[,] pattern, Color foreground) : this(rectangle, pattern)
    {
        Foreground.Value = foreground;
    }

    public DPattern(Rectangle rectangle, bool[,] pattern, Color foreground, Color background) : this(rectangle, pattern, foreground)
    {
        Background.Value = background;
    }

    public DPattern WithPoint(Point p)
    {
        Rectangle.Point.Value = p;
        return this;
    }

    public DPattern WithRandomizedPoint(int minX, int maxX, int minY, int maxY)
    {
        Rectangle.Point.X.Min = minX;
        Rectangle.Point.X.Max = maxX;
        Rectangle.Point.Y.Min = minY;
        Rectangle.Point.Y.Min = maxY;
        return this;
    }

    public DPattern WithSize(Size size)
    {
        Rectangle.Size.Value = size;
        return this;
    }

    public DPattern WithRandomizedSize(int minWidth, int maxWidth, int minHeight, int maxHeight)
    {
        Rectangle.Size.Width.Min = minWidth;
        Rectangle.Size.Width.Max = maxWidth;
        Rectangle.Size.Height.Min = minHeight;
        Rectangle.Size.Height.Max = maxHeight;
        return this;
    }

    public DPattern WithPattern(bool[,] pattern)
    {
        Pattern.Value = pattern;
        return this;
    }

    public DPattern WithPattern(Action<Bool2DArrayParameter> patternSetter)
    {
        patternSetter(Pattern);
        return this;
    }

    public DPattern WithRandomizedPattern(int size)
    {
        Pattern.Width.Value = Pattern.Height.Value = size;
        return this;
    }

    public DPattern WithBackgroundColor(SixLabors.ImageSharp.Color color)
    {
        Background.Value = color;
        return this;
    }

    public DPattern WithRandomizedBackgroundColor(int colorsCount, byte opacity = 255)
    {
        Background.Opacity = opacity;
        Background.Colors = Background.GeneratePalette(colorsCount);
        return this;
    }

    public DPattern WithRandomizedBackgroundColor(SixLabors.ImageSharp.Color[] palette)
    {
        Background.Colors = palette;
        return this;
    }

    public DPattern WithForegroundColor(SixLabors.ImageSharp.Color color)
    {
        Foreground.Value = color;
        return this;
    }

    public DPattern WithRandomizedForegroundColor(int colorsCount, byte opacity = 255)
    {
        Foreground.Opacity = opacity;
        Foreground.Colors = Background.GeneratePalette(colorsCount);
        return this;
    }

    public DPattern WithRandomizedForegroundColor(SixLabors.ImageSharp.Color[] palette)
    {
        Foreground.Colors = palette;
        return this;
    }

    public override void Render(Image image, GraphicsOptions graphicsOptions)
    {
        SixLabors.ImageSharp.Rectangle rect = Rectangle;

        if (rect.Width <= 0 || rect.Height <= 0)
            return;

        PatternBrush patternBrush = new(Foreground, Background, Pattern);
        DrawingOptions dopt = new() { GraphicsOptions = graphicsOptions };

        image.Mutate((x) =>
        {
            x.Fill(dopt, patternBrush, rect);
        });
    }
}
