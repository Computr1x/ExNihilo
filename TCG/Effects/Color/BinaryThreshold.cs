using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;

namespace TCG.Effects;

public class BinaryThreshold : IEffect
{
    public FloatParameter ThresholdLimit { get; set; } = new FloatParameter(0.5f) { Min = 0f, Max = 1f };

    public BinaryThreshold() { }

    public BinaryThreshold(float thresholdLimit)
    {
        ThresholdLimit.Value = thresholdLimit;
    }

    public BinaryThreshold WithThresholdLimit(float value)
    {
        ThresholdLimit.Value = value;
        return this;
    }

    public BinaryThreshold WithRandomizedThresholdLimit(float min, float max)
    {
        ThresholdLimit.Min = min;
        ThresholdLimit.Max = max;
        return this;
    }

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.BinaryThreshold(ThresholdLimit));

}