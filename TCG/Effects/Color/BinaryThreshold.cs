using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;

namespace TCG.Effects;

public class BinaryThreshold : IEffect
{
    public FloatParameter ThresholdLimit { get; set; } = new FloatParameter(0.5f) { Min = 0f, Max = 1f };

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.BinaryThreshold(ThresholdLimit));

}