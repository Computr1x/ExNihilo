using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Base.Parameters;

namespace TCG.Effects;

public class AdaptiveThreshold : IEffect
{
    public FloatParameter ThresholdLimit { get; set; } = new(0.15f) { Min = 0f, Max = 1f };

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.AdaptiveThreshold(ThresholdLimit));
}
