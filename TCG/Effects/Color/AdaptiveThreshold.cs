using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;

namespace TCG.Effects;

public class AdaptiveThreshold : IEffect
{
    public FloatParameter ThresholdLimit { get; set; } = new(0.15f) { Min = 0f, Max = 1f };

    public AdaptiveThreshold() { }

    public AdaptiveThreshold(float thresholdLimit)
    {
        ThresholdLimit.Value = thresholdLimit;
    }

    public AdaptiveThreshold WithThresholdLimit(float value)
    {
        ThresholdLimit.Value = value;
        return this;
    }

    public AdaptiveThreshold WithRandomizedThresholdLimit(float min, float max)
    {
        ThresholdLimit.Min = min;
        ThresholdLimit.Max = max;
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.AdaptiveThreshold(ThresholdLimit));
}
