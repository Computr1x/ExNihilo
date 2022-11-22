using ExNihilo.Base;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ExNihilo.Effects;

/// <summary>
/// Defines effect that allow the application of scale operations on an <see cref="Visual"/>
/// </summary>
public class Scale : Effect
{
    public override EffectType EffectType => EffectType.Transform;
    /// <summary>
    /// Amount of scale by x axis.
    /// </summary>
    public FloatProperty XScale { get; set; } = new(0, float.MaxValue, 1) { Min = 0, Max = 2 };
    /// <summary>
    /// Amount of scale by y axis.
    /// </summary>
    public FloatProperty YScale { get; set; } = new(0, float.MaxValue, 1) { Min = 0, Max = 2 };

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

    public override void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x =>
            x.Transform(new AffineTransformBuilder().AppendScale(new System.Numerics.Vector2(XScale, YScale))));
}
