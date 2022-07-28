using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;
using TCG.Rnd.Randomizers.Parameters;

namespace TCG.Drawables;

public class DPattern : IDrawable
{
    public RectangleParameter Rectangle { get; set; } = new RectangleParameter(new Rectangle());
    public Bool2DArrayParameter Pattern { get; set; } = new Bool2DArrayParameter(new bool[,] { { true, false}, { false, true } });
    public ColorParameter Background { get; set; } = new ColorParameter(Color.Transparent);
    public ColorParameter Foreground { get; set; } = new ColorParameter(Color.Black);
    public IList<IEffect> Effects { get; } = new List<IEffect>();


    public void Render(Image image, GraphicsOptions graphicsOptions)
    {
        PatternBrush patternBrush = new PatternBrush(Foreground, Background, Pattern);
        DrawingOptions dopt = new DrawingOptions() { GraphicsOptions = graphicsOptions };

        image.Mutate((x) =>
        {
            x.Fill(dopt, patternBrush, Rectangle);
        });
    }
}
