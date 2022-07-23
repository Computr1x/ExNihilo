using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using TCG.Base.Interfaces;
using TCG.Extensions.Processors;

namespace TCG.Effects
{
    public class Crystallize : IEffect
    {
        public int CrystalsCount { get; set; } = 64;
        public int Seed { get; set; } = 0;

        public void Render(Image image, GraphicsOptions graphicsOptions) =>
            image.Mutate(x => x.Crystallize(Seed, CrystalsCount));
    }
}
