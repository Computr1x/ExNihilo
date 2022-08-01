using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;
using TCG.Base.Utils;

namespace TCG.Base.Abstract;

public abstract class BaseDrawable : IDrawable
{
    public BrushParameter Brush { get; } = new(Brushes.Solid(Color.Black));
    public PenParameter Pen { get; set; } = new(Pens.Solid(Color.White, 1));

    public EnumParameter<DrawableType> Type { get; set; } = new(DrawableType.Filled);

    public IList<IEffect> Effects { get; }

    protected BaseDrawable()
    {
        Effects = new List<IEffect>();
    }

    public abstract void Render(Image image, GraphicsOptions graphicsOptions);

}

