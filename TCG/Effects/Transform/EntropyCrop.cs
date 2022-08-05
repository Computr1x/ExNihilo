using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;

namespace TCG.Effects;

public class EntropyCrop : IEffect
{
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