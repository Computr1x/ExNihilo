using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;
using TCG.Rnd.Randomizers.Parameters;

namespace TCG.Drawables;

public class DPattern : IDrawable
{
    public RectangleParameter Rectangle { get; } = new RectangleParameter();
    public Bool2DArrayParameter Pattern { get; } = new Bool2DArrayParameter(new bool[,] { { true, false}, { false, true } });
    public ColorParameter Background { get; } = new ColorParameter() { DefaultValue = Color.Transparent };
    public ColorParameter Foreground { get; } = new ColorParameter() { DefaultValue = Color.Black };
    public IList<IEffect> Effects { get; } = new List<IEffect>();

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


    public void Render(Image image, GraphicsOptions graphicsOptions)
    {
        PatternBrush patternBrush = new (Foreground, Background, Pattern);
        DrawingOptions dopt = new () { GraphicsOptions = graphicsOptions };

        image.Mutate((x) =>
        {
            x.Fill(dopt, patternBrush, Rectangle);
        });
    }
}
