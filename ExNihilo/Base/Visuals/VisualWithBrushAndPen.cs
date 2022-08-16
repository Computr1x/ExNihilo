namespace ExNihilo.Base;

public abstract class VisualWithBrushAndPen : Visual
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
    public EnumProperty<VisualType> Type { get; } = new(VisualType.Filled);
}

