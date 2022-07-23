using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;

namespace TCG.Effects
{
    public class GaussianNoise : IEffect
    {
        public int Seed { get; set; } = 0;
        public byte Amount { get; set; } = 255;
        public bool Monochrome { get; set; } = false;

        public void Render(Image image, GraphicsOptions graphicsOptions) =>
            image.Mutate(x => x.GaussianNoise(Seed, Amount, Monochrome));
    }
}
