using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCG.Base.Interfaces;

namespace TCG.Drawables;

public class DPattern : IDrawable
{
    public Rectangle Rectangle { get; set; }
    public bool[,] Pattern { get; set; }
    public Color Background { get; set; }
    public Color Foreground { get; set; }
    public IList<IEffect> Effects { get; }

    public DPattern(Rectangle rect, bool[,] pattern, Color background, Color foreground) : base()
    {
        Rectangle = rect;
        Pattern = pattern;
        Background = background;
        Foreground = foreground;
        Effects = new List<IEffect>();
    }

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
