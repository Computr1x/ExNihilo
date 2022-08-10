using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;

namespace TCG.Effects;

/// <summary>
/// Defines effect that allow the application of scale operations on an <see cref="IDrawable"/>
/// </summary>
public class Scale : IEffect
{
    /// <summary>
    /// Amount of scale by x axis.
    /// </summary>
    public FloatParameter XScale { get; set; } = new(0) { Min = 0, Max = 2 };
    /// <summary>
    /// Amount of scale by y axis.
    /// </summary>
    public FloatParameter YScale { get; set; } = new(0) { Min = 0, Max = 2 };

    /// <summary>
    /// <inheritdoc cref="Scale"/>
    /// </summary>
    public Scale() { }

    /// <summary>
    /// <inheritdoc cref="Scale"/>
    /// </summary>
    /// <param name="xScale"><inheritdoc cref="XScale" path="/summary"/></param>
    /// <param name="yScale"><inheritdoc cref="YScale" path="/summary"/></param>
    public Scale(float xScale, float yScale)
    {
        XScale.Value = xScale;
        YScale.Value = yScale;
    }

    /// <summary>
    /// Set XScale value
    /// </summary>
    /// <param name="value"><inheritdoc cref="XScale" path="/summary"/></param>
    public Scale WithXScale(float value)
    {
        XScale.Value = value;
        return this;
    }
    /// <summary>
    /// Set XScale randomization parameters.
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="XScale" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="XScale" path="/summary"/></param>
    public Scale WithRandomizedXScale(float min, float max)
    {
        XScale.WithRandomizedValue(min, max);
        return this;
    }

    /// <summary>
    /// Set YScale value
    /// </summary>
    /// <param name="value"><inheritdoc cref="YScale" path="/summary"/></param>
    public Scale WithYScale(float value)
    {
        YScale.Value = value;
        return this;
    }
    /// <summary>
    /// Set YScale randomization parameters.
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="YScale" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="YScale" path="/summary"/></param>
    public Scale WithRandomizedYScale(float min, float max)
    {
        YScale.WithRandomizedValue(min, max);
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x =>
            x.Transform(new AffineTransformBuilder().AppendScale(new System.Numerics.Vector2(XScale, YScale))));
}
