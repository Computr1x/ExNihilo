using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using ExNihilo.Base.Interfaces;
using ExNihilo.Base.Parameters;
using ExNihilo.Base.Utils;

namespace ExNihilo.Base.Abstract;

public abstract class BaseDrawableWithBrushAndPen : BaseDrawable
{
    /// <summary>
    /// Represents the pen with which to stroke an object.
    /// </summary>
    public BrushParameter Brush { get; } = new();
    /// <summary>
    /// Represents the pen with which to outlined an object.
    /// </summary>
    public PenParameter Pen { get; } = new();
    /// <summary>
    /// Specifies the rendering type of an object
    /// </summary>
    public EnumParameter<DrawableType> Type { get;  } = new(DrawableType.Filled);
}

