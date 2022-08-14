using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using ExNihilo.Base.Interfaces;
using ExNihilo.Base.Parameters;

namespace ExNihilo.Effects;

/// <summary>
/// Defines effect that allow the application of skew operations on an <see cref="IDrawable"/>
/// </summary>
public class Skew : IEffect
{
    /// <summary>
    /// The X angle, in degrees.
    /// </summary>
    public FloatParameter XDegree { get; set; } = new(0) { Min = 0, Max = 360 };
    /// <summary>
    /// The Y angle, in degrees.
    /// </summary>
    public FloatParameter YDegree { get; set; } = new(0) { Min = 0, Max = 360 };

    /// <summary>
    /// <inheritdoc cref="Skew"/>
    /// </summary>
    public Skew() { }

    /// <summary>
    /// <inheritdoc cref="Skew"/>
    /// </summary>
    /// <param name="xDegree"><inheritdoc cref="XDegree" path="/summary"/></param>
    /// <param name="yDegree"><inheritdoc cref="YDegree" path="/summary"/></param>
    public Skew(float xDegree, float yDegree)
    {
        XDegree.Value = xDegree;
        YDegree.Value = yDegree;
    }

    /// <summary>
    /// Set XDegree value
    /// </summary>
    /// <param name="value"><inheritdoc cref="XDegree" path="/summary"/></param>
    public Skew WithXDegree(float value)
    {
        XDegree.Value = value;
        return this;
    }
    /// <summary>
    /// Set XDegree randomization parameters.
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="XDegree" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="XDegree" path="/summary"/></param>
    public Skew WithRandomizedXDegree(float min, float max)
    {
        XDegree.WithRandomizedValue(min, max);
        return this;
    }
    /// <summary>
    /// Set YDegree value
    /// </summary>
    /// <param name="value"><inheritdoc cref="YDegree" path="/summary"/></param>
    public Skew WithYDegree(float value)
    {
        YDegree.Value = value;
        return this;
    }
    /// <summary>
    /// Set YDegree randomization parameters.
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="YDegree" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="YDegree" path="/summary"/></param>
    public Skew WithRandomizedYDegree(float min, float max)
    {
        YDegree.WithRandomizedValue(min, max);
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x =>
            x.Transform(new AffineTransformBuilder().AppendSkewDegrees(XDegree, YDegree)));
}
