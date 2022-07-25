using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;

namespace TCG.Effects
{
    public class BinaryThreshold : IEffect
    {
        public float ThresholdLimit { get; set; } = 0.5f;

        public void Render(Image image, GraphicsOptions graphicsOptions) =>
            image.Mutate(x => x.BinaryThreshold(ThresholdLimit));

    }
}