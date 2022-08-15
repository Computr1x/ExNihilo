using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using ExNihilo.Base.Interfaces;
using ExNihilo.Base.Properties;

namespace ExNihilo.Effects;

/// <summary>
/// Defines effect that allow the application of resize operations on an <see cref="Drawable"/>
/// </summary>
public class Resize : Effect
{
    /// <summary>
    /// The target image width.
    /// </summary>
    public IntProperty Width { get; set; } = new(1, int.MaxValue, 0);
    /// <summary>
    /// The target image height.
    /// </summary>
    public IntProperty Height { get; set; } = new(1, int.MaxValue, 0);

    /// <summary>
    /// <inheritdoc cref="Resize"/>
    /// </summary>
    public Resize() { }

    /// <summary>
    /// <inheritdoc cref="Resize"/>
    /// </summary>
    /// <param name="width"><inheritdoc cref="Width" path="/summary"/></param>
    /// <param name="height"><inheritdoc cref="Height" path="/summary"/></param>
    public Resize(int width, int height)
    {
        Width.Value = width;
        Height.Value = height;
    }

    /// <summary>
    /// Set Width value
    /// </summary>
    /// <param name="value"><inheritdoc cref="Width" path="/summary"/></param>
    public Resize WithWidth(int value)
    {
        Width.Value = value;
        return this;
    }
    /// <summary>
    /// Set Width randomization parameters.
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="Width" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="Width" path="/summary"/></param>
    public Resize WithRandomizedWidth(int min, int max)
    {
        Width.Min = min;
        Width.Max = max;
        return this;
    }
    /// <summary>
    /// Set Height value
    /// </summary>
    /// <param name="value"><inheritdoc cref="Height" path="/summary"/></param>
    public Resize WithHeight(int value)
    {
        Height.Value = value;
        return this;
    }
    /// <summary>
    /// Set Height randomization parameters.
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="Height" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="Height" path="/summary"/></param>
    public Resize WithRandomizedHeight(int min, int max)
    {
        Height.Min = min;
        Height.Max = max;
        return this;
    }

    public override void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Resize(Width, Height));
}