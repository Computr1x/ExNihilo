using ExNihilo.Base;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ExNihilo.Effects;

/// <summary>
/// Defines effect that allow the application of skew operations on an <see cref="Visual"/>
/// </summary>
public class Skew : Effect
{
    /// <summary>
    /// The X angle, in degrees.
    /// </summary>
    public FloatProperty XDegree { get; set; } = new(0) { Min = 0, Max = 360 };
    /// <summary>
    /// The Y angle, in degrees.
    /// </summary>
    public FloatProperty YDegree { get; set; } = new(0) { Min = 0, Max = 360 };

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

    public override void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => 
            x.Transform(new AffineTransformBuilder().AppendSkewDegrees(XDegree, YDegree, new System.Numerics.Vector2(image.Width/2, image.Height/2))));
}
