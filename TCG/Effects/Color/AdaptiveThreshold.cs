using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Rnd.Randomizers.Parameters;

namespace TCG.Effects;

public class AdaptiveThreshold : IEffect
{
    public FloatParameter ThresholdLimit { get; set; } = new(1) { Value = 0.15f };

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.AdaptiveThreshold(ThresholdLimit.Value));
}
