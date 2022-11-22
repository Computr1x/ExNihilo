using ExNihilo.Base;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace ExNihilo.Effects;

/// <summary>
/// Defines effect that allow the application of Bradley adaptive threshold to the <see cref="Visual"/>.
/// </summary>
public class AdaptiveThreshold : Effect
{
    public override EffectType EffectType => EffectType.Color;
    /// <summary>
    /// Threshold limit (0.0-1.0) to consider for binarization.
    /// </summary>
    public FloatProperty ThresholdLimit { get; set; } = new(0, 1, 0.15f) { Min = 0f, Max = 1f };

    /// <summary>
    /// <inheritdoc cref="AdaptiveThreshold"/>
    /// </summary>
    public AdaptiveThreshold() { }

    /// <summary>
    /// <inheritdoc cref="AdaptiveThreshold"/>
    /// </summary>
    /// <param name="thresholdLimit"><inheritdoc cref="ThresholdLimit" path="/summary"/></param>
    public AdaptiveThreshold(float thresholdLimit)
    {
        ThresholdLimit.Value = thresholdLimit;
    }

    /// <summary>
    /// Set ThresholdLimit value
    /// </summary>
    /// <param name="value"><inheritdoc cref="ThresholdLimit" path="/summary"/></param>
    public AdaptiveThreshold WithThresholdLimit(float value)
    {
        ThresholdLimit.Value = value;
        return this;
    }

    /// <summary>
    /// Set ThresholdLimit value randomization parameters
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="ThresholdLimit" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="ThresholdLimit" path="/summary"/></param>
    public AdaptiveThreshold WithRandomizedThresholdLimit(float min, float max)
    {
        ThresholdLimit.Min = min;
        ThresholdLimit.Max = max;
        return this;
    }

    public override void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.AdaptiveThreshold(ThresholdLimit));
}
