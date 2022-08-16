using ExNihilo.Base;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ExNihilo.Effects;

/// <summary>
/// Defines effect that allow the application of padding operations on an <see cref="Visual"/>
/// </summary>
public class Pad : Effect
{
    /// <summary>
    /// The new width.
    /// </summary>
    public IntProperty Width { get; set; } = new(1, int.MaxValue, 0) { Min = 0, Max = 50 };
    /// <summary>
    /// The new height
    /// </summary>
    public IntProperty Height { get; set; } = new(1, int.MaxValue, 0) { Min = 0, Max = 50 };

    /// <summary>
    /// <inheritdoc cref="Pad"/>
    /// </summary>
    public Pad() { }

    /// <summary>
    /// <inheritdoc cref="Pad"/>
    /// </summary>
    /// <param name="width"><inheritdoc cref="Width" path="/summary"/></param>
    /// <param name="height"><inheritdoc cref="Height" path="/summary"/></param>
    public Pad(int width, int height) { 
        Width.Value = width;
        Height.Value = height;
    }

    /// <summary>
    /// Set Width value
    /// </summary>
    /// <param name="value"><inheritdoc cref="Width" path="/summary"/></param>
    public Pad WithWidth(int value)
    {
        Width.Value = value;
        return this;
    }
    /// <summary>
    /// Set Width randomization parameters.
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="Width" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="Width" path="/summary"/></param>
    public Pad WithRandomizedWidth(int min, int max)
    {
        Width.WithRandomizedValue(min, max);
        return this;
    }

    /// <summary>
    /// Set Height value
    /// </summary>
    /// <param name="value"><inheritdoc cref="Height" path="/summary"/></param>
    public Pad WithHeight(int value)
    {
        Height.Value = value;
        return this;
    }
    /// <summary>
    /// Set Height randomization parameters.
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="Height" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="Height" path="/summary"/></param>
    public Pad WithRandomizedHeight(int min, int max)
    {
        Height.WithRandomizedValue(min, max);
        return this;
    }

    public override void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Pad(Width, Height));
}
