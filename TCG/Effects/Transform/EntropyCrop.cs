using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;

namespace TCG.Effects;

/// <summary>
/// Defines effect that allow the application of entropy cropping operations on an <see cref="IDrawable"/>
/// </summary>
public class EntropyCrop : IEffect
{
    /// <summary>
    /// The threshold for entropic density.
    /// </summary>
    public FloatParameter Threshold { get; set; } = new(0.5f) { Min = 0, Max = 1 };

    public EntropyCrop() { }

    public EntropyCrop(float threshold) { 
        Threshold.Value = threshold;
    }

    public EntropyCrop WithThreshold(float value)
    {
        Threshold.Value = value;
        return this;
    }

    public EntropyCrop WithRandomizedThreshold(float min, float max)
    {
        Threshold.Min = min;
        Threshold.Max = max;
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.EntropyCrop(Threshold));
}