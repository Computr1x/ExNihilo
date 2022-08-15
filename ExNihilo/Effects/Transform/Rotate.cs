using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using ExNihilo.Base.Interfaces;
using ExNihilo.Base.Properties;

namespace ExNihilo.Effects;

/// <summary>
/// Defines effect that allow the application of rotation operations on an <see cref="Drawable"/>
/// </summary>
public class Rotate : Effect
{
    /// <summary>
    /// Amount of rotation in degrees (-360-360).
    /// </summary>
    public FloatProperty Degree { get; set; } = new(-360, 360, 0) { Min = 0, Max = 360 };

    /// <summary>
    /// <inheritdoc cref="Rotate"/>
    /// </summary>
    public Rotate() { }

    /// <summary>
    /// <inheritdoc cref="Rotate"/>
    /// </summary>
    /// <param name="degree"><inheritdoc cref="Degree" path="/summary"/></param>
    public Rotate(float degree)
    {
        Degree.Value = degree;
    }

    /// <summary>
    /// Set Degree value
    /// </summary>
    /// <param name="value"><inheritdoc cref="Degree" path="/summary"/></param>
    public Rotate WithDegree(float value)
    {
        Degree.Value = value;
        return this;
    }
    /// <summary>
    /// Set Degree randomization parameters.
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="Degree" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="Degree" path="/summary"/></param>
    public Rotate WithRandomizedDegree(float min, float max)
    {
        Degree.Min = min;
        Degree.Max = max;
        return this;
    }

    public override void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Rotate(Degree));
}
