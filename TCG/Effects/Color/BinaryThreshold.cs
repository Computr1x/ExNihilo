using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;

namespace TCG.Effects;

/// <summary>
/// Defines effect that allow the application of binarize effect to the <see cref="IDrawable"/>
/// </summary>
public class BinaryThreshold : IEffect
{
    /// <summary>
    /// Threshold limit (0.0-1.0) to consider for binarization.
    /// </summary>
    public FloatParameter ThresholdLimit { get; set; } = new FloatParameter(0, 1, 0.5f) { Min = 0f, Max = 1f };

    /// <summary>
    /// <inheritdoc cref="BinaryThreshold"/>
    /// </summary>
    /// <param name="thresholdLimit"><inheritdoc cref="ThresholdLimit" path="/summary"/></param>
    public BinaryThreshold() { }

    /// <summary>
    /// <inheritdoc cref="BinaryThreshold"/>
    /// </summary>
    /// <param name="thresholdLimit"><inheritdoc cref="ThresholdLimit" path="/summary"/></param>
    public BinaryThreshold(float thresholdLimit)
    {
        ThresholdLimit.Value = thresholdLimit;
    }

    /// <summary>
    /// Set ThresholdLimit value
    /// </summary>
    /// <param name="value"><inheritdoc cref="ThresholdLimit" path="/summary"/></param>
    public BinaryThreshold WithThresholdLimit(float value)
    {
        ThresholdLimit.Value = value;
        return this;
    }
    /// <summary>
    /// Set ThresholdLimit value randomization parameters
    /// </summary>
    /// <param name="min">Minimal randomization value. <inheritdoc cref="ThresholdLimit" path="/summary"/></param>
    /// <param name="max">Maximal randomization value. <inheritdoc cref="ThresholdLimit" path="/summary"/></param>
    public BinaryThreshold WithRandomizedThresholdLimit(float min, float max)
    {
        ThresholdLimit.Min = min;
        ThresholdLimit.Max = max;
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.BinaryThreshold(ThresholdLimit));

}