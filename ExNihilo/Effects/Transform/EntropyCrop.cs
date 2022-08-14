using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using ExNihilo.Base.Interfaces;
using ExNihilo.Base.Parameters;

namespace ExNihilo.Effects;

/// <summary>
/// Defines effect that allow the application of entropy cropping operations on an <see cref="IDrawable"/>
/// </summary>
public class EntropyCrop : IEffect
{
    /// <summary>
    /// The threshold for entropic density.
    /// </summary>
    public FloatParameter Threshold { get; set; } = new(0.5f) { Min = 0, Max = 1 };

    /// <summary>
    /// <inheritdoc cref="EntropyCrop"/>
    /// </summary>
    public EntropyCrop() { }

    /// <summary>
    /// <inheritdoc cref="EntropyCrop"/>
    /// </summary>
    /// <param name="threshold"><inheritdoc cref="Threshold" path="/summary"/></param>
    public EntropyCrop(float threshold) { 
        Threshold.Value = threshold;
    }

    /// <summary>
    /// Set Threshold value
    /// </summary>
    /// <param name="value"><inheritdoc cref="Threshold" path="/summary"/></param>
    public EntropyCrop WithThreshold(float value)
    {
        Threshold.Value = value;
        return this;
    }
    /// <summary>
    /// Set Threshold randomization parameters.
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="Threshold" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="Threshold" path="/summary"/></param>
    public EntropyCrop WithRandomizedThreshold(float min, float max)
    {
        Threshold.Min = min;
        Threshold.Max = max;
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.EntropyCrop(Threshold));
}