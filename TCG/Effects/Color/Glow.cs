using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;

namespace TCG.Effects;

/// <summary>
/// Defines effect that allow the application of glow effect of the <see cref="IDrawable"/>
/// </summary>
public class Glow : IEffect
{
    /// <summary>
    /// The radius of the glow
    /// </summary>
    public FloatParameter Radius { get; set; } = new FloatParameter(15) { Min = 1, Max = 150 };

    /// <summary>
    /// <inheritdoc cref="Glow"/>
    /// </summary>
    public Glow() { }

    /// <summary>
    /// <inheritdoc cref="Glow"/>
    /// </summary>
    /// <param name="radius"><inheritdoc cref="Radius" path="/summary"/></param>
    public Glow(float radius)
    {
        Radius.Value = radius;
    }

    /// <summary>
    /// Set Radius value
    /// </summary>
    /// <param name="value"><inheritdoc cref="Radius" path="/summary"/></param>
    public Glow WithRadius(float value)
    {
        Radius.Value = value;
        return this;
    }
    /// <summary>
    /// Set Radius value randomization parameters.
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="Radius" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="Radius" path="/summary"/></param>
    public Glow WithRandomizedRadius(float min, float max)
    {
        Radius.Min = min;
        Radius.Max = max;
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.Glow(Radius));
}
