using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;
using TCG.Base.Utils;

namespace TCG.Base.Abstract;

public abstract class BaseDrawableWithBrushAndPen : BaseDrawable
{
    public BrushParameter Brush { get; } = new() {  DefaultValue = Brushes.Solid(Color.Black) };
    public PenParameter Pen { get; set; } = new(Pens.Solid(Color.White, 1));
    public EnumParameter<DrawableType> Type { get; set; } = new(DrawableType.Filled);

    public BaseDrawableWithBrushAndPen WithBrush(IBrush brush)
    {
        Brush.Value = brush;
        return this;
    }
    public BaseDrawableWithBrushAndPen WithBrush(Action<BrushParameter> actionBrush)
    {
        actionBrush(Brush);
        return this;
    }

    public BaseDrawableWithBrushAndPen WithPen(IPen pen)
    {
        Pen.Value = pen;
        return this;
    }

    public BaseDrawableWithBrushAndPen WithPen(Action<PenParameter> actionPen)
    {
        actionPen(Pen);
        return this;
    }

    public BaseDrawableWithBrushAndPen WithType(DrawableType value)
    {
        Type.Value = value;
        return this;
    }
}

