using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;

namespace TCG.Effects;

public class AdaptiveThreshold : IEffect
{
    public float ThresholdLimit { get; set; } = 0.15f;

    public void Render(Image image, GraphicsOptions graphicsOptions) =>
        image.Mutate(x => x.AdaptiveThreshold(ThresholdLimit));

    
}
