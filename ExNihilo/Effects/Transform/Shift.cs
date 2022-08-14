using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using ExNihilo.Base.Interfaces;
using ExNihilo.Base.Parameters;

namespace ExNihilo.Effects;

/// <summary>
/// Defines effect that allow the application of coordinates shift operations on an <see cref="IDrawable"/>
/// </summary>
public class Shift : IEffect
{
    /// <summary>
    /// Amount of shift by x axis.
    /// </summary>
    public FloatParameter XShift { get; set; } = new(0) { Min = 0, Max = 50 };
    /// <summary>
    /// Amount of shift by y axis.
    /// </summary>
    public FloatParameter YShift { get; set; } = new(0) { Min = 0, Max = 50 };

    /// <summary>
    /// <inheritdoc cref="Shift"/>
    /// </summary>
    public Shift() { }

    /// <summary>
    /// <inheritdoc cref="Shift"/>
    /// </summary>
    /// <param name="xShift"><inheritdoc cref="XShift" path="/summary"/></param>
    /// <param name="yShift"><inheritdoc cref="YShift" path="/summary"/></param>
    public Shift(float xShift, float yShift)
    {
        XShift.Value = xShift;
        YShift.Value = yShift;
    }

    /// <summary>
    /// Set XShift value
    /// </summary>
    /// <param name="value"><inheritdoc cref="XShift" path="/summary"/></param>
    public Shift WithXShift(float value)
    {
        XShift.Value = value;
        return this;
    }
    /// <summary>
    /// Set XShift randomization parameters.
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="XShift" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="XShift" path="/summary"/></param>
    public Shift WithRandomizedXShift(float min, float max)
    {
        XShift.WithRandomizedValue(min, max);
        return this;
    }

    /// <summary>
    /// Set YShift value
    /// </summary>
    /// <param name="value"><inheritdoc cref="YShift" path="/summary"/></param>
    public Shift WithYShift(float value)
    {
        YShift.Value = value;
        return this;
    }
    /// <summary>
    /// Set YShift randomization parameters.
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="YShift" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="YShift" path="/summary"/></param>
    public Shift WithRandomizedYShift(float min, float max)
    {
        YShift.WithRandomizedValue(min, max);
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Transform(new AffineTransformBuilder().AppendTranslation(new System.Numerics.Vector2(XShift, YShift))));
}
