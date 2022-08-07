using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Abstract;
using TCG.Base.Parameters;

namespace TCG.Drawables;

public class Pattern : BaseDrawable
{
    public RectangleParameter Area { get; } = new RectangleParameter();
    public Bool2DArrayParameter Template { get; } = new Bool2DArrayParameter(new bool[,] { { true, false }, { false, true } });
    public ColorParameter Background { get; } = new ColorParameter(SixLabors.ImageSharp.Color.Transparent);
    public ColorParameter Foreground { get; } = new ColorParameter(SixLabors.ImageSharp.Color.Black, 5);

    public Pattern() { }

    public Pattern(SixLabors.ImageSharp.Rectangle rectangle, bool[,] pattern)
    {
        Area.Value = rectangle;
        Template.Value = pattern;
    }

    public Pattern(SixLabors.ImageSharp.Rectangle rectangle, bool[,] pattern, Color foreground) : this(rectangle, pattern)
    {
        Foreground.Value = foreground;
    }

    public Pattern(SixLabors.ImageSharp.Rectangle rectangle, bool[,] pattern, Color foreground, Color background) : this(rectangle, pattern, foreground)
    {
        Background.Value = background;
    }

    public Pattern WithPoint(Point p)
    {
        Area.Point.Value = p;
        return this;
    }

    public Pattern WithRandomizedPoint(int minX, int maxX, int minY, int maxY)
    {
        Area.Point.X.Min = minX;
        Area.Point.X.Max = maxX;
        Area.Point.Y.Min = minY;
        Area.Point.Y.Min = maxY;
        return this;
    }

    public Pattern WithSize(Size size)
    {
        Area.Size.Value = size;
        return this;
    }

    public Pattern WithRandomizedSize(int minWidth, int maxWidth, int minHeight, int maxHeight)
    {
        Area.Size.Width.Min = minWidth;
        Area.Size.Width.Max = maxWidth;
        Area.Size.Height.Min = minHeight;
        Area.Size.Height.Max = maxHeight;
        return this;
    }

    public Pattern WithTemplate(bool[,] template)
    {
        Template.Value = template;
        return this;
    }

    public Pattern WithTemplate(Action<Bool2DArrayParameter> templateSetter)
    {
        templateSetter(Template);
        return this;
    }

    public Pattern WithRandomizedTemplate(int size)
    {
        Template.Width.Value = Template.Height.Value = size;
        return this;
    }

    public Pattern WithBackgroundColor(SixLabors.ImageSharp.Color color)
    {
        Background.Value = color;
        return this;
    }

    public Pattern WithRandomizedBackgroundColor(int colorsCount, byte opacity = 255)
    {
        Background.Opacity = opacity;
        Background.Colors = Background.GeneratePalette(colorsCount);
        return this;
    }

    public Pattern WithRandomizedBackgroundColor(SixLabors.ImageSharp.Color[] palette)
    {
        Background.Colors = palette;
        return this;
    }

    public Pattern WithForegroundColor(SixLabors.ImageSharp.Color color)
    {
        Foreground.Value = color;
        return this;
    }

    public Pattern WithRandomizedForegroundColor(int colorsCount, byte opacity = 255)
    {
        Foreground.Opacity = opacity;
        Foreground.Colors = Background.GeneratePalette(colorsCount);
        return this;
    }

    public Pattern WithRandomizedForegroundColor(SixLabors.ImageSharp.Color[] palette)
    {
        Foreground.Colors = palette;
        return this;
    }

    public override void Render(Image image, GraphicsOptions graphicsOptions)
    {
        SixLabors.ImageSharp.Rectangle rect = Area.Value ?? new SixLabors.ImageSharp.Rectangle(Area.Point, Area.Size);

        if (rect.Width <= 0 || rect.Height <= 0)
            return;

        PatternBrush patternBrush = new(Foreground, Background, Template);
        DrawingOptions dopt = new() { GraphicsOptions = graphicsOptions };

        image.Mutate((x) =>
        {
            x.Fill(dopt, patternBrush, rect);
        });
    }
}
