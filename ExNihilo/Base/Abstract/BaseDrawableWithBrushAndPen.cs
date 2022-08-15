using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using ExNihilo.Base.Interfaces;
using ExNihilo.Base.Properties;
using ExNihilo.Base.Utils;

namespace ExNihilo.Base.Abstract;

public abstract class BaseDrawableWithBrushAndPen : Drawable
{
    /// <summary>
    /// Represents the pen with which to stroke an object.
    /// </summary>
    public BrushProperty Brush { get; } = new();
    /// <summary>
    /// Represents the pen with which to outlined an object.
    /// </summary>
    public PenProperty Pen { get; } = new();
    /// <summary>
    /// Specifies the rendering type of an object
    /// </summary>
    public EnumProperty<DrawableType> Type { get;  } = new(DrawableType.Filled);
}

