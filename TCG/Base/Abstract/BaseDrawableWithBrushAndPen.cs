using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;
using TCG.Base.Utils;

namespace TCG.Base.Abstract;

public abstract class BaseDrawableWithBrushAndPen : BaseDrawable
{
    public BrushParameter Brush { get; } = new() {  DefaultValue = Brushes.Solid(Color.Black) };
    public PenParameter Pen { get; } = new(Pens.Solid(Color.White, 1));
    public EnumParameter<DrawableType> Type { get;  } = new(DrawableType.Filled);
}

